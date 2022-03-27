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
                string connectionString = "server=eu-cdbr-west-02.cleardb.net;database=heroku_db22767af67ab79;user=b08e20a37c03a7;password=cb1a7f31;port=3306";

                optionsBuilder.UseMySql(connectionString
                    , ServerVersion.AutoDetect(connectionString));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
                .Property(u => u.Id)
                .HasMaxLength(128);

            builder.Entity<AppUser>()
                .Property(u => u.Username)
                .HasMaxLength(128);

            builder.Entity<AppUser>()
                .Property(u => u.Password)
                .HasMaxLength(128);

            builder.Entity<Message>()
                .Property(m => m.Id)
                .HasMaxLength(128);

            builder.Entity<Message>()
                .Property(m => m.Content)
                .HasMaxLength(128);

            builder.Entity<Message>()
                .Property(m => m.UserId)
                .HasMaxLength(128);

            base.OnModelCreating(builder);
        }
    }
}