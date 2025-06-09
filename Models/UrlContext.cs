using Microsoft.EntityFrameworkCore;

namespace UrlApi.Models;

public class UrlContext : DbContext
{
    public UrlContext(DbContextOptions<UrlContext> options)
        : base(options)
    {
    }

    public DbSet<UrlItem> UrlItems { get; set; } = null!;
}