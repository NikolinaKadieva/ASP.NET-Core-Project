namespace EstateRentingSystem.Test.Controllers.Api
{
    using EstateRentingSystem.Controllers.Api;
    using EstateRentingSystem.Test.Mocks;
    using Xunit;
    public class StatisticsApiControllerTest
    {
        [Fact]

        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(3, result.TotalEstates);
            Assert.Equal(2, result.TotalRents);
            Assert.Equal(5, result.TotalUsers);
        }
    }
}
