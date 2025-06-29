namespace UrlApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// Defines the properties of a UrlItem, the items that go in the DB
public class UrlItem
{
    // The unique id property of the object
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // The original long url property
    public required string OriginalUrl { get; set; }

    // The generated short code property
    public string? ShortCode { get; set; }

    // The click count property
    public int ClickCount { get; set; }
}