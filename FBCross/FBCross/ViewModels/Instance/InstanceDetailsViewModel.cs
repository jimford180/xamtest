using FBCross.Data;
using FBCross.Rest;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                RaisePropertyChanged(() => EmployeeName);
            }
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
        public int TotalSpots { get => _totalSpots; set { _totalSpots = value; RaisePropertyChanged(() => TotalSpots); } }
        public string SecondEmployeeName { get => _secondEmployeeName; set { _secondEmployeeName = value; RaisePropertyChanged(() => SecondEmployeeName); } }
        public string InstanceDetails { get => _instanceDetails; set { _instanceDetails = value; RaisePropertyChanged(() => InstanceDetails); } }
        public List<Employee> AllEmployees { get => _allEmployees; set { _allEmployees = value; RaisePropertyChanged(() => AllEmployees); } }

        private string _serviceName;
        private string _employeeName;
        private string _dateTime;
        private int _spotsTaken;
        private int _totalSpots;
        private string _secondEmployeeName;
        private string _instanceDetails;
        private List<Employee> _allEmployees;

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
                DateTime = response.Data.Date;
                SpotsTaken = response.Data.SlotsUsed;
                TotalSpots = response.Data.TotalSlots;
                SecondEmployeeName = AllEmployees.SingleOrDefault(s => s.Id == response.Data.EmployeeId)?.Name;
                EmployeeName = AllEmployees.SingleOrDefault(s => s.Id == response.Data.SecondEmployeeId)?.Name;
            }

            Loading = false;
        }
    }
}
