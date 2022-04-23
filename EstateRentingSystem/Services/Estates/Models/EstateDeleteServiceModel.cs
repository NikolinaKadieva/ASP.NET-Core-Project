namespace EstateRentingSystem.Services.Estates.Models
{
    public class EstateDeleteServiceModel : EstateServiceModel
    {
        public string Description { get; init; }

        public int FurnitureId { get; init; }

        public int AnimalId { get; init; }

        public int CategoryId { get; init; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public string UserId { get; init; }
    }
}
