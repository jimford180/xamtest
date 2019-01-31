using AutoMapper;
using FBCross.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Appointment
{
    public class ChooseDateTimeViewModel : ViewModelBase
    {
        private readonly AppointmentViewModel _appointment;
        private readonly IMvxNavigationService _navigationService;
        private readonly IUnifiedAvailability _unifiedAvailability;
        private DateTime _date;
        
        public DateTime Date { get => _date; set { _date = value; RaisePropertyChanged(() => Date); } }

        public IMvxAsyncCommand<AvailableTimeViewModel> TimeSelectedCommand => new MvxAsyncCommand<AvailableTimeViewModel>(TimeSelected);

        private async Task TimeSelected(AvailableTimeViewModel item)
        {
            _appointment.DateTime = item.DateTime;
            _appointment.ClassInstanceSlug = item.ClassInstanceSlug;
            await _navigationService.Close(this);
        }

        private ObservableCollection<object> _selectedDate;

        public ObservableCollection<object> SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                if (_selectedDate.Count == 3)
                {
                    var dateString = string.Format("{0} {1} {2}", _selectedDate[0], _selectedDate[1], _selectedDate[2]);
                    Date = Convert.ToDateTime(dateString);
                }
                RaisePropertyChanged("SelectedDate");
                FetchAvailability();
            }
        }
        private string _availabilityResultMessage;

        private async Task FetchAvailability()
        {
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var merchantGuid = sessionInfo.MerchantGuid;
            var sessionToken = sessionInfo.SessionToken;
            var serviceIds = new List<int> { _appointment.Service.Id };
            var employeeId = _appointment.Employee?.Id;
            var startDate = Date;
            var endDate = Date.AddDays(1);
            var availabilityResponse = await _unifiedAvailability.Get(merchantGuid, sessionToken, serviceIds, employeeId, startDate, endDate);
            AvailableTimes = new List<AvailableTimeViewModel>();
            if (availabilityResponse.IsSuccessful)
            {
                if (availabilityResponse.Data.AvailableTimes != null)
                {
                    availabilityResponse.Data.AvailableTimes = availabilityResponse.Data.AvailableTimes.Where(t => t.DateTime >= startDate && t.DateTime < endDate).ToList();
                }
                if (availabilityResponse.Data.ScheduleType == Rest.Dto.ScheduleType.None)
                {
                    AvailabilityResultMessage = _appointment.Employee == null ? string.Format("There are no schedules set up for {0}.", _appointment.Service.Name) : string.Format("{0} has no schedules set up for {1}", _appointment.Employee.Name, _appointment.Service.Name);
                }
                else
                {
                    _appointment.Type = availabilityResponse.Data.ScheduleType == Rest.Dto.ScheduleType.Fixed ? AppointmentViewModelType.FixedTimeBooking : AppointmentViewModelType.ScheduleBooking;
                    if (availabilityResponse.Data.AvailableTimes != null && availabilityResponse.Data.AvailableTimes.Any())
                    {
                        AvailableTimes = availabilityResponse.Data.AvailableTimes.Select(t => Mapper.Map<AvailableTimeViewModel>(t)).ToList();
                        AvailabilityResultMessage = null;
                    }
                    else
                    {
                        AvailabilityResultMessage = string.Format("No available times found on {0}", Date.ToString("D"));
                    }
                }
            }
            Loading = false;
        }

        public List<AvailableTimeViewModel> AvailableTimes { get => _availableTimes; set { _availableTimes = value; RaisePropertyChanged(() => AvailableTimes); } }

        public string AvailabilityResultMessage { get => _availabilityResultMessage; set { _availabilityResultMessage = value; RaisePropertyChanged(() => AvailabilityResultMessage); RaisePropertyChanged(() => ShowAvailabilityResultMessage); } }

        public bool ShowAvailabilityResultMessage { get => !string.IsNullOrWhiteSpace(_availabilityResultMessage); }

        private List<AvailableTimeViewModel> _availableTimes;

        public ChooseDateTimeViewModel(AppointmentViewModel appointment, IMvxNavigationService navigationService, IUnifiedAvailability unifiedAvailability)
        {
            var dt = appointment.DateTime > DateTime.Now ? appointment.DateTime : DateTime.Now;
            _appointment = appointment;
            _navigationService = navigationService;
            _unifiedAvailability = unifiedAvailability;
            _date = dt.Date;
            _availableTimes = new List<AvailableTimeViewModel>();

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Date.Month).Substring(0, 3));
            if (dt.Date.Day < 10)
                todaycollection.Add("0" + dt.Date.Day);
            else
                todaycollection.Add(dt.Date.Day.ToString());
            todaycollection.Add(dt.Date.Year.ToString());
            SelectedDate = todaycollection;
        }
                
    }
}
