using AtHome.Shared.Models;

namespace AtHome.WebApi.Database;

public class ApplicationDbContext: DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Place> Places { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}