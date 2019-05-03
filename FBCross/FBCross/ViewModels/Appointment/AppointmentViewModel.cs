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
using Xamarin.Forms;
using System.Linq;
using FBCross.ViewModels.Shared;

namespace FBCross.ViewModels.Appointment
{
    public class AppointmentViewModel : ViewModelBase
    {
        private ServiceViewModel _service;
        private EmployeeViewModel _employee;
        private DateTime _dateTime;
        private AppointmentViewModelType _type;
        private Customer.Customer _customer;
        private bool _remindByEmail;
        private bool _remindBySms;
        private string _notes;
        private Guid _merchantGuid;
        private Guid? _guid;
        private int? _waitListId;
        private string _timeZone;
        private string _classInstanceSlug;
        private bool _lockedForFixedTime;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUnifiedAvailability _unifiedAvailability;
        private readonly ICustomer _customerService;
        private readonly IScheduleBooking _scheduleBookingService;
        private readonly IFixedTimeBooking _fixedTimeBookingService;
        private readonly IWaitListBooking _waitListBookingService;

        public string PageTitle { get => _guid.HasValue ? "Edit Appointment" : "Create Appointment"; }

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
            set { _guid = value; RaisePropertyChanged(() => Guid); RaisePropertyChanged(() => DeleteIconName); }
        }
        public int? WaitListId
        {
            get { return _waitListId; }
            set { _waitListId = value; RaisePropertyChanged(() => WaitListId); }
        }
        public string ClassInstanceSlug { get => _classInstanceSlug; set { _classInstanceSlug = value; RaisePropertyChanged(() => ClassInstanceSlug); } }

        public bool RemindByEmail { get => _remindByEmail; set { _remindByEmail = value; RaisePropertyChanged(() => RemindByEmail); } }
        public bool RemindBySms { get => _remindBySms; set { _remindBySms = value; RaisePropertyChanged(() => RemindBySms); } }
        public string Notes { get => _notes; set { _notes = value; RaisePropertyChanged(() => Notes); } }
        public AppointmentViewModelType Type { get => _type; set { _type = value; RaisePropertyChanged(() => Type); } }
        public string TimeZone { get => _timeZone; set => _timeZone = value; }

        public IMvxAsyncCommand ChooseServiceCommand => new MvxAsyncCommand(GoToServiceChoice);
        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(GoToEmployeeChoice);
        public IMvxAsyncCommand ChangeCustomerCommand => new MvxAsyncCommand(GoToCustomerChoice);
        public IMvxAsyncCommand ChangeDateTimeCommand => new MvxAsyncCommand(GoToDateTimeChoice);
        public IMvxAsyncCommand SaveAppointmentCommand => new MvxAsyncCommand(SaveAppointment);
        public IMvxAsyncCommand CancelAppointmentCommand => new MvxAsyncCommand(CancelAppointment);

        public string DeleteIconName {
            get
            {
                if (_guid.HasValue)
                {
                    return "imgTrash";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private async Task CancelAppointment()
        {
            if ((_guid.HasValue && Type == AppointmentViewModelType.ScheduleBooking) || (_waitListId.HasValue && Type == AppointmentViewModelType.FixedTimeWaitList) || (_guid.HasValue && Type == AppointmentViewModelType.FixedTimeBooking))
            {
                var confirm = await FormsApp.Current.MainPage.DisplayAlert("Cancel Appointment", "Are you sure you want to cancel this appointment?", "Yes", "No");
                if (confirm)
                {
                    Loading = true;
                    var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
                    bool isSuccessful = false;
                    switch (Type) {
                        case AppointmentViewModelType.ScheduleBooking:
                            isSuccessful = (await _scheduleBookingService.Delete(_guid.Value, sessionInfo.MerchantGuid, sessionInfo.SessionToken, true, string.Empty)).IsSuccessful;
                            break;
                        case AppointmentViewModelType.FixedTimeBooking:
                            isSuccessful = (await _fixedTimeBookingService.Delete(_guid.Value, sessionInfo.MerchantGuid, sessionInfo.SessionToken, true)).IsSuccessful;
                            break;
                        case AppointmentViewModelType.FixedTimeWaitList:
                            isSuccessful = (await _waitListBookingService.Delete(_waitListId.Value, sessionInfo.SessionToken, sessionInfo.MerchantGuid)).IsSuccessful;
                            break;
                    }
                    if (isSuccessful)
                    {
                        await _navigationService.Close(this);
                    }
                }
            }
            else
            {
                var confirm = await FormsApp.Current.MainPage.DisplayAlert("Cancel Appointment", "Are you sure you want to stop making this appointment?", "Yes", "No");
                if (confirm)
                {
                    await _navigationService.Close(this);
                }
            }
        }

        private async Task SaveAppointment()
        {
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            _merchantGuid = sessionInfo.MerchantGuid;
            switch (_type)
            {
                case AppointmentViewModelType.FixedTimeBooking:
                    if (_guid.HasValue)
                    {
                        var fixedBookingRequest = Mapper.Map<Rest.Dto.BookingDetail>(this);
                        var response = await _fixedTimeBookingService.Put(fixedBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            await _navigationService.Close(this);
                        }
                    }
                    else
                    {
                        var fixedBookingRequest = Mapper.Map<Rest.Dto.BookingRequest>(this);
                        var response = await _fixedTimeBookingService.Post(fixedBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            await _navigationService.Close(this);
                        }
                    }
                    break;
                case AppointmentViewModelType.ScheduleBooking:
                    var scheduleBookingRequest = Mapper.Map<Rest.Dto.ScheduleBookingRequest>(this);
                    if (!string.IsNullOrWhiteSpace(scheduleBookingRequest.TimeZone))
                        scheduleBookingRequest.TimeZoneNET = true;
                    if (_guid.HasValue)
                    {
                        var response = await _scheduleBookingService.Put(scheduleBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            FormsApp.CurrentScheduleBookingId = null;
                            await _navigationService.Close(this);
                        }
                    }
                    else
                    {
                        var response = await _scheduleBookingService.Post(scheduleBookingRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            FormsApp.CurrentFixedTimeBooking = null;
                            await _navigationService.Close(this);
                        }
                    }
                    break;
                case AppointmentViewModelType.FixedTimeWaitList:
                    if (_waitListId.HasValue)
                    {
                        var waitListRequest = Mapper.Map<Rest.Dto.WaitListRequest>(this);
                        var response = await _waitListBookingService.Put(_waitListId.Value, waitListRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            await _navigationService.Close(this);
                        }
                    }
                    else
                    {
                        var waitListRequest = Mapper.Map<Rest.Dto.WaitListRequest>(this);
                        var response = await _waitListBookingService.Post(waitListRequest, sessionInfo.SessionToken, sessionInfo.MerchantGuid);
                        if (response.IsSuccessful)
                        {
                            await _navigationService.Close(this);
                        }
                    }
                    break;
            }



            Loading = false;
        }

        private async Task GoToServiceChoice()
        {
            if (!_lockedForFixedTime)
            {
                var serviceChoice = new ChooseServiceViewModel(this, _navigationService);
                await _navigationService.Navigate(serviceChoice);
            }
        }

        private async Task GoToEmployeeChoice()
        {
            if (!_lockedForFixedTime)
            {
                var employeeChoice = new ChooseEmployeeViewModel(this, _navigationService);
                await _navigationService.Navigate(employeeChoice);
            }
        }
        private async Task GoToCustomerChoice()
        {
            var customerChoice = new ChooseCustomerViewModel(this, _navigationService, _customerService);
            await _navigationService.Navigate(customerChoice);
        }
        private async Task GoToDateTimeChoice()
        {
            if (!_lockedForFixedTime)
            {
                if (_service != null && (Type == AppointmentViewModelType.FixedTimeBooking || _employee != null))
                {
                    var dateTimeChoice = new ChooseDateTimeViewModel(this, _navigationService, _unifiedAvailability);
                    await _navigationService.Navigate(dateTimeChoice);
                }
                else
                {
                    MessagingCenter.Send<AppointmentViewModel>(this, "FailGoToDateTimeChoice");
                }
            }
        }
        public bool LockedForFixedTime { get => _lockedForFixedTime; set => _lockedForFixedTime = value; }

        public AppointmentViewModel(IMvxNavigationService navigationService, IUnifiedAvailability unifiedAvailability, ICustomer customerService, IScheduleBooking scheduleBookingService, IFixedTimeBooking fixedTimeBookingService, IWaitListBooking waitListBookingService)
        {
            _navigationService = navigationService;
            _unifiedAvailability = unifiedAvailability;
            _customerService = customerService;
            _scheduleBookingService = scheduleBookingService;
            _fixedTimeBookingService = fixedTimeBookingService;
            _waitListBookingService = waitListBookingService;
            _remindByEmail = true;
        }
        public override async void Start()
        {
            Guid scheduleBookingGuid;
            if (FormsApp.CurrentScheduleBookingId != null && Guid.TryParse(FormsApp.CurrentScheduleBookingId, out scheduleBookingGuid))
            {
                Loading = true;
                var request = _scheduleBookingService.Get(scheduleBookingGuid);
                var response = await request;
                if (response.IsSuccessful)
                {
                    var booking = response.Data;
                    _guid = scheduleBookingGuid;
                    Type = AppointmentViewModelType.ScheduleBooking;
                    DateTime = booking.SessionDateTime;
                    TimeZone = booking.TimeZone;
                    Customer = Mapper.Map<Customer.Customer>(booking);
                    var services = await FormsApp.Database.Services.GetEntitiesAsync();
                    Service = Mapper.Map<ServiceViewModel>(services.First(s => booking.ServiceIds.Contains(s.Id)));
                    var employees = await FormsApp.Database.Employees.GetEntitiesAsync();
                    Employee = Mapper.Map<EmployeeViewModel>(employees.First(s => s.Id == booking.EmployeeId));
                    RemindByEmail = booking.RemindByEmail;
                    RemindBySms = booking.RemindBySms;
                }
                Loading = false;
            }

        }
    }
}
