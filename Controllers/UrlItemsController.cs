using Microsoft.AspNetCore.Mvc;
using UrlApi.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

[ApiController]
public class ShortenController : ControllerBase
{
    private readonly IMongoCollection<UrlItem> _urlCollection;

    public ShortenController()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("UrlShortenerDb");
        _urlCollection = database.GetCollection<UrlItem>("urls");
    }

    // POST /api/shorten
    [HttpPost("api/shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlItem request)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[6];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);

        var urlItem = new UrlItem
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = finalString
        };

        await _urlCollection.InsertOneAsync(urlItem);

        return Ok($"http://localhost:5009/{finalString}");
    }

    // GET /{shortCode}
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        var urlItem = await _urlCollection.Find(u => u.ShortCode == shortCode).FirstOrDefaultAsync();

        if (urlItem == null)
            return NotFound();
        
        return Redirect(urlItem.OriginalUrl);
    }
}
