using AutoMapper;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Appointment
{
    public class ChooseEmployeeViewModel : ViewModelBase
    {
        private readonly AppointmentViewModel _appointment;
        private readonly IMvxNavigationService _navigationService;
        private List<EmployeeViewModel> _allEmployees;

        public IMvxAsyncCommand<EmployeeViewModel> EmployeeSelectedCommand => new MvxAsyncCommand<EmployeeViewModel>(EmployeeSelected);
        public List<EmployeeViewModel> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }

        private async Task EmployeeSelected(EmployeeViewModel arg)
        {
            _appointment.Employee = arg;
            await _navigationService.Navigate(_appointment);
        }
        
        public ChooseEmployeeViewModel(AppointmentViewModel appointment, IMvxNavigationService navigationService)
        {
            _appointment = appointment;
            _navigationService = navigationService;
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var services = await FormsApp.Database.Employees.GetEntitiesAsync();
            AllEmployees = services.Select(e => Mapper.Map<EmployeeViewModel>(e)).ToList();
        }
    }
}
