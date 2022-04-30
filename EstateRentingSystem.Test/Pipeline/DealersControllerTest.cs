namespace EstateRentingSystem.Test.Pipeline
{
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models.Dealers;
    using EstateRentingSystem.Models.Estates;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static WebConstants;
    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeDealerShouldBeForAuthorizedUsersAndReturnView()
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Become")
                    .WithUser())
                .To<DealersController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Dealer", "+359888123456")]
        public void PostBecomeDealerShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string dealerName,
            string phoneNumber)
            => MyPipeline
                .Configuration()
                .ShouldMap(requrst => requrst
                    .WithPath("/Dealers/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    { 
                        Name = dealerName,
                        PhoneNumber = phoneNumber
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<DealersController>(c => c.Become(new BecomeDealerFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Dealer>(dealers => dealers
                        .Any(d =>
                            d.Name == dealerName &&
                            d.PhoneNumber == phoneNumber &&
                            d.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<EstatesController>(c => c.Add(With.Any<EstateFormModel>())));
    }
}