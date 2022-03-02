namespace EstateRentingSystem.Infrastructure
{
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<EstateRentingDbContext>();

            data.Database.Migrate();

            SeedCategories(data);

            return app;        
        }

        private static void SeedCategories(EstateRentingDbContext data) 
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "One-Room"},
                new Category { Name = "Two-Rooms"},
                new Category { Name = "Three-Rooms"},
                new Category { Name = "Four-Rooms"},
                new Category { Name = "House"},
            });

            data.SaveChanges();
        }
    }
}
