namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Models;

    public class EstateService : IEstateService
    {
        private readonly EstateRentingDbContext data;

        public EstateService(EstateRentingDbContext data) 
            => this.data = data;

        public EstateQueryServiceModel All(
            string type,
            string searchTerm,
            EstateSorting sorting,
            int currentPage,
            int estatesPerPage)
        {
            var estatesQuery = this.data.Estates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                estatesQuery = estatesQuery.Where(e => e.Type == type);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                estatesQuery = estatesQuery.Where(e =>
                e.Type.ToLower().Contains(searchTerm.ToLower()) ||
                e.Description.ToLower().Contains(searchTerm.ToLower()) ||
                e.TypeOfConstruction.ToLower().Contains(searchTerm.ToLower()));
            }

            estatesQuery = sorting switch
            {

                EstateSorting.Year => estatesQuery.OrderByDescending(e => e.YearOfConstruction),
                EstateSorting.Type => estatesQuery.OrderBy(e => e.Type),
                EstateSorting.DateCreated or _ => estatesQuery.OrderByDescending(e => e.Id)
            };

            var totalEstates = estatesQuery.Count();

            var estates = estatesQuery
                .Skip((currentPage - 1) * estatesPerPage)
                .Take(estatesPerPage)
                .Select(e => new EstateServiceModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    TypeOfConstruction = e.TypeOfConstruction,
                    YearOfConstruction = e.YearOfConstruction,
                    Squaring = e.Squaring,
                    ImageUrl = e.ImageUrl,
                    Category = e.Category.Name
                })
                .ToList();

            return new EstateQueryServiceModel
            {
                TotalEstates = totalEstates,
                CurrentPage = currentPage,
                EstatesPerPage = estatesPerPage,
                Estates = estates
            };
        }

        public IEnumerable<string> AllEstateTypes()
            => this.data
                .Estates
                .Select(e => e.Type)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
    }
}
