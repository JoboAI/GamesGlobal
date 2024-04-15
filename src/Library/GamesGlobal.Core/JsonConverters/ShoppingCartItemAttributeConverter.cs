using System.Text.Json;
using System.Text.Json.Serialization;
using GamesGlobal.Core.Entities.ShoppingCartItemAttribute;
using GamesGlobal.Enum;

namespace GamesGlobal.Core.JsonConverters;

public class ShoppingCartItemAttributeConverter : JsonConverter<IShoppingCartItemAttributeDomainModel>
{
    public override IShoppingCartItemAttributeDomainModel? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("attributeType", out var typeElement))
            throw new JsonException(
                "Missing discriminator property 'attributeType' on shopping cart item attribute value object.");

        var attributeType = (ProductSpecificationAttributeType)typeElement.GetInt32();

        return attributeType switch
        {
            ProductSpecificationAttributeType.Image => JsonSerializer
                .Deserialize<ImageShoppingCartItemAttributeDomainModel>(root.GetRawText(), options),
            _ => throw new JsonException($"Unknown type discriminator: {attributeType}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IShoppingCartItemAttributeDomainModel value,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}