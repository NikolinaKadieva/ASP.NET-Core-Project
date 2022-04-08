namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Services.Estates.Models;

    public class EstateService : IEstateService
    {
        private readonly EstateRentingDbContext data;
        private readonly IConfigurationProvider mapper;

        public EstateService(EstateRentingDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper.ConfigurationProvider;
        }

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

            var estates = GetEstates(estatesQuery
                .Skip((currentPage - 1) * estatesPerPage)
                .Take(estatesPerPage));

            return new EstateQueryServiceModel
            {
                TotalEstates = totalEstates,
                CurrentPage = currentPage,
                EstatesPerPage = estatesPerPage,
                Estates = estates
            };
        }

        public EstateDetailsServiceModel Details(int id)
            => this.data
                .Estates
                .Where(e => e.Id == id)
                .ProjectTo<EstateDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public int Create(string type, string typeOfConstruction, string description, int yearOfConstruction, int squaring, string imageUrl, int categoryId, int dealerId)
        {
            var estateData = new Estate
            {
                Type = type,
                TypeOfConstruction = typeOfConstruction,
                Description = description,
                YearOfConstruction = yearOfConstruction,
                Squaring = squaring,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                DealerId = dealerId
            };

            this.data.Estates.Add(estateData);

            this.data.SaveChanges();

            return estateData.Id;
        }

        public bool Edit(int id, string type, string typeOfConstruction, string description, int yearOfConstruction, int squaring, string imageUrl, int categoryId)
        {
            var estateData = this.data.Estates.Find(id);

            if (estateData == null)
            {
                return false;
            }

            estateData.Type = type;
            estateData.TypeOfConstruction = typeOfConstruction;
            estateData.Description = description;
            estateData.YearOfConstruction = yearOfConstruction;
            estateData.Squaring = squaring;
            estateData.ImageUrl = imageUrl;
            estateData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<EstateServiceModel> ByUser(string userId)
            => GetEstates(this.data
                .Estates
                .Where(e => e.Dealer.UserId == userId));

        public bool IsByDealer(int estateId, int dealerId)
            => this.data
                .Estates
                .Any(e => e.Id == estateId && e.DealerId == dealerId);

        public IEnumerable<string> AllTypes()
            => this.data
                .Estates
                .Select(e => e.Type)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

        public IEnumerable<EstateCategoryServiceModel> AllCategories()
            => this.data
              .Categories
              .Select(e => new EstateCategoryServiceModel
              {
                  Id = e.Id,
                  Name = e.Name
              })
              .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(e => e.Id == categoryId);

        private static IEnumerable<EstateServiceModel> GetEstates(IQueryable<Estate> estateQuery)
            => estateQuery
                .Select(e => new EstateServiceModel
                {
                    Id = e.Id,
                    Type = e.Type,
                    TypeOfConstruction = e.TypeOfConstruction,
                    YearOfConstruction = e.YearOfConstruction,
                    Squaring = e.Squaring,
                    ImageUrl = e.ImageUrl,
                    CategoryName = e.Category.Name
                })
                .ToList();
    }
}
