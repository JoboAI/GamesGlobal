using GamesGlobal.Core.Interfaces.Common;
using GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

namespace GamesGlobal.UnitTest.Fixtures;

public class TestUserContext : IUserContext
{
    public string GetUserId()
    {
        return ShoppingCartDatabaseSeed.AliceShoppingCartId.ToString();
    }
}