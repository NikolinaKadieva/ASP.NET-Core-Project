namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using EstateRentingSystem.Models;
    using EstateRentingSystem.Services.Estates.Models;

    public interface IEstateService
    {
        EstateQueryServiceModel All(
            string type,
            string searchTerm,
            EstateSorting sorting,
            int currentPage,
            int estatesPerPage);

        EstateDetailsServiceModel Details(int estateId);

        int Create(
            string type,
            string typeOfConstruction,
            string description,
            int yearOfConstruction,
            int squaring,
            string imageUrl,
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
            int categoryId);

        IEnumerable<EstateServiceModel> ByUser(string userId);

        bool IsByDealer(int estateId, int dealerId);

        IEnumerable<string> AllTypes();

        IEnumerable<EstateCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}
