using Microsoft.EntityFrameworkCore;

namespace AutoMarket.Persistense.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt)
            : base(opt)
        {
        }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarAnnouncement> CarAnnouncements { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarBrand>()
                .Navigation(b => b.Announcements) // To bind CarBrands and CarAnnouncements
                .UsePropertyAccessMode(PropertyAccessMode.Field); // Because of private set in Announcements property.

            modelBuilder.Entity<CarBrand>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<CarAnnouncement>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Price).HasPrecision(18, 2);

                entity.HasOne(a => a.CarBrand)
                      .WithMany(b => b.Announcements)
                      .HasForeignKey(a=> a.CarBrandId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
