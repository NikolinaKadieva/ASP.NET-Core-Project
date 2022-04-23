namespace EstateRentingSystem.Test.Pipeline
{
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models.Renters;
    using EstateRentingSystem.Models.Estates;
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    using static WebConstants;
    public class RentersControllerTest
	{
        [Fact]
        public void GetBecomeRenterShouldBeForAuthorizedUsersAndReturnView()
            => MyPipeline
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Renters/Become")
                    .WithUser())
                .To<RentersController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("Renter", "+359899999999")]
        public void PostBecomeRenterShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string renterName,
            string phoneNumber)
            => MyPipeline
                .Configuration()
                .ShouldMap(requrst => requrst
                    .WithPath("/Renters/Become")
                    .WithMethod(HttpMethod.Post)
                    .WithFormFields(new
                    {
                        Name = renterName,
                        PhoneNumber = phoneNumber
                    })
                    .WithUser()
                    .WithAntiForgeryToken())
                .To<RentersController>(c => c.Become(new BecomeRenterFormModel
                {
                    Name = renterName,
                    PhoneNumber = phoneNumber
                }))
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Renter>(renters => renters
                        .Any(d =>
                            d.Name == renterName &&
                            d.PhoneNumber == phoneNumber &&
                            d.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<EstatesController>(c => c.All(With.Any<AllEstatesQueryModel>())));
    }
}
