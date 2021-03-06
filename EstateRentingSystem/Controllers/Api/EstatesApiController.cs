namespace EstateRentingSystem.Controllers.Api
{
    using Microsoft.AspNetCore.Mvc;
    using EstateRentingSystem.Models.Api.Estates;
    using EstateRentingSystem.Services.Estates;
    using EstateRentingSystem.Services.Estates.Models;

    [ApiController]
    [Route("api/estates")]
    public class EstatesApiController : ControllerBase
    {
        private readonly IEstateService estates;

        public EstatesApiController(IEstateService estates)
            => this.estates = estates;

        [HttpGet]
        public EstateQueryServiceModel All([FromQuery] AllEstatesApiRequestModel query)
            => this.estates.All(
                query.Type,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.EstatesPerPage);
    }
}