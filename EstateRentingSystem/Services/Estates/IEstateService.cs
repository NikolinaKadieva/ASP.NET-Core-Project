namespace EstateRentingSystem.Services.Estates
{
    using System.Collections.Generic;
    using EstateRentingSystem.Models;

    public interface IEstateService
    {
        EstateQueryServiceModel All(
            string type,
            string searchTerm,
            EstateSorting sorting,
            int currentPage,
            int estatesPerPage);

        IEnumerable<string> AllEstateTypes();
    }
}
