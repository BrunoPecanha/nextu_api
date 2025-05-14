using AutoMapper;
using System;
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

            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.ImgPath, opt => opt.MapFrom(src => src.ImgPath))
                  .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<CustomerInQueueForEmployeeDto, Customer>();
            CreateMap<Customer, CustomerInQueueForEmployeeDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(" ", src.User.Name, src.User.LastName)))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => string.Join(", ", src.CustomerServices.Select(o => o.Service.Name).ToList())))
                  .ForMember(dest => dest.TimeGotInQueue, opt => opt.MapFrom(src => src.TimeEnteredQueue.ToString("HH:mm")))
                  .ForMember(dest => dest.TimeCalledInQueue, opt => opt.MapFrom(src => src.TimeCalledInQueue.HasValue ? src.TimeCalledInQueue.Value.ToString("HH:mm") : null))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.InService, opt => opt.MapFrom(src => src.Status == CustomerStatusEnum.InService));

            CreateMap<CustomerInQueueCardDto, Customer>();
            CreateMap<Customer, CustomerInQueueCardDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.ServiceQtd, opt => opt.MapFrom(src => src.CustomerServices.Count()))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.StoreIcon, opt => opt.MapFrom(src => src.Queue.Store.Category.Icon))
                  .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => src.Queue.Store.LogoPath));

            CreateMap<CustomerInQueueCardDetailsDto, Customer>();
            CreateMap<Customer, CustomerInQueueCardDetailsDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.AttendantsName, opt => opt.MapFrom(src => src.Queue.Employee.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => new PaymentDto(src.Payment.Name, src.Payment.Icon, src.Payment.Notes)))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.CustomerServices.Select(o => new ServiceDto(o.Service.Name, o.Service.Category.Icon, o.Service.Price))))
                  .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CustomerServices.Sum(o => o.Service.Price)));

            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))                  
                  .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                  .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                  .ForMember(dest => dest.ImgPath, opt => opt.MapFrom(src => src.ImgPath))
                  .ForMember(dest => dest.Activated, opt => opt.MapFrom(src => src.Activated))
                  .ForMember(dest => dest.VariableTime, opt => opt.MapFrom(src => src.VariableTime))
                  .ForMember(dest => dest.VariablePrice, opt => opt.MapFrom(src => src.VariablePrice))
                  .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon));

            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.RegisteringDate))
                .ForMember(dest => dest.Whatsapp, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
                .ForMember(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<HighLightDto, HighLight>();
            CreateMap<HighLight, HighLightDto>();

            CreateMap<OpeningHoursDto, OpeningHours>();
            CreateMap<OpeningHours, OpeningHoursDto>()
                .ForMember(dest => dest.WeekDay, opt => opt.MapFrom(src => src.WeekDay))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));                

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<QueueDto, Queue>();
            CreateMap<Queue, QueueDto>()
                        .ForMember(dest => dest.CurrentCount, opt => opt.MapFrom(src =>
                                                     (src.Status == QueueStatusEnum.Open || src.Status == QueueStatusEnum.Paused)
                                                         ? src.QueueCustomers.Count(x => x.Customer.Status == CustomerStatusEnum.Waiting || x.Customer.Status == CustomerStatusEnum.InService)
                                                         : src.QueueCustomers.Count(x => x.Customer.Status == CustomerStatusEnum.Absent || x.Customer.Status == CustomerStatusEnum.Removed || x.Customer.Status == CustomerStatusEnum.Done)));

            CreateMap<StoreProfessionalsDto, Store>();
            CreateMap<Store, StoreProfessionalsDto>()
                .ForMember(dest => dest.StoreLogoPath, opt => opt.MapFrom(src => src.LogoPath))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Subtitle, opt => opt.MapFrom(src => src.StoreSubtitle))
                .ForMember(dest => dest.Professionals, opt => opt.MapFrom(src => src.EmployeeStore.Select(es => es.Employee)));

            CreateMap<ProfessionalDto, User>();
            CreateMap<User, ProfessionalDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Subtitle, opt => opt.MapFrom(src => src.Subtitle))
                .ForMember(dest => dest.ServicesProvided, opt => opt.MapFrom(src => src.ServicesProvided))
                .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => true));
        }
    }
}