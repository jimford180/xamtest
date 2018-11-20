﻿using AutoMapper;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Appointment
{
    public class ChooseServiceViewModel : ViewModelBase
    {
        private readonly AppointmentViewModel _appointment;
        private readonly IMvxNavigationService _navigationService;
        private List<ServiceViewModel> _allServices;

        public IMvxAsyncCommand<ServiceViewModel> ServiceSelectedCommand => new MvxAsyncCommand<ServiceViewModel>(ServiceSelected);
        public List<ServiceViewModel> AllServices { get => _allServices; set { _allServices = value; RaisePropertyChanged(() => AllServices); } }

        private async Task ServiceSelected(ServiceViewModel arg)
        {
            _appointment.Service = arg;
            await _navigationService.Navigate(_appointment);
        }
        
        public ChooseServiceViewModel(AppointmentViewModel appointment, IMvxNavigationService navigationService)
        {
            _appointment = appointment;
            _navigationService = navigationService;
            LoadServices();
        }

        private async void LoadServices()
        {
            var services = await FormsApp.Database.Services.GetEntitiesAsync();
            AllServices = services.Select(s => Mapper.Map<ServiceViewModel>(s)).ToList();
        }
    }
}
