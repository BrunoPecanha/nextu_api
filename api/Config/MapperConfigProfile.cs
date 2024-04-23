using AutoMapper;
using Supply.Domain.Dto;
using Supply.Domain.Entity;

namespace WeApi.Config
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<CostumerDto, Costumer>();
            CreateMap<Costumer, CostumerDto>();
        }
    }
}
