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
            var totalEstates = this.data.Estates.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalEstates = totalEstates,
                TotalUsers = totalUsers
            };
        }
    }
}
