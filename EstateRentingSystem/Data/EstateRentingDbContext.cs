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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Estate>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Estates)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Estate>()
                .HasOne(e => e.Dealer)
                .WithMany(d => d.Estates)
                .HasForeignKey(e => e.DealerId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .Entity<Dealer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
