using AutoMapper;
using Supply.Domain.Dto;
using Supply.Domain.Entity;

namespace WeApi.Config
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<CostumerDto, Costumer>()
                .ForMember(x => x.Id, m => m.MapFrom(a => a.Id))
                .ForMember(x => x.Name, m => m.MapFrom(a => a.Name))
                .ForMember(x => x.LastName, m => m.MapFrom(a => a.LastName))
                .ForMember(x => x.Phone, m => m.MapFrom(a => a.Phone))
                .ForMember(x => x.Street, m => m.MapFrom(a => a.Street))
                .ForMember(x => x.Number, m => m.MapFrom(a => a.Number))
                .ForMember(x => x.City, m => m.MapFrom(a => a.City))
                .ForMember(x => x.Active, m => m.MapFrom(a => a.Active));

            CreateMap<Costumer, CostumerDto>()
                .ForMember(x => x.Id, m => m.MapFrom(a => a.Id))
                .ForMember(x => x.Name, m => m.MapFrom(a => a.Name))
                .ForMember(x => x.LastName, m => m.MapFrom(a => a.LastName))
                .ForMember(x => x.Phone, m => m.MapFrom(a => a.Phone))
                .ForMember(x => x.Street, m => m.MapFrom(a => a.Street))
                .ForMember(x => x.Number, m => m.MapFrom(a => a.Number))
                .ForMember(x => x.City, m => m.MapFrom(a => a.City))
                .ForMember(x => x.Active, m => m.MapFrom(a => a.Active));
        }
    }
}
