using System.Text;
using System.Text.Json;
using GamesGlobal.Web.Infrastructure.Dtos;

namespace GamesGlobal.IntegrationTest.Extensions;

public static class TestSetupExtensions
{
    public static async Task WithCartItemAsync(this HttpClient client, Guid shoppingCartId,
        ShoppingCartItemAddDto addItemDto, JsonSerializerOptions? jsonSerializerOptions)
    {
        var json = JsonSerializer.Serialize(addItemDto, jsonSerializerOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Post the item to the shopping cart
        var response = await client.PostAsync($"api/v1/shopping-cart/{shoppingCartId}/items", content);

        response.EnsureSuccessStatusCode();
    }
}