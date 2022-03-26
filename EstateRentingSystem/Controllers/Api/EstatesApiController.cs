namespace EstateRentingSystem.Controllers.Api
{
    using EstateRentingSystem.Data;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/estates")]
    public class EstatesApiController : ControllerBase
    {
        private readonly EstateRentingDbContext data;

        public EstatesApiController(EstateRentingDbContext data)
            => this.data = data;
    }
}
