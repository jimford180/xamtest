using AutoMapper;
using FBCross.Rest;
using FBCross.Rest.Dto;
using FBCross.ViewModels.Agenda;
using FBCross.ViewModels.Authentication;
using FBCross.ViewModels.Navigation;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBCross
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzU1MDNAMzEzNjJlMzMyZTMwT2FGREFMRFJnc2FGTWFHcU9UWm5RWXhPQ2hxazM3M3YxdmxDUmdockJNWT0=");

            Mvx.IoCProvider.RegisterType<ICalendarFeed, CalendarFeed>();
            Mvx.IoCProvider.RegisterType<IInstanceDetails, InstanceDetails>();
            Mvx.IoCProvider.RegisterType<ISessionAuth, SessionAuth>();
            Mvx.IoCProvider.RegisterType<IMerchantState, Rest.MerchantState>();
            Mvx.IoCProvider.RegisterType<IUnifiedAvailability, UnifiedAvailability>();
            Mvx.IoCProvider.RegisterType<ICustomer, Rest.Customer>();
            Mvx.IoCProvider.RegisterType<IFixedTimeBooking, FixedTimeBooking>();
            Mvx.IoCProvider.RegisterType<IScheduleBooking, ScheduleBooking>();
            Mvx.IoCProvider.RegisterType<IActivityFeed, ActivityFeed>();
            Mvx.IoCProvider.RegisterType<IWaitListBooking, WaitListBooking>();
            Mvx.IoCProvider.RegisterType<IHoliday, Rest.Holiday>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Rest.Dto.Employee, Data.Employee>();
                cfg.CreateMap<Rest.Dto.AppointmentType, Data.Service>();
                cfg.CreateMap<Rest.Dto.BookingDetail, ViewModels.Instance.FixedTimeBookingViewModel>();
                cfg.CreateMap<Rest.Dto.WaitListDetail, ViewModels.Instance.FixedTimeBookingViewModel>().ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.WaitListId.ToString()));
                cfg.CreateMap<Data.Service, ViewModels.Shared.ServiceViewModel>();
                cfg.CreateMap<Data.Employee, ViewModels.Shared.EmployeeViewModel>();
                cfg.CreateMap<Data.Location, ViewModels.Shared.LocationViewModel>();
                cfg.CreateMap<Rest.Dto.Schedule, Data.MasterSchedule>().ForMember(dest => dest.ServiceIds, opt => opt.MapFrom(src => string.Join(",", src.ServiceIds)));
                cfg.CreateMap<Rest.Dto.MasterClass, Data.MasterClass>();
                cfg.CreateMap<Rest.Dto.Location, Data.Location>();

                cfg.CreateMap<Rest.Dto.AvailableTime, ViewModels.Appointment.AvailableTimeViewModel>();
                cfg.CreateMap<Rest.Dto.Customer, ViewModels.Customer.Customer>();
                cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.BookingRequest>();
                ConfigureAppointmentViewModelToScheduleBookingRequest(cfg);
                ConfigureAppointmentViewModelToFixedTimeBookingRequest(cfg);
                cfg.CreateMap<Rest.Dto.ScheduleBookingInfo, ViewModels.Customer.Customer>();
                cfg.CreateMap<Rest.Dto.BookingDetail, ViewModels.Customer.Customer>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
                cfg.CreateMap<Rest.Dto.WaitListDetail, ViewModels.Customer.Customer>();

                ConfigurationBlockViewModelToHoliday(cfg);
            }
            );

            RegisterAppStart<RootViewModel>();

        }

        private void ConfigurationBlockViewModelToHoliday(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ViewModels.Block.BlockViewModel, Rest.Dto.Holiday>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.Add(src.StartTime)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.Add(src.EndTime)))
                .ForMember(dest => dest.StringStartDate, opt => opt.MapFrom(src => src.StartDate.Add(src.StartTime).ToString()))
                .ForMember(dest => dest.StringEndDate, opt => opt.MapFrom(src => src.EndDate.Add(src.EndTime).ToString()));
        }

        private static void ConfigureAppointmentViewModelToScheduleBookingRequest(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.ScheduleBookingRequest>()
                            .ForMember(dest => dest.ServiceGuids, opt => opt.MapFrom(src => new List<Guid> { src.Service.ServiceGuid }))
                            .ForMember(dest => dest.EmployeeGuid, opt => opt.MapFrom(src => src.Employee.EmployeeGuid))
                            .ForMember(dest => dest.DateTimes, opt => opt.MapFrom(src => new List<string> { src.DateTime.ToString() }))
                            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                            .ForMember(dest => dest.GymGuid, opt => opt.MapFrom(src => src.MerchantGuid))
                            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
                            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Customer.Id))
                            .ForMember(dest => dest.ServiceGuidsCsv, opt => opt.MapFrom(src => src.Service.ServiceGuid))
                            .ForMember(dest => dest.SessionDateTime, opt => opt.MapFrom(src => src.DateTime.ToString()))
                            .ForMember(dest => dest.CustomBookingFields, opt => opt.MapFrom(src => src.Customer.CustomFields.Select(cf => new CustomBookingField
                            {
                                MerchantFieldId = cf.FieldId.Value,
                                Value = cf.Value
                            }).ToList()));
        }
        private static void ConfigureAppointmentViewModelToFixedTimeBookingRequest(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.BookingRequest>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.GymGuid, opt => opt.MapFrom(src => src.MerchantGuid))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.CustomBookingFields, opt => opt.MapFrom(src => src.Customer.CustomFields.Select(cf => new CustomBookingField
                {
                    MerchantFieldId = cf.FieldId.Value,
                    Value = cf.Value
                }).ToList()));

            cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.BookingDetail>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
                .ForMember(dest => dest.BookingId, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.ClassSlug, opt => opt.MapFrom(src => src.ClassInstanceSlug))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.CustomBookingFields, opt => opt.MapFrom(src => src.Customer.CustomFields.Select(cf => new CustomBookingField
                {
                    MerchantFieldId = cf.FieldId.Value,
                    Value = cf.Value
                }).ToList()));

            cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.WaitListRequest>()
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Customer.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Customer.LastName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Customer.Email))
               .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Customer.Phone))
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Customer.Id))
               .ForMember(dest => dest.CustomBookingFields, opt => opt.MapFrom(src => src.Customer.CustomFields.Select(cf => new CustomBookingField
               {
                   MerchantFieldId = cf.FieldId.Value,
                   Value = cf.Value
               }).ToList()));
        }
    }
}
