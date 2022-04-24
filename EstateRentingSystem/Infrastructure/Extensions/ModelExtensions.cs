namespace EstateRentingSystem.Infrastructure.Extensions
{
    using EstateRentingSystem.Services.Estates.Models;
    public static class ModelExtensions
    {
        public static string GetInformation(this IEstateModel estate)
            => estate.Type + "-" + estate.TypeOfConstruction;
    }
}