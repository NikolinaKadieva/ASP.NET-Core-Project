namespace EstateRentingSystem.Services.Estates.Models
{
    using System.Collections.Generic;
    public class EstateQueryServiceModel
    {
        public int CurrentPage { get; init; }

        public int EstatesPerPage { get; init; }

        public int TotalEstates { get; init; }

        public IEnumerable<EstateServiceModel> Estates { get; init; }
    }
}