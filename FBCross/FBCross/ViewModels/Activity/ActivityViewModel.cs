using FBCross.Rest;
using FBCross.ViewModels.Appointment;
using FBCross.ViewModels.Instance;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Activity
{
    public class ActivityViewModel : ViewModelBase
    {
        public ActivityViewModel(IActivityFeed activityFeedService, IMvxNavigationService navigationService)
        {
            _activityFeedService = activityFeedService;
            _navigationService = navigationService;
        }
        private IActivityFeed _activityFeedService;
        private IMvxNavigationService _navigationService;

        public IMvxAsyncCommand<ActivityItem> ItemSelectedCommand => new MvxAsyncCommand<ActivityItem>(ItemSelected);

        private Task ItemSelected(ActivityItem item)
        {
            if (item.BookingType == 1)
            {
                return _navigationService.Navigate<AppointmentViewModel>();
            }
            else
            {
                FormsApp.CurrentInstanceId = item.ClassSessionSlug;
                return _navigationService.Navigate<InstanceDetailsViewModel>();
            }
            
        }

        private ObservableCollection<ActivityItem> _items { get; set; }
        public ObservableCollection<ActivityItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
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
            var activityFeedRequest = _activityFeedService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken);
            var response = await activityFeedRequest;
            if (Items == null)
            {
                Items = new ObservableCollection<ActivityItem>();
            }
            if (response.IsSuccessful && response.Data != null)
            {
                Items.Clear();
                foreach (var activityItem in response.Data)
                {
                    Items.Add(new ActivityItem
                    {
                        ActionText = activityItem.ActionTakenDate,
                        Description = activityItem.Description,
                        BookingType = activityItem.BookingType, 
                        ClassSessionSlug = activityItem.ClassSessionSlug
                    });
                }
            }
            Loading = false;
        }
    }
}
