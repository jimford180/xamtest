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
using Employee = FBCross.Data.Employee;

namespace FBCross.ViewModels.Instance
{
    public class InstanceDetailsViewModel : ViewModelBase
    {
        private readonly IInstanceDetails _instanceDetailsService;
        private readonly IMvxNavigationService _navigationService;

        public InstanceDetailsViewModel(IInstanceDetails instanceDetailsService, IMvxNavigationService navigationService)
        {
            _instanceDetailsService = instanceDetailsService;
            _navigationService = navigationService;
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
        public string DateTime { get => _dateTime; set { _dateTime = value; RaisePropertyChanged(() => DateTime); } }
        public int SpotsTaken { get => _spotsTaken; set { _spotsTaken = value; RaisePropertyChanged(() => SpotsTaken); } }
        public string SpotsTakenText { get { return string.Format("{0} / ", _spotsTaken); } }
        public int TotalSpots { get => _totalSpots; set { _totalSpots = value; RaisePropertyChanged(() => TotalSpots); } }
        public bool RequiresPayment { get => _requiresPayment; set { _requiresPayment = value; RaisePropertyChanged(() => RequiresPayment); } }
        public bool LockPrice { get => _lockPrice; set { _lockPrice = value; RaisePropertyChanged(() => LockPrice); } }
        public bool NoShowFee { get => _noShowFee; set { _noShowFee = value; RaisePropertyChanged(() => NoShowFee); } }
        public decimal Price { get => _price; set { _price = value; RaisePropertyChanged(() => Price); } }

        public List<BookingDetail> CurrentBookings { get; private set; }
        public List<WaitListDetail> WaitListBookings { get; private set; }

        public string InstanceDetails { get => _instanceDetails; set { _instanceDetails = value; RaisePropertyChanged(() => InstanceDetails); } }
        public List<Employee> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }

        public Employee Employee { get => _employee; set { _employee = value; RaisePropertyChanged(() => Employee); Save(); } }
        public Employee SecondEmployee { get => _secondEmployee; set { _secondEmployee = value; RaisePropertyChanged(() => SecondEmployee); Save(); } }

        public IMvxCommand CurrentBookingsCommand => new MvxCommand(GoToCurrentBookings);
        public IMvxCommand WaitListCommand => new MvxCommand(GoToWaitList);

        private void GoToCurrentBookings()
        {
            var bookingsViewModel = new FixedTimeBookingsViewModel(CurrentBookings);
            _navigationService.Navigate(bookingsViewModel);

        }
        private void GoToWaitList()
        {
            var waitListViewModel = new WaitListBookingsViewModel(WaitListBookings);
            _navigationService.Navigate(waitListViewModel);
        }

        private async void Save()
        {
            if (!Loading)
            {
                Loading = true;
                var instanceUpdate = new ClassInstanceUpdate
                {
                    EmployeeId = Employee.Id,
                    RequiresPayment = RequiresPayment,
                    LockPrice = LockPrice,
                    NoShowFee = NoShowFee,
                    Price = Price,
                    ReverseCancellation = false,
                    SecondEmployeeId = SecondEmployee.Id,
                    TotalSlots = TotalSpots,
                };
                var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
                var response = await _instanceDetailsService.Put(instanceUpdate, FormsApp.CurrentInstanceId,  sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                Loading = false;
            }
        }

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
                CurrentBookings = response.Data.Bookings;
                WaitListBookings = response.Data.WaitListParties;
            }

            Loading = false;
        }


    }
}
