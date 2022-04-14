namespace EstateRentingSystem.Models.Home
{
    using System.Collections.Generic;
    using EstateRentingSystem.Services.Estates.Models;
    public class IndexViewModel
    { 
        public int TotalEstates { get; init; }
        public int TotalUsers { get; init; }
        public int TotalRents { get; init; }
        public IList<LatestEstateServiceModel> Estates { get; init; }
    }
}