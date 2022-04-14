namespace EstateRentingSystem.Data
{
    using EstateRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class EstateRentingDbContext : IdentityDbContext<User>
{
        public EstateRentingDbContext(DbContextOptions<EstateRentingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Estate> Estates { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Dealer> Dealers { get; init; }

        public DbSet<Furniture> Furnitures { get; init; }

        public DbSet<Animal> Animals { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Estate>()
                .HasOne(c => c.Category)
                .WithMany(e => e.Estates)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Estate>()
                .HasOne(d => d.Dealer)
                .WithMany(e => e.Estates)
                .HasForeignKey(d => d.DealerId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Estate>()
                .HasOne(a => a.Animal)
                .WithMany(e => e.Estates)
                .HasForeignKey(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Estate>()
               .HasOne(f => f.Furniture)
               .WithMany(e => e.Estates)
               .HasForeignKey(f => f.FurnitureId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
