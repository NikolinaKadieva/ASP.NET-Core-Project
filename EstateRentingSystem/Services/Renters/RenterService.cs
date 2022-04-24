namespace EstateRentingSystem.Services.Renters
{
    using EstateRentingSystem.Data;
    using System.Linq;
    public class RenterService : IRenterService
    {
        private readonly EstateRentingDbContext data;

        public RenterService(EstateRentingDbContext data) 
            => this.data = data;

        public int IdByUser(string userId)
             => this.data
                .Renters
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public bool IsRenter(string userId)
             => this.data
                .Renters
                .Any(d => d.UserId == userId);
    }
}
