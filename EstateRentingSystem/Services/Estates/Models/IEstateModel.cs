namespace EstateRentingSystem.Services.Estates.Models
{
    public interface IEstateModel
    {
        string Type { get; }

        string TypeOfConstruction { get; }
    }
}