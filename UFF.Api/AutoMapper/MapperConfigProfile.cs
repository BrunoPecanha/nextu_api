using AutoMapper;
using System.Linq;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Enum;

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

            CreateMap<QueueDto, Queue>();
            CreateMap<Queue, QueueDto>()
                .ForMember(dest => dest.CurrentCount, opt => opt.MapFrom(src => src.Status == QueueStatusEnum.Open ? src.QueueCustomers.Where(x => x.Customer.Status == CustomerStatusEnum.Waiting || x.Customer.Status == CustomerStatusEnum.InService).Count() 
                                                                                                                   : src.QueueCustomers.Where(x => x.Customer.Status == CustomerStatusEnum.Absent || x.Customer.Status == CustomerStatusEnum.Removed || x.Customer.Status == CustomerStatusEnum.Done).Count()));
        }
    }
}
