namespace EstateRentingSystem.Infrastructure
{
    using AutoMapper;
    using EstateRentingSystem.Data.Models;
    using EstateRentingSystem.Models.Estates;
    using EstateRentingSystem.Services.Estates.Models;
    
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<EstateDetailsServiceModel, EstateFormModel>();
            this.CreateMap<Estate, LatestEstateServiceModel>();

            this.CreateMap<Estate, EstateDetailsServiceModel>()
                .ForMember(e => e.UserId, configure => configure.MapFrom(e => e.Dealer.UserId));
        }
    }
}
