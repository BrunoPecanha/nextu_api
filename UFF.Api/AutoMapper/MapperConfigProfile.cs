using AutoMapper;
using UFF.Domain.Dto;
using UFF.Domain.Entity;

namespace WeApi.AutoMapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
