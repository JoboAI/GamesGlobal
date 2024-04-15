using GamesGlobal.Core.Entities;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

namespace GamesGlobal.Test.Shared.Factories;

public static class ShoppingCartTestDataFactory
{
    public static ShoppingCartDataModel CreateShoppingCartDataModel(
        string userIdentifier = "User001"
    )
    {
        var shoppingCart = new ShoppingCartDataModel
        {
            UserIdentifier = userIdentifier,
            Items = new List<ShoppingCartItemDataModel>()
        };

        shoppingCart.Items.Add(CreateShoppingCartItemDataModel(shoppingCart.Id,
            ProductTestDataFactory.CreateProductDataModel()));
        return shoppingCart;
    }

    public static ShoppingCartItemDataModel CreateShoppingCartItemDataModel(
        Guid shoppingCartId,
        ProductDataModel product,
        int quantity = 1
    )
    {
        var shoppingCartItem = new ShoppingCartItemDataModel
        {
            ProductId = product.Id,
            Product = product,
            Quantity = quantity,
            ShoppingCartId = shoppingCartId,
            Attributes = new List<ShoppingCartItemAttributeDataModel>()
        };
        return shoppingCartItem;
    }

    public static ShoppingCartItemAttributeDataModel CreateShoppingCartItemAttributeDataModel(
        Guid shoppingCartItemId,
        Guid productSpecificationAttributeId = default
    )
    {
        if (productSpecificationAttributeId == default) productSpecificationAttributeId = Guid.NewGuid();

        var attribute = new ShoppingCartItemAttributeDataModel
        {
            ShoppingCartItemId = shoppingCartItemId,
            ProductSpecificationAttributeId = productSpecificationAttributeId
        };
        return attribute;
    }


    public static ShoppingCartDomainModel CreateShoppingCartDomainModel()
    {
        var shoppingCart = new ShoppingCartDomainModel
        {
            Items = new List<ShoppingCartItemDomainModel>()
        };

        shoppingCart.Items.Add(CreateShoppingCartItemDomainModel());

        return shoppingCart;
    }

    public static ShoppingCartItemDomainModel CreateShoppingCartItemDomainModel(
        Guid productId = default,
        decimal unitPrice = 10m,
        int quantity = 1
    )
    {
        if (productId == default) productId = Guid.NewGuid();

        return new ShoppingCartItemDomainModel
        {
            ProductId = productId,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Attributes = new List<IShoppingCartItemAttributeDomainModel>()
        };
    }

    public static IShoppingCartItemAttributeDomainModel CreateShoppingCartItemAttributeDomainModel(
        Guid productSpecificationAttributeId = default
    )
    {
        if (productSpecificationAttributeId == default) productSpecificationAttributeId = Guid.NewGuid();

        return new ImageShoppingCartItemAttributeDomainModel
        {
            ProductSpecificationAttributeId = productSpecificationAttributeId,
            ImageId = ImageDatabaseSeed.SeededImageId,
            AttributeType = ProductSpecificationAttributeType.Image
        };
    }
}