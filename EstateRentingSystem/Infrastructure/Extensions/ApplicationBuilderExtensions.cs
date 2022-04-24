namespace EstateRentingSystem.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static EstateRentingSystem.Areas.Admin.AdminConstants;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            MigrateDatabase(services);

            SeedCategories(services);
            SeedAdministrator(services);

            return app;        
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<EstateRentingDbContext>();

            data.Database.Migrate();
        }

        private static void SeedCategories(IServiceProvider services) 
        {
            var data = services.GetRequiredService<EstateRentingDbContext>();

            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category { Name = "One-Room"},
                new Category { Name = "Two-Rooms"},
                new Category { Name = "Three-Rooms"},
                new Category { Name = "Multi-Room"},
                new Category { Name = "House"},
            });

            data.Furnitures.AddRange(new[]
            {
                new Furniture { Type = "Furniture"},
                new Furniture { Type = "Unfurniture"},
            });

            data.Animals.AddRange(new[]
            {
                new Animal { Type = "Animals are not allowed"},
                new Animal { Type = "Allowed cats"},
                new Animal { Type = "Allowed dogs"},
                new Animal { Type = "Allowed fish"},
                new Animal { Type = "Allowed parrots"},
            });

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@ers.com";
                    const string adminPassword = "adminEstates";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
