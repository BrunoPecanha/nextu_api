using AutoMapper;
using uff.Domain.Dto;
using uff.Domain.Entity;

namespace WeApi.Config
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<CostumerDto, User>();
            CreateMap<User, CostumerDto>();
        }
    }
}
