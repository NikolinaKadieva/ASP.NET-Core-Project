namespace EstateRentingSystem.Models.Api.Estates
{
    using EstateRentingSystem.Models;

    public class AllEstatesApiRequestModel
    {
        public string Type { get; init; }

        public string SearchTerm { get; init; }

        public EstateSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int EstatesPerPage { get; init; } = 10;
    }
}
