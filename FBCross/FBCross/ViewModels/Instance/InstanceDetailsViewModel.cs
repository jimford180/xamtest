using FBCross.Data;
using FBCross.Rest;
using FBCross.Rest.Dto;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employee = FBCross.Data.Employee;

namespace FBCross.ViewModels.Instance
{
    public class InstanceDetailsViewModel : ViewModelBase
    {
        private readonly IInstanceDetails _instanceDetailsService;
        private readonly IMvxNavigationService _navigationService;
        private Rest.ICustomer _customerService { get; }
        private Rest.IScheduleBooking _scheduleBookingService { get; }
        private Rest.IFixedTimeBooking _fixedTimeBookingService { get; }
        private Rest.IWaitListBooking _waitListBookingService{ get; }
        private readonly Rest.IUnifiedAvailability _unifiedAvailability;

        public InstanceDetailsViewModel(IInstanceDetails instanceDetailsService, IMvxNavigationService navigationService, IUnifiedAvailability unifiedAvailability, ICustomer customerService, IScheduleBooking scheduleBookingService, IFixedTimeBooking fixedTimeBookingService, IWaitListBooking waitListBookingService)
        {
            _instanceDetailsService = instanceDetailsService;
            _navigationService = navigationService;
            _unifiedAvailability = unifiedAvailability;
            _customerService = customerService;
            _scheduleBookingService = scheduleBookingService;
            _fixedTimeBookingService = fixedTimeBookingService;
            _waitListBookingService = waitListBookingService;
        }
        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                _serviceName = value;
                RaisePropertyChanged(() => ServiceName);
            }
        }
        public string Id { get => _id; set { _id = value; RaisePropertyChanged(() => Id); } }
        public string DateTime { get => _dateTime; set { _dateTime = value; RaisePropertyChanged(() => DateTime); } }
        public int SpotsTaken { get => _spotsTaken; set { _spotsTaken = value; RaisePropertyChanged(() => SpotsTaken); RaisePropertyChanged(() => SpotsTakenText); } }
        public string SpotsTakenText { get { return string.Format("Spots Taken: {0} / {1}", _spotsTaken, _totalSpots); } }
        public int TotalSpots { get => _totalSpots; set { _totalSpots = value; RaisePropertyChanged(() => TotalSpots); RaisePropertyChanged(() => SpotsTakenText); } }
        public bool RequiresPayment { get => _requiresPayment; set { _requiresPayment = value; RaisePropertyChanged(() => RequiresPayment); } }
        public bool LockPrice { get => _lockPrice; set { _lockPrice = value; RaisePropertyChanged(() => LockPrice); } }
        public bool NoShowFee { get => _noShowFee; set { _noShowFee = value; RaisePropertyChanged(() => NoShowFee); } }
        public decimal Price { get => _price; set { _price = value; RaisePropertyChanged(() => Price); } }

        public List<BookingDetail> CurrentBookings { get; private set; }
        public List<WaitListDetail> WaitListBookings { get; private set; }

        public string InstanceDetails { get => _instanceDetails; set { _instanceDetails = value; RaisePropertyChanged(() => InstanceDetails); } }
        public List<Employee> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }

        public Employee Employee { get => _employee; set { _employee = value; RaisePropertyChanged(() => Employee); RaisePropertyChanged(() => EmployeeName); Save(); } }
        public Employee SecondEmployee { get => _secondEmployee; set { _secondEmployee = value; RaisePropertyChanged(() => SecondEmployee); RaisePropertyChanged(() => SecondEmployeeName); Save(); } }
        public string EmployeeName { get => "Employee: " + (_employee  == null ? "None" : _employee.Name); }
        public string SecondEmployeeName { get => "Second Employee: " + (_secondEmployee == null ? "None" : _secondEmployee.Name); }

        private string _currentBookingsText;
        private string _waitListText;
        public string CurrentBookingsText { get => _currentBookingsText; set { _currentBookingsText = value; RaisePropertyChanged(() => CurrentBookingsText); } }
        public string WaitListText { get => _waitListText; set { _waitListText = value; RaisePropertyChanged(() => WaitListText); } }

        public IMvxCommand CurrentBookingsCommand => new MvxCommand(GoToCurrentBookings);
        public IMvxCommand WaitListCommand => new MvxCommand(GoToWaitList);
        public IMvxAsyncCommand RemoveSessionCommad => new MvxAsyncCommand(RemoveSession);
        public IMvxAsyncCommand SpotsTakenCommand => new MvxAsyncCommand(SpotsTakenTapped);
        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(ChooseEmployee);
        public IMvxAsyncCommand ChooseSecondEmployeeCommand => new MvxAsyncCommand(ChooseSecondEmployee);

        private async Task ChooseEmployee()
        {
            var employeeViewModel = new ChooseInstanceEmployeeViewModel(this, _navigationService, EmployeeType.Primary);
            await _navigationService.Navigate(employeeViewModel);
        }

        

        private async Task ChooseSecondEmployee()
        {
            var employeeViewModel = new ChooseInstanceEmployeeViewModel(this, _navigationService, EmployeeType.Secondary);
            await _navigationService.Navigate(employeeViewModel);
        }

        private async Task SpotsTakenTapped()
        {
            var spotsViewModel = new TotalSpotsViewModel(this, _navigationService);
            await _navigationService.Navigate(spotsViewModel);
        }

        private async Task RemoveSession()
        {
            var confirm = await FormsApp.Current.MainPage.DisplayAlert("Cancel Session", "Are you sure you want to cancel this session? Any regsistered customers will receive a cancellation message, and the time will not be bookable again.", "Yes", "No");
            if (confirm)
            {
                Loading = true;
                var sessionInformation = await FormsApp.GetSessionTokenAndMerchantGuid();
                var response = await _instanceDetailsService.Delete(_id, true, true, sessionInformation.SessionToken, sessionInformation.MerchantGuid);
                Loading = false;
                if (response.IsSuccessful)
                {
                    await _navigationService.Close(this);
                }
            }
        }

        private void GoToCurrentBookings()
        {
            if (!Loading)
            {
                var bookingsViewModel = new FixedTimeBookingsViewModel(this, CurrentBookings, _navigationService, _unifiedAvailability, _customerService, _scheduleBookingService, _fixedTimeBookingService, _waitListBookingService);
                _navigationService.Navigate(bookingsViewModel);
            }

        }
        private void GoToWaitList()
        {
            if (!Loading)
            {
                var waitListViewModel = new WaitListBookingsViewModel(this, WaitListBookings, _navigationService, _unifiedAvailability, _customerService, _scheduleBookingService, _fixedTimeBookingService, _waitListBookingService);
                _navigationService.Navigate(waitListViewModel);
            }
        }

        public async void Save()
        {
            if (!Loading)
            {
                Loading = true;
                var instanceUpdate = new ClassInstanceUpdate
                {
                    EmployeeId = Employee?.Id,
                    RequiresPayment = RequiresPayment,
                    LockPrice = LockPrice,
                    NoShowFee = NoShowFee,
                    Price = Price,
                    ReverseCancellation = false,
                    SecondEmployeeId = SecondEmployee?.Id,
                    TotalSlots = TotalSpots,
                };
                var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
                var response = await _instanceDetailsService.Put(instanceUpdate, FormsApp.CurrentInstanceId,  sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                Loading = false;
            }
        }
        private string _id;
        private string _serviceName;
        private string _dateTime;
        private int _spotsTaken;
        private int _totalSpots;
        private string _instanceDetails;
        private List<Employee> _allEmployees;
        private Employee _employee;
        private Employee _secondEmployee;
        private bool _requiresPayment;
        private bool _lockPrice = false;
        private bool _noShowFee;
        private decimal _price;

        public override async void Start()
        {
            base.Start();
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var response = await _instanceDetailsService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken, FormsApp.CurrentInstanceId);
            var allServices = await FormsApp.Database.Services.GetEntitiesAsync();
            AllEmployees = await FormsApp.Database.Employees.GetEntitiesAsync();
            if (response.IsSuccessful)
            {
                ServiceName = allServices.SingleOrDefault(s => s.Id == response.Data.ServiceId)?.Name;
                DateTime = string.Format("{0} {1}", response.Data.Date, response.Data.Time);
                SpotsTaken = response.Data.SlotsUsed;
                TotalSpots = response.Data.TotalSlots;
                Employee = AllEmployees.SingleOrDefault(s => s.Id == response.Data.EmployeeId);
                SecondEmployee = AllEmployees.SingleOrDefault(s => s.Id == response.Data.SecondEmployeeId);
                RequiresPayment = response.Data.RequiresPayment;
                NoShowFee = response.Data.NoShowFee;
                Price = response.Data.Price;
                CurrentBookings = response.Data.Bookings.Where(c => !c.Cancelled).ToList();
                WaitListBookings = response.Data.WaitListParties;
                Id = response.Data.Id;
                CurrentBookingsText = $"Current Bookings ({CurrentBookings.Count})";
                WaitListText = $"Wait List ({WaitListBookings.Count})";
            }

            Loading = false;
        }


    }
}
