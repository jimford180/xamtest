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
    public class ChooseLocationViewModel : ViewModelBase
    {
        private readonly EventViewModel _event;
        private readonly IMvxNavigationService _navigationService;
        private List<LocationViewModel> _allLocations;

        public IMvxAsyncCommand<LocationViewModel> ServiceSelectedCommand => new MvxAsyncCommand<LocationViewModel>(LocationSelected);
        public List<LocationViewModel> AllLocations { get => _allLocations; set { _allLocations = value; RaisePropertyChanged(() => AllLocations); } }

        private async Task LocationSelected(LocationViewModel arg)
        {
            _event.Location = arg;
            await _navigationService.Close(this);
        }
        
        public ChooseLocationViewModel(EventViewModel eventModel, IMvxNavigationService navigationService)
        {
            _event = eventModel;
            _navigationService = navigationService;
            LoadLocations();
        }

        private async void LoadLocations()
        {
            var locations = await FormsApp.Database.Locations.GetEntitiesAsync();
            AllLocations = locations.Select(s => Mapper.Map<LocationViewModel>(s)).ToList();
        }
    }
}
