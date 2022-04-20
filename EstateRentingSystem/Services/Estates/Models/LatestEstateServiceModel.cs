namespace EstateRentingSystem.Services.Estates.Models
{
    public class LatestEstateServiceModel : EstateServiceModel
    {
        public int Id { get; init; }

        public string Type { get; init; }

        public string TypeOfConstruction { get; init; }

        public int YearOfConstruction { get; init; }

        public int Squaring { get; init; }

        public string ImageUrl { get; init; }
    }
}