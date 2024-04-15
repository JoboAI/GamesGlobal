using GamesGlobal.UnitTest.Fixtures;
using Xunit;

namespace GamesGlobal.UnitTest.Collections;

[CollectionDefinition("Handler Test Collection")]
public class HandlerTestCollection : ICollectionFixture<AutoMapperFixture>
{
}