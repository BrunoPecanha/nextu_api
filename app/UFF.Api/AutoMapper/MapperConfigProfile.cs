﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using UFF.Domain.Commands.Queue;
using UFF.Domain.Dto;
using UFF.Domain.Entity;
using UFF.Domain.Enum;

namespace WeApi.AutoMapper
{
    public class MapperConfigProfile : Profile
    {
        public MapperConfigProfile()
        {
            CreateMap<User, UserDto>()
                  .ForMember(dest => dest.AcceptAwaysMinorQueue, opt => opt.MapFrom(src => src.AcceptAwaysMinorQueue))
                  .ForMember(dest => dest.DDD, opt => opt.MapFrom(src => src.Phone.Substring(0, 2)))
                  .ForMember(dest => dest.LooseCustomer, opt => opt.MapFrom(src => src.LooseCustomer))
                  .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone.Substring(2)));

            CreateMap<StoreRatingDto, StoreRating>();
            CreateMap<StoreRating, StoreRatingDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                  .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store.Name))
                  .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.Score));

            CreateMap<NotificationDto, Notification>();
            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<ServiceCategoryDto, ServiceCategory>();
            CreateMap<ServiceCategory, ServiceCategoryDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForMember(dest => dest.ImgPath, opt => opt.MapFrom(src => src.ImgPath))
                  .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<FavoriteProfessionalDto, FavoriteProfessional>();
            CreateMap<FavoriteProfessional, FavoriteProfessionalDto>()
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
                  .ForMember(dest => dest.Professional, opt => opt.MapFrom(src => src.Professional.Name))
                  .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => src.Professional.Favorites.Any(x => x.UserId == src.UserId)));

            CreateMap<FavoriteStoreDto, FavoriteStore>();
            CreateMap<FavoriteStore, FavoriteStoreDto>()
                  .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User.Name))
                  .ForMember(dest => dest.Store, opt => opt.MapFrom(src => src.Store.Name))
                  .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => src.Store.Favorites.Any(x => x.UserId == src.UserId)));

            CreateMap<CustomerInQueueForEmployeeDto, Customer>();
            CreateMap<Customer, CustomerInQueueForEmployeeDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.RandomCustomerName) ? string.Join(" ", src.User.Name, src.User.LastName) : src.RandomCustomerName))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Items))
                  .ForMember(dest => dest.TimeGotInQueue, opt => opt.MapFrom(src => src.TimeEnteredQueue.ToLocalTime().ToString("HH:mm")))
                  .ForMember(dest => dest.TimeCalledInQueue, opt => opt.MapFrom(src => src.TimeCalledInQueue.HasValue ? src.TimeCalledInQueue.Value.ToLocalTime().ToString("HH:mm") : null))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                  .ForMember(dest => dest.IsPaused, opt => opt.MapFrom(src => src.Queue.Status == QueueStatusEnum.Paused))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.PricePending, opt => opt.MapFrom(src => src.Items.Any(x => x.FinalPrice == default)))
                  .ForMember(dest => dest.CanEditName, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.RandomCustomerName)))
                  .ForMember(dest => dest.InService, opt => opt.MapFrom(src => src.Status == CustomerStatusEnum.InService));

            CreateMap<CustomerInQueueCardDto, Customer>();
            CreateMap<Customer, CustomerInQueueCardDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.ServiceQtd, opt => opt.MapFrom(src => src.Items.Count()))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment.Name))
                  .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                  .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.Queue.Store.Id))
                  .ForMember(dest => dest.IsPaused, opt => opt.MapFrom(src => src.Queue.Status == QueueStatusEnum.Paused))
                  .ForMember(dest => dest.StoreIcon, opt => opt.MapFrom(src => src.Queue.Store.Category.Icon))
                  .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => src.Queue.Store.LogoPath));

            CreateMap<CustomerInQueueCardDetailsDto, Customer>();
            CreateMap<Customer, CustomerInQueueCardDetailsDto>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.AttendantsName, opt => opt.MapFrom(src => src.EmployeeAttendant.Name))
                  .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                  .ForMember(dest => dest.TimeCalledInQueue, opt => opt.MapFrom(src => src.TimeCalledInQueue.HasValue ? src.TimeCalledInQueue.Value.ToLocalTime().ToString("HH:mm") : null))
                  .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => new PaymentDto(src.Payment.Name, src.Payment.Icon, src.Payment.Notes)))
                  .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Items))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                  .ForMember(dest => dest.EstimatedWaitingTime, opt => opt.MapFrom(x => x.EstimatedWaitingTime))
                  .ForMember(dest => dest.Total, opt => opt.MapFrom(src =>
                                                                    src.Items
                                                                        .Where(o =>
                                                                            (!o.Service.VariablePrice) || (o.Service.VariablePrice && o.FinalPrice > 0))
                                                                        .Sum(o =>
                                                                            o.Service.VariablePrice ? o.FinalPrice * o.Quantity : o.Service.Price * o.Quantity)));
            CreateMap<PaymentDto, Payment>();
            CreateMap<Payment, PaymentDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<EmployeeStore, EmployeeStoreItemDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => string.Join(" ", src.Employee.Name, src.Employee.LastName)))
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                .ForMember(dest => dest.InviteIsPending, opt => opt.MapFrom(src => !src.IsActive && !src.RequestAnswered))
                .ForMember(dest => dest.AssociatedSince, opt => opt.MapFrom(src =>
                    src.IsActive && src.RequestAnswered ? src.LastUpdate.ToString("dd/MM/yyyy") : src.RegisteringDate.ToString("dd/MM/yyyy")));

            CreateMap<EmployeeStoreItemDto, EmployeeStore>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.StoreId, opt => opt.MapFrom(src => src.StoreId))
                .ForMember(dest => dest.RegisteringDate, opt => opt.Ignore())
                .ForMember(dest => dest.LastUpdate, opt => opt.Ignore());

            CreateMap<List<EmployeeStore>, EmployeeStoreDto>()
                .ForMember(dest => dest.EmployeeStoreAssociations, opt => opt.MapFrom(src => src));

            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CustomerDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => string.Join(" ", src.User.Name, src.User.LastName)))
                 .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                 .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.Payment.Id))
                 .ForMember(dest => dest.EmployeeAttedandtId, opt => opt.MapFrom(src => src.EmployeeAttendantId))
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.Name))
                 .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                 .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                 .ForMember(dest => dest.ProcessedAt, opt => opt.MapFrom(src => src.ProcessedAt))
                 .ForMember(dest => dest.ProcessedByName, opt => opt.MapFrom(src => src.ProcessedBy.Name))
                 .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Items))
                 .ForMember(dest => dest.ProcessedBy, opt => opt.MapFrom(src => src.ProcessedBy.Id))
                 .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
                 .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Items
                                                                                .Where(o => (!o.Service.VariablePrice) || (o.Service.VariablePrice && o.FinalPrice > 0))
                                                                                .Sum(o => o.Service.VariablePrice ? o.FinalPrice * o.Quantity : o.Service.Price * o.Quantity)));
            CreateMap<CustomerServiceDto, CustomerService>();
            CreateMap<CustomerService, CustomerServiceDto>()
                 .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                 .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.QueueId))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
                 .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                 .ForMember(dest => dest.VariablePrice, opt => opt.MapFrom(src => src.Service.VariablePrice))
                 .ForMember(dest => dest.VariableTime, opt => opt.MapFrom(src => src.Service.VariableTime))
                 .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Service.Category.Icon))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price))
                 .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
                 .ForMember(dest => dest.FinalDuration, opt => opt.MapFrom(src => src.Duration.TotalMinutes));

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
                  .ForMember(dest => dest.ImageHash, opt => opt.MapFrom(src => src.ImageHash))
                  .ForMember(dest => dest.VariablePrice, opt => opt.MapFrom(src => src.VariablePrice))
                  .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon));
            
            CreateMap<StoreDto, Store>();
            CreateMap<Store, StoreDto>()
                .ForMember(dest => dest.StartServiceWithQRCode, opt => opt.MapFrom(src => src.StartServiceWithQRCode))
                .ForMember(dest => dest.EndServiceWithQRCode, opt => opt.MapFrom(src => src.EndServiceWithQRCode))
                .ForMember(dest => dest.ReleaseOrdersBeforeGetsQueued, opt => opt.MapFrom(src => src.ReleaseOrderBeforeGetsQueued))
                .ForMember(dest => dest.ShareQueue, opt => opt.MapFrom(src => src.ShareQueue))
                .ForMember(dest => dest.InCaseFailureAcceptFinishWithoutQRCode, opt => opt.MapFrom(src => src.InCaseFailureAcceptFinishWithoutQRCode))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Category.Icon))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.RegisteringDate))
                .ForMember(dest => dest.Whatsapp, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
                .ForMember(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Verified))
                .ForMember(dest => dest.WebSite, opt => opt.MapFrom(src => src.Site))
                .ForMember(dest => dest.MinorQueue, opt => opt.MapFrom(src => src.Queues.Count > 0 ? src.Queues.First(x => x.Status == QueueStatusEnum.Open).Customers.Count : 0))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<HighLightDto, HighLight>();
            CreateMap<HighLight, HighLightDto>();

            CreateMap<QueueReportDto, Customer>();
            CreateMap<Customer, QueueReportDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.ServiceStartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.ServiceEndTime))
                .ForMember(dest => dest.QueueDate, opt => opt.MapFrom(src => src.RegisteringDate))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.Name))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Items.Sum(x => x.FinalPrice)))
                .ForMember(dest => dest.TotalTime, opt => opt.MapFrom(src =>
                                src.ServiceStartTime.HasValue && src.ServiceEndTime.HasValue
                                    ? (src.ServiceEndTime.Value - src.ServiceStartTime.Value).ToString(@"hh\:mm")
                                    : "00:00"));

            CreateMap<OpeningHoursDto, OpeningHours>();
            CreateMap<OpeningHours, OpeningHoursDto>()
                .ForMember(dest => dest.WeekDay, opt => opt.MapFrom(src => src.WeekDay))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));

            CreateMap<Customer, OrderDto>()
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.User.Name} {src.User.LastName}"))
                .ForMember(dest => dest.PaymentIcon, opt => opt.MapFrom(src => src.Payment.Icon))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.Name))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.Payment.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.ProcessedAt, opt => opt.MapFrom(src => src.ProcessedAt))
                .ForMember(dest => dest.ProcessedByName, opt => opt.MapFrom(src => src.ProcessedBy != null ? src.ProcessedBy.Name : string.Empty))
                .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.RejectionReason))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Items.Sum(y => y.FinalPrice * y.Quantity)))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Status == CustomerStatusEnum.Pending && (DateTime.UtcNow - src.RegisteringDate).TotalMinutes > 2 ? PriorityEnum.High : PriorityEnum.Normal));

            CreateMap<CategoryDto, Category>();
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon));

            CreateMap<QueueDto, Queue>();
            CreateMap<Queue, QueueDto>()
                        .ForMember(dest => dest.QueueDescription, opt => opt.MapFrom(src => src.Name))
                        .ForMember(dest => dest.QueueId, opt => opt.MapFrom(src => src.Id))
                        .ForMember(dest => dest.ResponsibleId, opt => opt.MapFrom(src => src.EmployeeId))
                        .ForMember(dest => dest.ResponsibleName, opt => opt.MapFrom(src => $"{src.Employee.Name} {src.Employee.LastName}"))
                         .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.Customers.Count()))
                        .ForMember(dest => dest.CurrentCount, opt => opt.MapFrom(src =>
                                                     (src.Status == QueueStatusEnum.Open || src.Status == QueueStatusEnum.Paused)
                                                         ? src.Customers.Count(x => x.Status == CustomerStatusEnum.Waiting || x.Status == CustomerStatusEnum.InService || x.Status == CustomerStatusEnum.Absent)
                                                         : src.Customers.Count(x => x.Status == CustomerStatusEnum.Canceled || x.Status == CustomerStatusEnum.Removed || x.Status == CustomerStatusEnum.Done)));

            CreateMap<StoreProfessionalsDto, Store>();
            CreateMap<Store, StoreProfessionalsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(src => src.Verified))
                .ForMember(dest => dest.StoreLogoPath, opt => opt.MapFrom(src => src.LogoPath))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Subtitle, opt => opt.MapFrom(src => src.StoreSubtitle))
                .ForMember(dest => dest.Professionals, opt => opt.MapFrom(src => src.EmployeeStore.Select(es => es.Employee)));

            CreateMap<ProfessionalDto, User>();
            CreateMap<User, ProfessionalDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Subtitle, opt => opt.MapFrom(src => src.Subtitle))
                .ForMember(dest => dest.ServicesProvided, opt => opt.MapFrom(src => src.ServicesProvided))
                .ForMember(dest => dest.Liked, opt => opt.MapFrom(src => true));


            CreateMap<CustomerHistoryDto, Customer>();
            CreateMap<Customer, CustomerHistoryDto>()
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.Name))
                 .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Items.Sum(x => x.FinalPrice * x.Quantity)))
                 .ForMember(dest => dest.EstablishmentName, opt => opt.MapFrom(src => src.Queue.Store.Name))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.StatusReason, opt => opt.MapFrom(src => src.RemoveReason))
                 .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.RegisteringDate.Date.ToString("dd/MM/yyyy")))
                 .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src =>
                     src.ServiceStartTime.HasValue ? src.ServiceStartTime.Value.ToString("HH:mm") : string.Empty))
                 .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src =>
                     src.ServiceEndTime.HasValue ? src.ServiceEndTime.Value.ToString("HH:mm") : string.Empty))
                 .ForMember(dest => dest.Services, opt => opt.MapFrom(src =>
                     string.Join(", ", src.Items.Select(x => x.Service.Name))));

        }
    }
}