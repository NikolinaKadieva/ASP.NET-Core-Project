namespace EstateRentingSystem.Test.Services
{
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Services.Dealers;
    using EstateRentingSystem.Test.Mocks;
    using Xunit;

    public class DealerServiceTest
    {
        private const string TestUserId = "IdForTest";

        [Fact]
        public void IsDealerShouldReturnTrueIfUserIsRealyDealer()
        {
            var dealerService = GetDealerService();

            var result = dealerService.IsDealer(TestUserId);

            Assert.True(result);
        }

        [Fact]
        public void IsDealerShouldReturnFalseIfUserIsNotDealer()
        {
            var dealerService = GetDealerService();

            var result = dealerService.IsDealer("WrongUserId");

            Assert.False(result);
        }

        private static IDealerService GetDealerService()
        {
            var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer { UserId = TestUserId });

            data.SaveChanges();

            return new DealerService(data);
        }
    }
}
