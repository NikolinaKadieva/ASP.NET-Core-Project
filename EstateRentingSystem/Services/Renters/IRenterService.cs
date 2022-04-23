namespace EstateRentingSystem.Services.Renters
{
    public interface IRenterService
    {
        public bool IsRenter(string userId);

        public int IdByUser(string userId);
    }
}
