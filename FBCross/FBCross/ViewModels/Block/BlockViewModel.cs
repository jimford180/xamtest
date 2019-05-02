using AutoMapper;
using FBCross.Rest;
using FBCross.ViewModels.Shared;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Block
{
    public class BlockViewModel : ViewModelBase
    {
        private EmployeeViewModel _employee;
        private DateTime _startDate;
        private DateTime _endDate;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private bool _visibleToCustomers;
        private bool _informationalOnly;
        private string _title;
        private bool _repeat;
        private int? _recurrenceFrequency;
        private string _recurrenceType;
        private int? _recurrenceTimes;
        private int? _currentBlockId;
        private IMvxNavigationService _navigationService;
        private readonly IHoliday _holidayService;

        public BlockViewModel(IMvxNavigationService navigationService, IHoliday holidayService)
        {
            _startDate = DateTime.Now.Date;
            _endDate = DateTime.Now.AddDays(1).Date;
            _startTime = new TimeSpan(12, 0, 0);
            _endTime = new TimeSpan(12, 0, 0);
            _visibleToCustomers = false;
            _informationalOnly = false;
            _repeat = false;
            _navigationService = navigationService;
            _holidayService = holidayService;
            if (FormsApp.CurrentBlockId != null)
            {
                _currentBlockId = Convert.ToInt32(FormsApp.CurrentBlockId);
            }
        }

        public EmployeeViewModel Employee { get => _employee; set { _employee = value; RaisePropertyChanged(() => Employee); RaisePropertyChanged(() => EmployeeName); } }
        public string EmployeeName
        {
            get { return _employee == null ? "All Employees" : _employee.Name; }
        }

        public DateTime StartDate { get => _startDate; set { _startDate = value; RaisePropertyChanged(() => StartDate); RaisePropertyChanged(() => StartDateText); } }
        public DateTime EndDate { get => _endDate; set { _endDate= value; RaisePropertyChanged(() => EndDate); RaisePropertyChanged(() => EndDateText); } }
        public TimeSpan StartTime { get => _startTime; set { _startTime = value; RaisePropertyChanged(() => StartTime); RaisePropertyChanged(() => StartTimeText); } }
        public TimeSpan EndTime { get => _endTime; set { _endTime = value; RaisePropertyChanged(() => EndTime); RaisePropertyChanged(() => EndTimeText); } }
        public string StartDateText { get => _startDate.ToShortDateString(); }
        public string EndDateText { get => _endDate.ToShortDateString(); }
        public string StartTimeText { get => DateTime.Now.Date.Add(_startTime).ToShortTimeString(); }
        public string EndTimeText { get => DateTime.Now.Date.Add(_endTime).ToShortTimeString(); }
        public string Title { get => _title; set { _title = value; RaisePropertyChanged(() => Title); } }
        public bool VisibleToCustomers{ get => _visibleToCustomers; set { _visibleToCustomers = value; RaisePropertyChanged(() => VisibleToCustomers); } }
        public bool InformationalOnly { get => _informationalOnly; set { _informationalOnly = value; RaisePropertyChanged(() => InformationalOnly); } }
        public bool Repeat { get => _repeat; set { _repeat = value; RaisePropertyChanged(() => Repeat); } }
        public int? RecurrenceFrequency { get => _recurrenceFrequency; set { _recurrenceFrequency = value; RaisePropertyChanged(() => RecurrenceFrequency); } }
        public string RecurrenceType { get => _recurrenceType; set { _recurrenceType = value; RaisePropertyChanged(() => RecurrenceType); } }
        public int? RecurrenceTimes { get => _recurrenceTimes; set { _recurrenceTimes = value; RaisePropertyChanged(() => RecurrenceTimes); } }

        public string PageTitle { get => _currentBlockId.HasValue ? "Edit Block" : "Create Block"; }

        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(ChooseEmployee);

        private async Task ChooseEmployee()
        {
            var vm = new ChooseEmployeeViewModel(this, _navigationService);
            await _navigationService.Navigate(vm);
        }

        public IMvxAsyncCommand ChooseStartDateCommand => new MvxAsyncCommand(SelectStartDate);

        private async Task SelectStartDate()
        {
            var vm = new ChooseDateViewModel(_navigationService, this, true);
            await _navigationService.Navigate(vm);
        }

        public IMvxAsyncCommand ChooseEndDateCommand => new MvxAsyncCommand(SelectEndDate);

        private async Task SelectEndDate()
        {
            var vm = new ChooseDateViewModel(_navigationService, this, false);
            await _navigationService.Navigate(vm);
        }

        public IMvxAsyncCommand ChooseStartTimeCommand => new MvxAsyncCommand(SelectStartTime);

        private async Task SelectStartTime()
        {
            var vm = new ChooseTimeViewModel(_navigationService, this, true);
            await _navigationService.Navigate(vm);
        }

        public IMvxAsyncCommand ChooseEndTimeCommand => new MvxAsyncCommand(SelectEndTime);

        private async Task SelectEndTime()
        {
            var vm = new ChooseTimeViewModel(_navigationService, this, false);
            await _navigationService.Navigate(vm);
        }


        public IMvxAsyncCommand SaveBlockCommand => new MvxAsyncCommand(SaveBlock);

        private async Task SaveBlock()
        {
            Loading = true;
            var start = StartDate.Add(StartTime);
            var end = EndDate.Add(EndTime);
            if (end <= start)
            {
                await FormsApp.Current.MainPage.DisplayAlert("Invalid dates", "The end date/time must be after the start.", "OK");
            }
            else
            {
                var holiday = Mapper.Map<Rest.Dto.Holiday>(this);
                var sessionData = await FormsApp.GetSessionTokenAndMerchantGuid();
                bool success;
                if (FormsApp.CurrentBlockId == null)
                {
                    var response = await _holidayService.Post(holiday, sessionData.SessionToken, sessionData.MerchantGuid);
                    success = response.IsSuccessful;
                }
                else
                {
                    holiday.Id = Convert.ToInt32(FormsApp.CurrentBlockId);
                    var response = await _holidayService.Put(holiday, sessionData.SessionToken, sessionData.MerchantGuid);
                    success = response.IsSuccessful;
                }
                if (success)
                {
                    FormsApp.CurrentBlockId = null;
                    await _navigationService.Close(this);
                }
                else
                {
                    await FormsApp.Current.MainPage.DisplayAlert("An error occurred", "The block could not be saved.", "OK");
                }
            }
            Loading = false;
        }

        public IMvxAsyncCommand DeleteBlockCommand => new MvxAsyncCommand(DeleteBlock);

        private async Task DeleteBlock()
        {
            var sessionData = await FormsApp.GetSessionTokenAndMerchantGuid();
            await _holidayService.Delete(Convert.ToInt32(FormsApp.CurrentBlockId), sessionData.MerchantGuid, sessionData.SessionToken);
            await _navigationService.Close(this);
        }

        public string DeleteIconName
        {
            get
            {
                if (_currentBlockId.HasValue)
                {
                    return "imgTrash";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public async override void Start()
        {
            base.Start();
            int currentBlockId;
            if (_currentBlockId != null && Int32.TryParse(FormsApp.CurrentBlockId, out currentBlockId))
            {
                Loading = true;
                var sessionData = await FormsApp.GetSessionTokenAndMerchantGuid();
                var request = _holidayService.Get(currentBlockId, sessionData.MerchantGuid, sessionData.SessionToken);
                var response = await request;
                if (response.IsSuccessful)
                {
                    var block = response.Data;
                    Title = block.Title;
                    StartDate = block.StartDate.Date;
                    EndDate = block.EndDate.Date;
                    StartTime = block.StartDate.TimeOfDay;
                    EndTime = block.EndDate.TimeOfDay;
                    var employees = await FormsApp.Database.Employees.GetEntitiesAsync();
                    if (block.EmployeeId.HasValue && employees.Any(e => e.Id == block.EmployeeId.Value));
                    {
                        Employee = Mapper.Map<EmployeeViewModel>(employees.FirstOrDefault(s => s.Id == block.EmployeeId));
                    }
                    VisibleToCustomers = block.VisibleToCustomers;
                    InformationalOnly = block.InformationalOnly;
                }
                Loading = false;
            }
        }


    }
}
