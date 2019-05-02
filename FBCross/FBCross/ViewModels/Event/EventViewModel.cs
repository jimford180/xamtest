using FBCross.ViewModels.Shared;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Event
{
    public class EventViewModel : ViewModelBase
    {
        private ServiceViewModel _service;
        private EmployeeViewModel _employee;
        private LocationViewModel _location;
        private DateTime _dateTime;
        private readonly IMvxNavigationService _navigationService;

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

        public LocationViewModel Location{ get => _location; set { _location = value; RaisePropertyChanged(() => Employee); RaisePropertyChanged(() => LocationName); } }
        public string LocationName
        {
            get { return _location == null ? "Select Location" : _location.Name; }
        }


        public IMvxAsyncCommand ChooseServiceCommand => new MvxAsyncCommand(GoToServiceChoice);
        public IMvxAsyncCommand ChooseEmployeeCommand => new MvxAsyncCommand(GoToEmployeeChoice);
        public IMvxAsyncCommand ChooseLocationCommand => new MvxAsyncCommand(GoToLocationChoice);

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
        private async Task GoToLocationChoice()
        {
                var employeeChoice = new ChooseEmployeeViewModel(this, _navigationService);
                await _navigationService.Navigate(employeeChoice);
        }

        public EventViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
