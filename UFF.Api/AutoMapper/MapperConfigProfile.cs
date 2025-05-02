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

            CreateMap<CustomerInQueueForEmployeeDto, Customer>();
            CreateMap<Customer, CustomerInQueueForEmployeeDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(" ", src.User.Name, src.User.LastName)))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => string.Join(", ", src.CustomerServices.Select(o => o.Service.Name).ToList())))
                  .ForMember(dest => dest.TimeGotInQueue, opt => opt.MapFrom(src => src.TimeEnteredQueue.ToString("HH:mm")))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.InService, opt => opt.MapFrom(src => src.Status == CustomerStatusEnum.InService));

            CreateMap<CustomerInQueueReducedDto, Customer>();
            CreateMap<Customer, CustomerInQueueReducedDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(" ", src.User.Name, src.User.LastName)))
                  .ForMember(dest => dest.ServiceQtd, opt => opt.MapFrom(src => src.CustomerServices.Count()))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => src.Queue.Store.LogoPath));

            CreateMap<CustomerInQueueComplementDto, Customer>();
            CreateMap<Customer, CustomerInQueueComplementDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => new PaymentDto(src.Payment.Name, src.Payment.Icon, src.Payment.Notes)))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.CustomerServices.Select(o => new ServiceDto(o.Service.Name, o.Service.Category.Icon, o.Service.Price))));

            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>()
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon))
                  .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<QueueDto, Queue>();
            CreateMap<Queue, QueueDto>()
                        .ForMember(dest => dest.CurrentCount, opt => opt.MapFrom(src =>
                                                     (src.Status == QueueStatusEnum.Open || src.Status == QueueStatusEnum.Paused)
                                                         ? src.QueueCustomers.Count(x => x.Customer.Status == CustomerStatusEnum.Waiting || x.Customer.Status == CustomerStatusEnum.InService)
                                                         : src.QueueCustomers.Count(x => x.Customer.Status == CustomerStatusEnum.Absent || x.Customer.Status == CustomerStatusEnum.Removed || x.Customer.Status == CustomerStatusEnum.Done)));
        }
    }
}
