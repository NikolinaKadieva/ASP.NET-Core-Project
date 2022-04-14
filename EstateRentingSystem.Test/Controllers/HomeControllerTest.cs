namespace EstateRentingSystem.Test.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using EstateRentingSystem.Controllers;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models.Home;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Statistics;
    using EstateRentingSystem.Test.Mocks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(GetEstates()))
                    .ShouldReturn()
                    .View(view => view
                        .WithModelOfType<IndexViewModel>()
                        .Passing(m => m.Estates.Should().HaveCount(3)));

        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            data.Estates.AddRange(Enumerable.Range(0, 10).Select(i => new Estate()));
            data.Users.Add(new User());

            data.SaveChanges();

            var estateService = new EstateService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(estateService, statisticsService);

            var result = homeController.Index();

            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Estates.Count);
            Assert.Equal(10, indexViewModel.TotalEstates);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null, null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        private static IEnumerable<Estate> GetEstates()
            => Enumerable.Range(0, 10).Select(i => new Estate());
    }
}
