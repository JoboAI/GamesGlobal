using AutoMapper;
using GamesGlobal.Infrastructure.Mappings;

namespace GamesGlobal.UnitTest.Fixtures;

public class AutoMapperFixture
{
    public AutoMapperFixture()
    {
        Configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(ProductProfile).Assembly); });
        Mapper = Configuration.CreateMapper();
    }

    public IConfigurationProvider Configuration { get; }
    public IMapper Mapper { get; }
}