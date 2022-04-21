namespace EstateRentingSystem.Test.Routing
{
    using EstateRentingSystem.Controllers;
    using MyTested.AspNetCore.Mvc;
    using Xunit;
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/")
                .To<HomeController>(e => e.Index());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Home/Error")
                .To<HomeController>(e => e.Error());
    }
}
