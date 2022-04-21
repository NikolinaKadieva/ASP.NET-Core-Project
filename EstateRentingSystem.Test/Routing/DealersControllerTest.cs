namespace EstateRentingSystem.Test.Routing
{
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Models.Dealers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeDealerShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Dealers/Become")
                .To<DealersController>(e => e.Become());

        [Fact]
        public void PostBecomeDealerShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Dealers/Become")
                    .WithMethod(HttpMethod.Post))
                .To<DealersController>(e => e.Become(With.Any<BecomeDealerFormModel>()));
    }
}
