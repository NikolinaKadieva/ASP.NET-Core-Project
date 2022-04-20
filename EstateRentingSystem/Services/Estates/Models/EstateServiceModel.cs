namespace EstateRentingSystem.Services.Estates.Models
{
    public class EstateServiceModel : IEstateModel
    {
        public int Id { get; init; }

        public string Type { get; init; }

        public string TypeOfConstruction { get; init; }

        public int YearOfConstruction { get; init; }

        public int Squaring { get; init; }

        public string ImageUrl { get; init; }

        public string FurnitureType { get; init; }

        public string AnimalType { get; init; }

        public string CategoryName { get; init; }

        public bool IsPublic { get; init; }
    }
}
