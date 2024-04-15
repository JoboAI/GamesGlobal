using GamesGlobal.Infrastructure.DataAccess;
using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.IntegrationTest.TestData.Interfaces;

namespace GamesGlobal.IntegrationTest.TestData.Profiles;

public class ImageDatabaseSeed : ITestDataSeed
{
    public static readonly Guid SeededImageId = new("12345678-abcd-ef00-0123-0123456789ab");

    public int Order => 1;

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        if (await context.Images.FindAsync(SeededImageId) != null)
            return;

        var image = new ImageDataModel
        {
            Id = SeededImageId,
            ImageData = new byte[] { 1, 2, 3, 4 },
            ContentType = "image/png"
        };

        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
    }
}