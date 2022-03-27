namespace EstateRentingSystem.Services.Estates
{
    public class EstateServiceModel
    {
        public int Id { get; init; }

        public string Type { get; init; }

        public string TypeOfConstruction { get; init; }

        public int YearOfConstruction { get; init; }

        public int Squaring { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }
    }
}
