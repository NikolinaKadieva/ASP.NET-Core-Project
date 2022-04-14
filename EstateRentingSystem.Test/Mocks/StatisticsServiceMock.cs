namespace EstateRentingSystem.Test.Mocks
{
    using EstateRentingSystem.Services.Statistics;
    using Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get 
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalEstates = 3,
                        TotalRents = 2,
                        TotalUsers = 5
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}