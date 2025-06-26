namespace UrlApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class UrlItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string OriginalUrl { get; set; }

    public string? ShortCode { get; set; }

    public int ClickCount { get; set; }
}