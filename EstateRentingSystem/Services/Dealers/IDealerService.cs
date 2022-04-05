namespace EstateRentingSystem.Services.Dealers
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int IdByUser(string userId);
    }
}
