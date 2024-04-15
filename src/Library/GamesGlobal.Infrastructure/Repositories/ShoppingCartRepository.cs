using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly GamesGlobalDbContext _dbContext;
    private readonly IMapper _mapper;

    public ShoppingCartRepository(GamesGlobalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ShoppingCartDomainModel> GetOrCreateShoppingCartAsync(string userIdentifier)
    {
        // Check if a cart for the user already exists using AnyAsync for efficiency
        var cartExists = await _dbContext.ShoppingCarts
            .AnyAsync(c => c.UserIdentifier == userIdentifier);

        ShoppingCartDataModel shoppingCartDataModel;

        if (!cartExists)
        {
            // Create a new cart if one doesn't exist
            shoppingCartDataModel = new ShoppingCartDataModel
            {
                UserIdentifier = userIdentifier
            };

            _dbContext.ShoppingCarts.Add(shoppingCartDataModel);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            // Load the existing cart with Items, Product, and Attributes information
            shoppingCartDataModel = await _dbContext.ShoppingCarts
                .AsSplitQuery()
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .Include(c => c.Items)
                .ThenInclude(i => i.Attributes)
                .SingleAsync(c => c.UserIdentifier == userIdentifier);
        }

        // Map the data model to the domain model before returning
        return _mapper.Map<ShoppingCartDomainModel>(shoppingCartDataModel);
    }

    public async Task<ShoppingCartItemDomainModel?> GetCartItemByIdAsync(Guid itemId)
    {
        var cartItemDataModel = await _dbContext.ShoppingCartItems
            .AsNoTracking()
            .Include(a => a.Attributes)
            .FirstOrDefaultAsync(sci => sci.Id == itemId);

        return _mapper.Map<ShoppingCartItemDomainModel>(cartItemDataModel);
    }

    public async Task AddItemToCartAsync(Guid shoppingCartId, ShoppingCartItemDomainModel item)
    {
        // Assume that shopping cart exists;
        var shoppingCartDataModel = await _dbContext.ShoppingCarts
            .Include(c => c.Items)
            .ThenInclude(a => a.Attributes)
            .FirstOrDefaultAsync(c => c.Id == shoppingCartId);

        // Mapping and adding to the cart's collection in preparation for persistence.
        var itemDataModel = _mapper.Map<ShoppingCartItemDataModel>(item);
        shoppingCartDataModel!.Items.Add(itemDataModel);

        // Change tracking picks up the addition; Save occurs even if cart is null.
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCartItemAsync(ShoppingCartItemDomainModel cartItem)
    {
        // Mapping assumes correct configuration with AutoMapper.
        var itemDataModel = _mapper.Map<ShoppingCartItemDataModel>(cartItem);

        // Leverage EF Core's change tracking for update.
        _dbContext.ShoppingCartItems.Update(itemDataModel);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> RemoveItemFromCartAsync(Guid itemId, Guid shoppingCartId)
    {
        // Check for item existence before initiating remove operation.
        var itemExists = await _dbContext.ShoppingCartItems
            .AnyAsync(i => i.Id == itemId && i.ShoppingCartId == shoppingCartId);

        if (itemExists)
        {
            // Utilize stub entity for deletion to avoid full entity graph materialization.
            var cartItem = new ShoppingCartItemDataModel { Id = itemId };
            _dbContext.ShoppingCartItems.Attach(cartItem);
            _dbContext.ShoppingCartItems.Remove(cartItem);

            // Rely on cascade delete convention to handle removal of dependent entities.
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Early exit if item isn't found, signaling no operation was performed.
        return false;
    }

    public async Task<bool> ClearCartAsync(Guid shoppingCartId)
    {
        // Optimize removal operation by querying for IDs only
        var cartItemIds = await _dbContext.ShoppingCartItems
            .Where(c => c.ShoppingCartId == shoppingCartId)
            .Select(c => c.Id)
            .ToListAsync();

        if (cartItemIds.Count > 0)
        {
            // Leverage tracked entities for deletion without full materialization
            var dummyCartItems = cartItemIds.Select(id => new ShoppingCartItemDataModel { Id = id }).ToList();
            _dbContext.ShoppingCartItems.RemoveRange(dummyCartItems);

            // Context aware of cascade delete configuration for related entities
            await _dbContext.SaveChangesAsync();
            return true;
        }

        // Implicitly communicates: shopping cart with the provided ID is empty or does not exist.
        return false;
    }
}