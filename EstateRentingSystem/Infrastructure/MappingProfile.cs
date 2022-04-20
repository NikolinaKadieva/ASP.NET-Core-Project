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
            this.CreateMap<Category, EstateCategoryServiceModel>();
            this.CreateMap<Furniture, EstateFurnitureServiceModel>();
            this.CreateMap<Animal, EstateAnimalServiceModel>();

            this.CreateMap<EstateDetailsServiceModel, EstateFormModel>();
            this.CreateMap<Estate, LatestEstateServiceModel>();

            this.CreateMap<Estate, EstateServiceModel>()
                .ForMember(e => e.CategoryName, configure => configure.MapFrom(e => e.Category.Name))
                .ForMember(e => e.AnimalType, configure => configure.MapFrom(e => e.Animal.Type))
                .ForMember(e => e.FurnitureType, configure => configure.MapFrom(e => e.Furniture.Type));

            this.CreateMap<Estate, EstateDetailsServiceModel>()
                .ForMember(e => e.UserId, configure => configure.MapFrom(e => e.Dealer.UserId))
                .ForMember(e => e.CategoryName, configure => configure.MapFrom(e => e.Category.Name))
                .ForMember(e => e.AnimalType, configure => configure.MapFrom(e => e.Animal.Type))
                .ForMember(e => e.FurnitureType, configure => configure.MapFrom(e => e.Furniture.Type));
        }
    }
}
