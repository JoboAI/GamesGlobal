using AutoMapper;
using GamesGlobal.Core.Interfaces.Repositories;
using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.Mappings;
using GamesGlobal.Infrastructure.Repositories;

namespace GamesGlobal.UnitTest.Fixtures;

public class DatabaseFixture
{
    private readonly IMapper _mapper;

    public DatabaseFixture()
    {
        _mapper = ConfigureAutoMapper();

        var dbOptions = InMemoryDatabaseHelper.CreateNewContextOptions();
        Context = new GamesGlobalDbContext(new TestUserContext(), dbOptions);
        DebtorRepository = new ProductRepository(Context, _mapper);
        ReceivableRepository = new ShoppingCartRepository(Context, _mapper);
        ImageRepository = new ImageRepository(Context, _mapper);
        Context.SaveChanges();
    }

    public GamesGlobalDbContext Context { get; }
    public IProductRepository DebtorRepository { get; }
    public IShoppingCartRepository ReceivableRepository { get; }
    public IImageRepository ImageRepository { get; }

    private IMapper ConfigureAutoMapper()
    {
        var configuration = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(ProductProfile).Assembly); });

        return configuration.CreateMapper();
    }

    ~DatabaseFixture()
    {
        Context.Dispose();
    }
}