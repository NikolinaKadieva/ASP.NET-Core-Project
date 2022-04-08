namespace EstateRentingSystem.Models.Estates
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using EstateRentingSystem.Services.Estates.Models;

    public class AllEstatesQueryModel
    {
        public const int EstatesPerPage = 3;
        public string Type { get; init; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; init;}

        public EstateSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalEstates { get; set; }

        public IEnumerable<string> Types { get; set; }

        public IEnumerable<EstateServiceModel> Estates { get; set; }
    }
}
