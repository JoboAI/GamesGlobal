using AutoMapper;
using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly GamesGlobalDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductRepository(GamesGlobalDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ProductDomainModel?> GetByIdAsync(Guid productId)
    {
        var productEntity = await _dbContext.Products
            .Include(a => a.SpecificationAttributes)
            .FirstOrDefaultAsync(p => p.Id == productId);

        return _mapper.Map<ProductDomainModel>(productEntity);
    }
}