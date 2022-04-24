namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Services.Estates.Models;
    public interface IEstateService
    {
        EstateQueryServiceModel All(
            string type = null,
            string searchTerm = null,
            EstateSorting sorting = EstateSorting.DateCreated,
            int currentPage = 1,
            int estatesPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<LatestEstateServiceModel> Latest();
        EstateDetailsServiceModel Details(int estateId);
        EstateDeleteServiceModel Delete(int estateId);

        public void DeleteConfirmed(int id);

        int Create(
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
            int dealerId);

        bool Edit(
            int estateId,
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
            bool isPublic);

        IEnumerable<EstateServiceModel> ByUser(string userId);
        IEnumerable<EstateServiceModel> ByRenter(string userId);

        bool IsByDealer(int estateId, int dealerId);

        void ChangeVisibility(int estateId);

        void ChangeAvailability(int estateId);

        IEnumerable<string> AllTypes();

        IEnumerable<EstateCategoryServiceModel> AllCategories();
        IEnumerable<EstateFurnitureServiceModel> AllFurnitures();
        IEnumerable<EstateAnimalServiceModel> AllAnimals();

        bool CategoryExists(int categoryId);
        bool FurnitureExists(int furnitureId);
        bool AnimalExists(int animalId);
    }
}
