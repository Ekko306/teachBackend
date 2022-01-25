using Microsoft.EntityFrameworkCore;

namespace teachBackend.Models
{
    public class OnlineContext : DbContext
    {
        public OnlineContext(DbContextOptions<OnlineContext> options)
            : base(options)
        {
        }

        public DbSet<Online> Onlines { get; set; }
    }
}