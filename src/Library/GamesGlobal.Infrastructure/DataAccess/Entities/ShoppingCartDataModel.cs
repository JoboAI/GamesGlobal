using GamesGlobal.Infrastructure.DataAccess.Entities.Common;

namespace GamesGlobal.Infrastructure.DataAccess.Entities;

/// <summary>
///     Represents an item within a shopping cart.
/// </summary>
public class ShoppingCartDataModel : BaseEntity
{
    /// <summary>
    ///     Foreign key relating to the user who owns the shopping cart.
    /// </summary>
    public string UserIdentifier { get; set; }

    /// <summary>
    ///     A collection of ShoppingCart related to this User.
    /// </summary>
    public virtual List<ShoppingCartItemDataModel> Items { get; set; } =
        new();
}