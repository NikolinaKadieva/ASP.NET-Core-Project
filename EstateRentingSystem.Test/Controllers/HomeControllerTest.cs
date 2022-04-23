namespace EstateRentingSystem.Test.Controllers
{
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Services.Estates.Models;
    using FluentAssertions;
    using MyTested.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using Xunit;

    using static Data.Estates;
    using static WebConstants.Cache;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                .Instance(controller => controller
                    .WithData(TenPublicEstates))
                .Calling(c => c.Index())
                .ShouldHave()
                .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestEstatesCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(10))
                        .WithValueOfType<List<LatestEstateServiceModel>>()))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<LatestEstateServiceModel>>()
                    .Passing(model => model.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();
    }
}