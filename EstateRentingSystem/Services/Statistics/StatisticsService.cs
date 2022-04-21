namespace EstateRentingSystem.Services.Statistics
{
    using System.Linq;
    using EstateRentingSystem.Data;

    public class StatisticsService : IStatisticsService
    {
        private readonly EstateRentingDbContext data;

        public StatisticsService(EstateRentingDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalEstates = this.data.Estates.Count(e => e.IsPublic);
            var totalUsers = this.data.Users.Count();
            var totalRents = this.data.Estates.Count(e => !e.IsAvailable);

            return new StatisticsServiceModel
            {
                TotalEstates = totalEstates,
                TotalUsers = totalUsers,
                TotalRents = totalRents
            };
        }
    }
}
