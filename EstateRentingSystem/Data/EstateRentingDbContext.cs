namespace EstateRentingSystem.Data
{
    using EstateRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    public class EstateRentingDbContext : IdentityDbContext
{
        public EstateRentingDbContext(DbContextOptions<EstateRentingDbContext> options)
            : base(options)
        {
        }
        public DbSet<Estate> Estates { get; init; }

        public DbSet<Category> Categories { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Estate>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Estates)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
