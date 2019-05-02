using AutoMapper;
using FBCross.ViewModels.Shared;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Event
{
    public class ChooseServiceViewModel : ViewModelBase
    {
        private readonly EventViewModel _event;
        private readonly IMvxNavigationService _navigationService;
        private List<ServiceViewModel> _allServices;

        public IMvxAsyncCommand<ServiceViewModel> ServiceSelectedCommand => new MvxAsyncCommand<ServiceViewModel>(ServiceSelected);
        public List<ServiceViewModel> AllServices { get => _allServices; set { _allServices = value; RaisePropertyChanged(() => AllServices); } }

        private async Task ServiceSelected(ServiceViewModel arg)
        {
            _event.Service = arg;
            await _navigationService.Close(this);
        }
        
        public ChooseServiceViewModel(EventViewModel eventModel, IMvxNavigationService navigationService)
        {
            _event = eventModel;
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
