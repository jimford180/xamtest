using AutoMapper;
using FBCross.Rest;
using FBCross.ViewModels.Customer;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FBCross.ViewModels.Appointment
{
    public class ChooseCustomerViewModel : ViewModelBase
    {
        private readonly AppointmentViewModel _appointment;
        private readonly IMvxNavigationService _navigationService;
        private readonly ICustomer _customerService;

        public IMvxAsyncCommand<Customer.Customer> CustomerSelectedCommand => new MvxAsyncCommand<Customer.Customer>(CustomerSelected);
        public ICommand SearchCustomersCommand => new Command(SearchCustomers);

        private async void SearchCustomers()
        {
            if (!string.IsNullOrEmpty(_searchTerm))
            {
                Loading = true;
                ShowNoCustomerWarning = false;
                var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
                var customerResponse = await _customerService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken, _searchTerm);
                if (customerResponse.IsSuccessful && customerResponse.Data.Customers != null)
                {
                    Customers = customerResponse.Data.Customers.Select(c => Mapper.Map<Customer.Customer>(c)).ToList();
                    if (Customers.Count == 0)
                        ShowNoCustomerWarning = true;
                }
                Loading = false;
            }
        }

        public IMvxAsyncCommand AddCustomerCommand => new MvxAsyncCommand(AddCustomer);

        private async Task AddCustomer()
        {
            ShowCustomerForm = true;
            ShowNoCustomerWarning = false;
        }

        private async Task CustomerSelected(Customer.Customer arg)
        {
            _appointment.Customer = arg;
            await _navigationService.Navigate(_appointment);
        }


        public string SearchTerm { get => _searchTerm; set { _searchTerm = value; RaisePropertyChanged(() => SearchTerm); } }

        public bool ShowCustomerForm { get => _showCustomerForm; set { _showCustomerForm = value; RaisePropertyChanged(() => ShowCustomerForm); RaisePropertyChanged(() => ShowSearchForm); } }
        public bool ShowSearchForm { get => !_showCustomerForm; }
        public bool ShowAddButton { get => _showAddButton; set { _showAddButton = value; RaisePropertyChanged(() => ShowAddButton); } }
        public bool ShowNoCustomerWarning { get => _showNoCustomerWarning; set { _showNoCustomerWarning = value; RaisePropertyChanged(() => ShowNoCustomerWarning); } }

        public List<Customer.Customer> Customers { get => _customers; set { _customers = value; RaisePropertyChanged(() => Customers); } }

        private bool _showAddButton;

        private string _searchTerm;

        private bool _showCustomerForm;
        private bool _showNoCustomerWarning;

        private List<Customer.Customer> _customers;

        public ChooseCustomerViewModel(AppointmentViewModel appointment, IMvxNavigationService navigationService, ICustomer customerService)
        {
            _appointment = appointment;
            _navigationService = navigationService;
            _customerService = customerService;
            ShowAddButton = true;
            ShowNoCustomerWarning = false;
            ShowCustomerForm = true;
        }

       
    }
}
