using FBCross.Rest;
using FBCross.ViewModels.Instance;
using FBCross.ViewModels.Month;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FBCross.ViewModels.Agenda
{
    public class AgendaViewModel : ViewModelBase
    {

        private readonly ICalendarFeed _calendarFeedService;
        private readonly IMvxNavigationService _navigationService;

        public AgendaViewModel(ICalendarFeed calendarFeedService, IMvxNavigationService navigationService)
        {
            _calendarFeedService = calendarFeedService;
            _navigationService = navigationService;
            MessagingCenter.Subscribe<MonthViewModel>(this, "DateSelected", DateSelected);
        }

        private async void DateSelected(MonthViewModel obj)
        {
            await LoadData();
        }

        private ObservableCollection<AgendaItemGroup> _items { get; set; }
        public ObservableCollection<AgendaItemGroup> Items {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public IMvxAsyncCommand<AgendaItem> ItemSelectedCommand => new MvxAsyncCommand<AgendaItem>(ItemSelected);

        private Task ItemSelected(AgendaItem item)
        {
            FormsApp.CurrentInstanceId = item.Url.Replace("#instance/", string.Empty);
            return  _navigationService.Navigate<InstanceDetailsViewModel>();
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var start = DateTime.Now.Date;
            var end = DateTime.Now.Date.AddDays(8);
            if (FormsApp.SelectedDate != null && FormsApp.SelectedDate != DateTime.MinValue)
            {
                start = FormsApp.SelectedDate;
                end = start.AddDays(1);
            }
            var calendarFeedRequest = _calendarFeedService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken, start, end);
            var response = await calendarFeedRequest;
            if (Items == null)
            {
                Items = new ObservableCollection<AgendaItemGroup>();
            }
            if (response.IsSuccessful && response.Data != null)
            {
                Items.Clear();
                foreach (var group in AgendaItemGroup.FromCalendarFeedResponse(response.Data))
                {
                    Items.Add(group);
                }
            }
            Loading = false;
        }
    }
}
