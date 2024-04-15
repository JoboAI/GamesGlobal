using GamesGlobal.IntegrationTest.Fixtures;
using GamesGlobal.Web.Api;
using Xunit;

namespace GamesGlobal.IntegrationTest.Collections;

[CollectionDefinition("GamesGlobal Api Test Collection")]
public class GamesGlobalApiTestCollection : ICollectionFixture<GamesGlobalWebApplicationFactory<Program>>
{
}