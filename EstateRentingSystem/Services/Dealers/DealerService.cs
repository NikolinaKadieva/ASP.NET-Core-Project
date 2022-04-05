namespace EstateRentingSystem.Services.Dealers
{
    using System.Linq;
    using EstateRentingSystem.Data;

    public class DealerService : IDealerService
    {
        private readonly EstateRentingDbContext data;

        public DealerService(EstateRentingDbContext data) 
            => this.data = data;

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);

        public int IdByUser(string userId)
            => this.data
                .Dealers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();
    }
}
