using Microsoft.EntityFrameworkCore;
using SignalRDemo.Data.Entities;

namespace SignalRDemo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        
        public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=signalr-demo;user=root;password=1234;port=3306"
                    , ServerVersion.AutoDetect("server=localhost;database=signalr-demo;user=root;password=1234;port=3306"));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}