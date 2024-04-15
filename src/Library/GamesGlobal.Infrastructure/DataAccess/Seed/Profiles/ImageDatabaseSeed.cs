using GamesGlobal.Infrastructure.DataAccess.Entities;
using GamesGlobal.Infrastructure.DataAccess.Seed.Interfaces;

namespace GamesGlobal.Infrastructure.DataAccess.Seed.Profiles;

public class ImageDatabaseSeed : IDatabaseSeed
{
    // Define a constant static Id for the seeded image
    public static readonly Guid
        SeededImageId = new("12345678-abcd-ef00-0123-0123456789ab"); // Replace with your desired GUID

    public int Order => 1; // Depending on your needs, you can change it to the appropriate order

    public async Task SeedAsync(GamesGlobalDbContext context)
    {
        // Check if images already exist to avoid duplication
        if (await context.Images.FindAsync(SeededImageId) != null)
            return; // If the image is already there, don't add it

        // Create the seed data for a blank image
        var image = new ImageDataModel
        {
            Id = SeededImageId,
            ImageData = new byte[] { },
            ContentType = "image/png"
        };

        // Add the image to the context and save changes
        await context.Images.AddAsync(image);
        await context.SaveChangesAsync();
    }
}