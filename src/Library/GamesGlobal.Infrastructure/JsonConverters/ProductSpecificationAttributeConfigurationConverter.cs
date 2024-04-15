using System.Text.Json;
using System.Text.Json.Serialization;
using GamesGlobal.Enum;
using GamesGlobal.Infrastructure.DataAccess.Entities.ProductSpecificationAttributes;

namespace GamesGlobal.Infrastructure.JsonConverters;

public class
    ProductSpecificationAttributeConfigurationConverter : JsonConverter<
    IProductSpecificationAttributeConfigurationDataModel>
{
    public override IProductSpecificationAttributeConfigurationDataModel? Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions? options)
    {
        using var jsonDoc = JsonDocument.ParseValue(ref reader);
        var root = jsonDoc.RootElement;

        if (!root.TryGetProperty("attributeType", out var typeElement))
            throw new JsonException(
                "Missing discriminator property 'attributeType' on product specification attribute configuration object.");

        var attributeType = (ProductSpecificationAttributeType)typeElement.GetInt32();
        return attributeType switch
        {
            ProductSpecificationAttributeType.Image =>
                JsonSerializer.Deserialize<ImageProductSpecificationAttributeConfigurationDataModel>(root.GetRawText(),
                    options),
            _ => throw new JsonException($"Unknown type discriminator: {attributeType}")
        };
    }

    public override void Write(Utf8JsonWriter writer, IProductSpecificationAttributeConfigurationDataModel value,
        JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}