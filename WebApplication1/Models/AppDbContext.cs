using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Entityes
{
    public class AppDbContext : DbContext
    {
        public DbSet<Settings> Settings { get; set; }
        public DbSet<JsonData> JsonData { get; set; }
        public DbSet<JsonFields> JsonFields { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
