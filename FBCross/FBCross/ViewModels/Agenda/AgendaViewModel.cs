using FBCross.Rest;
using FBCross.ViewModels.Appointment;
using FBCross.ViewModels.Block;
using FBCross.ViewModels.Instance;
using FBCross.ViewModels.Month;
using FBCross.ViewModels.Shared;
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
        private EmployeeViewModel _employee;

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

        public EmployeeViewModel Employee { get => _employee; set { _employee = value; RaisePropertyChanged(() => Employee); RaisePropertyChanged(() => EmployeeName); FormsApp.CurrentAgendaEmployee = value; } }
        public string EmployeeName
        {
            get { return _employee == null ? "All Employees" : _employee.Name; }
        }

        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(ChooseEmployee);

        private async Task ChooseEmployee()
        {
            var vm = new ChooseEmployeeViewModel(this, _navigationService);
            await _navigationService.Navigate(vm);
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
            if (item.Url.StartsWith("#instance"))
            {
                FormsApp.CurrentInstanceId = item.Url.Replace("#instance/", string.Empty);
                return _navigationService.Navigate<InstanceDetailsViewModel>();
            }
            else
            {
                if (item.Url.ToLower().StartsWith("#removeblock"))
                {
                    FormsApp.CurrentBlockId = item.Url.ToLower().Replace("#removeblock/", "");
                    return _navigationService.Navigate<BlockViewModel>();
                }
                else
                {
                    FormsApp.CurrentScheduleBookingId = item.Url.Replace("#booking/", string.Empty);
                    return _navigationService.Navigate<AppointmentViewModel>();
                }
            }
        }

        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            Employee = FormsApp.CurrentAgendaEmployee;
            await LoadData();
        }

        private async Task LoadData()
        {
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var start = DateTime.Now.Date;
            var end = DateTime.Now.Date.AddDays(8).AddMinutes(-1);
            if (FormsApp.SelectedDate != null && FormsApp.SelectedDate != DateTime.MinValue)
            {
                start = FormsApp.SelectedDate;
                end = start.AddDays(1).AddMinutes(-1);
            }
            int? employeeId = Employee?.Id;
            var calendarFeedRequest = _calendarFeedService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken, start, end, employeeId);
            var response = await calendarFeedRequest;
            if (Items == null)
            {
                Items = new ObservableCollection<AgendaItemGroup>();
            }
            if (response.IsSuccessful && response.Data != null)
            {
                Items.Clear();
                foreach (var group in AgendaItemGroup.FromCalendarFeedResponse(response.Data, start, end))
                {
                    Items.Add(group);
                }
            }
            Loading = false;
        }
    }
}
