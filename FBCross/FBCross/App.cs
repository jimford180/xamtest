using AutoMapper;
using FBCross.Rest;
using FBCross.ViewModels.Agenda;
using FBCross.ViewModels.Authentication;
using FBCross.ViewModels.Navigation;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
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
            Mvx.IoCProvider.RegisterType<IMerchantState, MerchantState>();
            Mvx.IoCProvider.RegisterType<IUnifiedAvailability, UnifiedAvailability>();
            Mvx.IoCProvider.RegisterType<ICustomer, Customer>();
            Mvx.IoCProvider.RegisterType<IFixedTimeBooking, FixedTimeBooking>();
            Mvx.IoCProvider.RegisterType<IScheduleBooking, ScheduleBooking>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Rest.Dto.Employee, Data.Employee>();
                cfg.CreateMap<Rest.Dto.AppointmentType, Data.Service>();
                cfg.CreateMap<Rest.Dto.BookingDetail, ViewModels.Instance.FixedTimeBookingViewModel>();
                cfg.CreateMap<Rest.Dto.WaitListDetail, ViewModels.Instance.FixedTimeBookingViewModel>();
                cfg.CreateMap<Data.Service, ViewModels.Appointment.ServiceViewModel>();
                cfg.CreateMap<Data.Employee, ViewModels.Appointment.EmployeeViewModel>();
                cfg.CreateMap<Rest.Dto.AvailableTime, ViewModels.Appointment.AvailableTimeViewModel>();
                cfg.CreateMap<Rest.Dto.Customer, ViewModels.Customer.Customer>();
                cfg.CreateMap<ViewModels.Appointment.AppointmentViewModel, Rest.Dto.BookingRequest>();
                ConfigureAppointmentViewModelToScheduleBookingRequest(cfg);
            }
            );

            RegisterAppStart<RootViewModel>();
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
                            .ForMember(dest => dest.SessionDateTime, opt => opt.MapFrom(src => src.DateTime.ToString()));

        }
    }
}
