using AutoMapper;
using uff.Domain.Dto;
using uff.Domain.Entity;

namespace WeApi.AutoMapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>();
        }
    }
}
