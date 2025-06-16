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
        var client = new MongoClient("mongodb://mongo:27017");
        var database = client.GetDatabase("UrlShortenerDb");
        _urlCollection = database.GetCollection<UrlItem>("urls");
    }

    // POST /api/shorten
    [HttpPost("api/shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlItem request)
    {
        
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
        return BadRequest("OriginalUrl cannot be empty.");

        // Return existing short code if URL already shortened
        var existing = await _urlCollection.Find(u => u.OriginalUrl == request.OriginalUrl).FirstOrDefaultAsync();
        if (existing != null)
            return Ok($"http://localhost:5009/{existing.ShortCode}");

        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        string shortCode;

        // Generate a unique short code
        do
        {
            shortCode = new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
        while (await _urlCollection.Find(u => u.ShortCode == shortCode).AnyAsync());

        var urlItem = new UrlItem
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode
        };

        await _urlCollection.InsertOneAsync(urlItem);

        return Ok($"http://localhost:5009/{shortCode}");
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
