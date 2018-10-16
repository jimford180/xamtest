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
            Mvx.IoCProvider.RegisterType<ICalendarFeed, CalendarFeed>();
            Mvx.IoCProvider.RegisterType<IInstanceDetails, InstanceDetails>();
            Mvx.IoCProvider.RegisterType<ISessionAuth, SessionAuth>();
            Mvx.IoCProvider.RegisterType<IMerchantState, MerchantState>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Rest.Dto.Employee, Data.Employee>();
                cfg.CreateMap<Rest.Dto.AppointmentType, Data.Service>();
            }
            );

            RegisterAppStart<RootViewModel>();
        }
    }
}
