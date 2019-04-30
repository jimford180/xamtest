using AutoMapper;
using FBCross.Data;
using FBCross.ViewModels.Appointment;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Instance
{
    public class ChooseInstanceEmployeeViewModel : ViewModelBase
    {
        private readonly InstanceDetailsViewModel _instance;
        private readonly IMvxNavigationService _navigationService;
        private readonly EmployeeType _type;
        private List<Employee> _allEmployees;

        public IMvxAsyncCommand<Employee> EmployeeSelectedCommand => new MvxAsyncCommand<Employee>(EmployeeSelected);
        public List<Employee> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }
        
        private async Task EmployeeSelected(Employee arg)
        {
            if (_type == EmployeeType.Primary)
                _instance.Employee = arg;
            else
                _instance.SecondEmployee = arg;
            await _navigationService.Close(this);
        }
        
        public ChooseInstanceEmployeeViewModel(InstanceDetailsViewModel instance, IMvxNavigationService navigationService, EmployeeType type)
        {
            _instance = instance;
            _navigationService = navigationService;
            _type = type;
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var employees = await FormsApp.Database.Employees.GetEntitiesAsync();
            AllEmployees = employees.ToList();
        }
    }
}
