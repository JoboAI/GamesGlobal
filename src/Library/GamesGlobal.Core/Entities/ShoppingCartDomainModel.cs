using GamesGlobal.Core.Entities.Common;

namespace GamesGlobal.Core.Entities;

public class ShoppingCartDomainModel : BaseDomainModel
{
    /// <summary>
    ///     List of attributes providing additional customization for the shopping cart item.
    /// </summary>
    public List<ShoppingCartItemDomainModel> Items { get; set; }
}