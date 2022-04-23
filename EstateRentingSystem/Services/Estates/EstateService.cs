namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using EstateRentingSystem.Data;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Models.Renters;
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
            string type = null,
            string searchTerm = null,
            EstateSorting sorting = EstateSorting.DateCreated,
            int currentPage = 1,
            int estatesPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var estatesQuery = this.data.Estates
                .Where(e => publicOnly ? e.IsPublic : true);

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

        public IEnumerable<LatestEstateServiceModel> Latest()
           => this.data
               .Estates
               .Where(e => e.IsPublic)
               .OrderByDescending(e => e.Id)
               .ProjectTo<LatestEstateServiceModel>(this.mapper)
               .Take(3)
               .ToList();

        public EstateDetailsServiceModel Details(int id)
            => this.data
                .Estates
                .Where(e => e.Id == id)
                .ProjectTo<EstateDetailsServiceModel>(this.mapper)
                .FirstOrDefault();

        public EstateDeleteServiceModel Delete(int id)
           => this.data
               .Estates
               .Where(e => e.Id == id)
               .ProjectTo<EstateDeleteServiceModel>(this.mapper)
               .FirstOrDefault();

        public void DeleteConfirmed(int id)
        {
            var estate = this.data.Estates.Find(id);
            this.data.Estates.Remove(estate);
            this.data.SaveChanges();
        }

        public int Create(
            string type, 
            string typeOfConstruction, 
            string description,
            int yearOfConstruction, 
            int squaring,
            string imageUrl, 
            int price,
            int furnitureId,
            int animalId, 
            int categoryId, 
            int dealerId)
        {
            var estateData = new Estate
            {
                Type = type,
                TypeOfConstruction = typeOfConstruction,
                Description = description,
                YearOfConstruction = yearOfConstruction,
                Squaring = squaring,
                ImageUrl = imageUrl,
                Price = price,
                IsAvailable = true,
                FurnitureId = furnitureId,
                AnimalId = animalId,
                CategoryId = categoryId,
                DealerId = dealerId,
                IsPublic = false
            };

            this.data.Estates.Add(estateData);

            this.data.SaveChanges();

            return estateData.Id;
        }

        public bool Edit(
            int id,
            string type, 
            string typeOfConstruction,
            string description,
            int yearOfConstruction, 
            int squaring, 
            string imageUrl,
            int price,
            int furnitureId,
            int animalId, 
            int categoryId,
            bool isPublic)
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
            estateData.Price = price;
            estateData.FurnitureId = furnitureId;
            estateData.AnimalId = animalId;
            estateData.CategoryId = categoryId;
            estateData.IsPublic = isPublic;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<EstateServiceModel> ByUser(string userId)
            => GetEstates(this.data
                .Estates
                .Where(e => e.Dealer.UserId == userId));

        public IEnumerable<EstateServiceModel> ByRenter(string userId)
            => GetEstates(this.data
                 .Estates
                .Where(e => e.Renter.UserId == userId));

        public bool IsByDealer(int estateId, int dealerId)
            => this.data
                .Estates
                .Any(e => e.Id == estateId && e.DealerId == dealerId);

        public void ChangeVisibility(int estateId)
        {
            var estate = this.data.Estates.Find(estateId);

            estate.IsPublic = !estate.IsPublic;

            this.data.SaveChanges();
        }

        public void ChangeAvailability(int estateId)
        {
            var estate = this.data.Estates.Find(estateId);
            var renterId = this.data.Renters.Select(r => r.Id).FirstOrDefault();
            estate.RenterId = renterId;
            estate.IsAvailable = false;

            this.data.SaveChanges();
        }

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
              .ProjectTo<EstateCategoryServiceModel>(this.mapper)
              .ToList();

        public IEnumerable<EstateFurnitureServiceModel> AllFurnitures()
           => this.data
             .Furnitures
             .Select(f => new EstateFurnitureServiceModel
             {
                 Id = f.Id,
                 Type = f.Type
             })
             .ToList();

        public IEnumerable<EstateAnimalServiceModel> AllAnimals()
           => this.data
             .Animals
             .Select(a => new EstateAnimalServiceModel
             {
                 Id = a.Id,
                 Type = a.Type
             })
             .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(e => e.Id == categoryId);

        public bool AnimalExists(int animalId)
            => this.data
                .Animals
                .Any(a => a.Id == animalId);

        public bool FurnitureExists(int furnitureId)
            => this.data
                .Furnitures
                .Any(f => f.Id == furnitureId);

        private IEnumerable<EstateServiceModel> GetEstates(IQueryable<Estate> estateQuery)
            => estateQuery
                .ProjectTo<EstateServiceModel>(this.mapper)
                .ToList();
    }
}
