using FBCross.Data;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FBCross.ViewModels.Customer;
using MvvmCross;
using MvvmCross.Navigation;
using FBCross.Rest;
using AutoMapper;
using FBCross.ViewModels.Navigation;

namespace FBCross.ViewModels.Appointment
{
    public class AppointmentViewModel : ViewModelBase
    {
        private ServiceViewModel _service;
        private EmployeeViewModel _employee;
        private DateTime _dateTime;
        private bool _isFixedTimeAppointment;
        private Customer.Customer _customer;
        private bool _remindByEmail;
        private bool _remindBySms;
        private string _notes;
        private Guid _merchantGuid;
        private Guid? _guid;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUnifiedAvailability _unifiedAvailability;
        private readonly ICustomer _customerService;
        private readonly IScheduleBooking _scheduleBookingService;
        private readonly IFixedTimeBooking _fixedTimeBookingService;

        public ServiceViewModel Service { get => _service; set { _service = value; RaisePropertyChanged(() => Service); RaisePropertyChanged(() => ServiceName); } }
        public string ServiceName
        {
            get { return _service == null ? "Select Service" : _service.Name; }
        }

        public EmployeeViewModel Employee { get => _employee; set { _employee = value; RaisePropertyChanged(() => Employee); RaisePropertyChanged(() => EmployeeName); } }
        public string EmployeeName
        {
            get { return _employee == null ? "Select Employee" : _employee.Name; }
        }

        public DateTime DateTime { get => _dateTime; set { _dateTime = value; RaisePropertyChanged(() => DateTime); RaisePropertyChanged(() => Date); RaisePropertyChanged(() => Time); } }
        public string Date
        {
            get { return DateTime > DateTime.MinValue ? DateTime.ToString("MM/dd/yyyy") : "Select Date"; }
        }
        public string Time
        {
            get { return DateTime > DateTime.MinValue ? DateTime.ToString("h:mm tt") : string.Empty; }
        }

        public Customer.Customer Customer { get => _customer; set { _customer = value; RaiseAllPropertiesChanged(); } }
        public string CustomerName
        {
            get { return _customer == null ? "Select Customer" : string.Format("{0} {1}", _customer.FirstName, _customer.LastName); }
        }
        public string CustomerEmail
        {
            get { return _customer == null ? string.Empty : _customer.Email; }
        }
        public string CustomerPhone
        {
            get { return _customer == null ? string.Empty : _customer.Phone; }
        }
        public string CustomerNotes
        {
            get { return _customer == null ? string.Empty : _customer.Notes; }
        }
        public Guid MerchantGuid
        {
            get { return _merchantGuid == null ? Guid.Empty : _merchantGuid; }
        }
        public Guid Guid
        {
            get { return _guid.HasValue ? _guid.Value : Guid.Empty; }
        }

        public bool RemindByEmail { get => _remindByEmail; set { _remindByEmail = value; RaisePropertyChanged(() => RemindByEmail); } }
        public bool RemindBySms { get => _remindBySms; set { _remindBySms = value; RaisePropertyChanged(() => RemindBySms); } }
        public string Notes { get => _notes; set { _notes = value; RaisePropertyChanged(() => Notes); } }
        public bool IsFixedTimeAppointment { get => _isFixedTimeAppointment; set { _isFixedTimeAppointment = value; RaisePropertyChanged(() => IsFixedTimeAppointment); } }

        public IMvxAsyncCommand ChooseServiceCommand => new MvxAsyncCommand(GoToServiceChoice);
        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(GoToEmployeeChoice);
        public IMvxAsyncCommand ChangeCustomerCommand => new MvxAsyncCommand(GoToCustomerChoice);
        public IMvxAsyncCommand ChangeDateTimeCommand => new MvxAsyncCommand(GoToDateTimeChoice);
        public IMvxAsyncCommand SaveAppointmentCommand => new MvxAsyncCommand(SaveAppointment);

        private async Task SaveAppointment()
        {
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            _merchantGuid = sessionInfo.MerchantGuid;
            if (_isFixedTimeAppointment)
            {
                var fixedBookingRequest = Mapper.Map<Rest.Dto.BookingRequest>(this);
                var response = await _fixedTimeBookingService.Post(fixedBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                if (response.IsSuccessful)
                {
                    await _navigationService.Navigate<TabbedHomeViewModel>();
                }
            }
            else
            {
                var scheduleBookingRequest = Mapper.Map<Rest.Dto.ScheduleBookingRequest>(this);
                var response = await _scheduleBookingService.Post(scheduleBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                if (response.IsSuccessful)
                {
                    await _navigationService.Navigate<RootViewModel>();
                }
            }
        }

        private async Task GoToServiceChoice()
        {
            var serviceChoice = new ChooseServiceViewModel(this, _navigationService);
            await _navigationService.Navigate(serviceChoice);
        }

        private async Task GoToEmployeeChoice()
        {
            var employeeChoice = new ChooseEmployeeViewModel(this, _navigationService);
            await _navigationService.Navigate(employeeChoice);
        }
        private async Task GoToCustomerChoice()
        {
            var customerChoice = new ChooseCustomerViewModel(this, _navigationService, _customerService);
            await _navigationService.Navigate(customerChoice);
        }
        private async Task GoToDateTimeChoice()
        {
            var dateTimeChoice = new ChooseDateTimeViewModel(this, _navigationService, _unifiedAvailability);
            await _navigationService.Navigate(dateTimeChoice);
        }

        public AppointmentViewModel(IMvxNavigationService navigationService, IUnifiedAvailability unifiedAvailability, ICustomer customerService, IScheduleBooking scheduleBookingService, IFixedTimeBooking fixedTimeBookingService)
        {
            _navigationService = navigationService;
            _unifiedAvailability = unifiedAvailability;
            _customerService = customerService;
            _scheduleBookingService = scheduleBookingService;
            _fixedTimeBookingService = fixedTimeBookingService;
        }
    }
}
