namespace EstateRentingSystem.Test.Pipeline
{
    using System.Collections.Generic;
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Services.Estates.Models;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.Estates;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(e => e.Index())
                .Which(controller => controller
                    .WithData(TenPublicEstates))
                    .ShouldReturn()
                    .View(view => view
                        .WithModelOfType<List<LatestEstateServiceModel>>()
                        .Passing(model => model.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                .Pipeline()
                .ShouldMap("/Home/Error")
                .To<HomeController>(e => e.Error())
                .Which()
                .ShouldReturn()
                .View();
    }
}
