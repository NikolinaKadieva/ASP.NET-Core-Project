namespace EstateRentingSystem.Models.Home
{
    using System.Collections.Generic;
    public class IndexViewModel
    { 
        public int TotalEstates { get; init; }
        public int TotalUsers { get; init; }
        public int TotalRents { get; init; }

        public List<EstateIndexViewModel> Estates { get; init; }
    }
}