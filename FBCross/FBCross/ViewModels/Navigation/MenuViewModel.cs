using FBCross.ViewModels.Appointment;
using FBCross.ViewModels.Authentication;
using FBCross.ViewModels.Block;
using FBCross.ViewModels.Event;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FBCross.ViewModels.Navigation
{
    public class MenuViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;
        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            Items = new ObservableCollection<MenuItem>
            {
                new MenuItem{ Name = "Home", PageType=PageType.Home },
                new MenuItem{ Name = "New Appointment", PageType=PageType.NewAppointment },
                //new MenuItem{ Name = "New Event", PageType=PageType.NewEvent},
                new MenuItem{ Name = "New Block", PageType=PageType.NewBlock},
                new MenuItem{ Name = "Logout", PageType=PageType.Logout }
            };
        }

        private ObservableCollection<MenuItem> _items { get; set; }
        public ObservableCollection<MenuItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public IMvxAsyncCommand<MenuItem> ItemSelectedCommand => new MvxAsyncCommand<MenuItem>(ItemSelected);

        private async Task ItemSelected(MenuItem item)
        {
            switch (item.PageType)
            {
                case PageType.Home:
                    await _navigationService.Navigate<TabbedHomeViewModel>();
                    break;
                case PageType.NewAppointment:
                    FormsApp.CurrentScheduleBookingId = null;
                    await _navigationService.Navigate<AppointmentViewModel>();
                    break;
                case PageType.NewEvent:
                    await _navigationService.Navigate<EventViewModel>();
                    break;
                case PageType.NewBlock:
                    FormsApp.CurrentBlockId = null;
                    await _navigationService.Navigate<BlockViewModel>();
                    break;
                case PageType.Logout:
                    await FormsApp.Logout();
                    await _navigationService.Navigate<LoginViewModel>();
                    break;
            }
            if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage && navigationPage.CurrentPage is MasterDetailPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }

    }
}
