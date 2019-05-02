using AutoMapper;
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
    public class ChooseEmployeeViewModel : ViewModelBase
    {
        private readonly BlockViewModel _appointment;
        private readonly IMvxNavigationService _navigationService;
        private List<EmployeeViewModel> _allEmployees;

        public IMvxAsyncCommand<EmployeeViewModel> EmployeeSelectedCommand => new MvxAsyncCommand<EmployeeViewModel>(EmployeeSelected);
        public List<EmployeeViewModel> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }

        private async Task EmployeeSelected(EmployeeViewModel arg)
        {
            _appointment.Employee = arg;
            await _navigationService.Close(this);
        }
        
        public ChooseEmployeeViewModel(BlockViewModel appointment, IMvxNavigationService navigationService)
        {
            _appointment = appointment;
            _navigationService = navigationService;
            LoadEmployees();
        }

        private async void LoadEmployees()
        {
            var services = await FormsApp.Database.Employees.GetEntitiesAsync();
            AllEmployees = new List<EmployeeViewModel> { new EmployeeViewModel { Id = 0, Name = "All Employees" } };
            AllEmployees.AddRange(services.Select(e => Mapper.Map<EmployeeViewModel>(e)).ToList());
        }
    }
}
