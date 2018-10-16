using FBCross.ViewModels.Authentication;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Navigation
{
    public class MenuViewModel : MvxViewModel
    {

        public MenuViewModel()
        {
            Items = new ObservableCollection<MenuItem>
            {
                new MenuItem{ Name = "Home", PageType=PageType.Home },
                new MenuItem{ Name = "New Appointment", PageType=PageType.NewAppointment },
                new MenuItem{ Name = "Logout", PageType=PageType.Logout }
            };
        }
        private readonly IMvxNavigationService _navigationService;
        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
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

        private  Task ItemSelected(MenuItem item)
        {
            Task task = null;
            switch (item.PageType)
            {
                case PageType.Home:
                    task = _navigationService.Navigate<TabbedHomeViewModel>();
                    break;
                case PageType.NewAppointment:
                    task = _navigationService.Navigate<TabbedHomeViewModel>();
                    break;
                case PageType.Logout:
                    var t2 = FormsApp.Database.Sessions.RemoveAll();
                    task = t2.ContinueWith(a => _navigationService.Navigate<LoginViewModel>());
                    break;
            }
            return task;
        }

    }
}
