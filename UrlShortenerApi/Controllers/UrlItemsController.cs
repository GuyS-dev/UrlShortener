using Microsoft.AspNetCore.Mvc;
using UrlApi.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

[ApiController]
public class ShortenController : ControllerBase
{
    private readonly IMongoCollection<UrlItem> _urlCollection;

    // Creates the mongoDB database on default port 27017 and by the name "UrlShortenerDb" with a collection named "urls"
    public ShortenController()
    {
        var client = new MongoClient("mongodb://mongo:27017");
        var database = client.GetDatabase("UrlShortenerDb");
        _urlCollection = database.GetCollection<UrlItem>("urls");
    }


    /// <summary>
    /// This endpoint gets a http link and shortens it using a 6 characters unique code.
    /// <br/>
    /// POST: /api/shorten
    /// </summary>
    /// <param name="request">A UrlItem containing the original url.</param>
    /// <returns>The shortened link that ends with the unique code, or an error if the input is empty.</returns>
    [HttpPost("api/shorten")]
    public async Task<IActionResult> ShortenUrl([FromBody] UrlItem request)
    {
        // Return en error if en empty string is entered instead of a valid link
        if (string.IsNullOrWhiteSpace(request.OriginalUrl))
            return BadRequest("OriginalUrl cannot be empty.");

        // Return existing short code url if it was already shortened
        var existing = await _urlCollection.Find(u => u.OriginalUrl == request.OriginalUrl).FirstOrDefaultAsync();
        if (existing != null)
            return Ok($"http://localhost:5009/{existing.ShortCode}");

        // A string containing all capital letters, small letters and digits, to get random char from it
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        string shortCode;

        // Generate a unique 6 characters short code
        do
        {
            shortCode = new string(Enumerable.Range(0, 6).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }
        while (await _urlCollection.Find(u => u.ShortCode == shortCode).AnyAsync());

        // Creates a new UrlItem item with the long original url, the 6 characters code, and a click counter set to 0
        var urlItem = new UrlItem
        {
            OriginalUrl = request.OriginalUrl,
            ShortCode = shortCode,
            ClickCount = 0
        };

        // Inserts the new item to the Mongo database
        await _urlCollection.InsertOneAsync(urlItem);

        // Returns the new generated shortened url
        return Ok($"http://localhost:5009/{shortCode}");
    }

    
    /// <summary>
    /// This endpoint gets a shortened link with existing code and redirects to the original long url.
    /// <br/>
    /// GET: /{shortCode}
    /// </summary>
    /// <param name="shortCode">The short code that is being accessed.</param>
    /// <returns>A redirect or an error if the code doesn't exist.</returns>
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectToOriginal(string shortCode)
    {
        // A MongoDB query for finding an item with this short code
        var filter = Builders<UrlItem>.Filter.Eq(u => u.ShortCode, shortCode);
        // A mongoDB update for the "ClickCount" field to increase it by 1 every time its accessed
        var update = Builders<UrlItem>.Update.Inc(u => u.ClickCount, 1);

        var urlItem = await _urlCollection.FindOneAndUpdateAsync(filter, update);

        // Checks if no such DB item was found for that short code
        if (urlItem == null)
        {
            // Return an error code saying that short code doesn't exist and therefore doesn't lead to any long url
            return NotFound(new
            {
                error = "Short code not found",
                shortCode = shortCode,
                status = 404
            });
        }

        // If a matching item is found, it redirects to that item's original long url
        return Redirect(urlItem.OriginalUrl);
    }


    /// <summary>
    /// This endpoint gets a short code and returns the number of times it was accessed (click count).
    /// <br/>
    /// GET: /api/stats/{short_code}
    /// </summary>
    /// <param name="short_code">The short code you ask the stats for.</param>
    /// <returns>The click count of that shortened url.</returns>
    [HttpGet("/api/stats/{short_code}")]
    public async Task<IActionResult> ClicksCounter(string short_code)
    {
        // Looking for the DB item that contains that short code
        var urlItem = await _urlCollection.Find(u => u.ShortCode == short_code).FirstOrDefaultAsync();

        // Checks if no such DB item was found for that short code
        if (urlItem == null)
        {
            // Return an error code saying that short code doesn't exist and therefore doesn't lead to any long url
            return NotFound(new
            {
                error = "Short code not found",
                short_code = short_code,
                status = 404
            });
        }

        // Returns the click count of the shortened link if the short code exists
        return Ok(urlItem.ClickCount);
    }
}
