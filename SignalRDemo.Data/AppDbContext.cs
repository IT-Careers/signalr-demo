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
                string connectionString = "Host=ec2-52-18-116-67.eu-west-1.compute.amazonaws.com:5432;database=d9clgq2vgndm7g;username=dkrnsoihsolacv;password=09ca2948c45b87bc150bcc4c0c206c5cebcd4e83c155005a833705885b7412cf";

                optionsBuilder.UseNpgsql(connectionString);
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