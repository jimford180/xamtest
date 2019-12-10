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
        private List<UnifiedField> _fields;

        public IMvxAsyncCommand<Customer.Customer> CustomerSelectedCommand => new MvxAsyncCommand<Customer.Customer>(CustomerSelected);
        public ICommand SearchCustomersCommand => new MvxAsyncCommand(SearchCustomers);
        public ICommand SaveCustomerCommand => new MvxAsyncCommand(SaveCustomer);

        public string SaveCustomerText { get { return ShowSaveButton ? "SAVE" : ""; } }

        private async Task SaveCustomer()
        {
            if (Fields.Where(f => f.Required).All(f => !string.IsNullOrWhiteSpace(f.Value)))
            {
                _appointment.Customer = new Customer.Customer
                {
                    FirstName = Fields.Single(f => f.Type == FieldType.FirstName).Value,
                    LastName = Fields.Single(f => f.Type == FieldType.LastName).Value,
                    Email = Fields.Single(f => f.Type == FieldType.Email).Value,
                    Phone = Fields.Single(f => f.Type == FieldType.Phone).Value,
                    Notes = Fields.Single(f => f.Type == FieldType.Notes).Value,
                    CustomFields = Fields.Where(f => f.Type == FieldType.Custom).ToList()
                };
                CopyCustomFieldsFromCustomerToAppointment();
                await _navigationService.Close(this);
            }
            else
            {
                var missingFields = Fields.Where(f => f.Required && string.IsNullOrWhiteSpace(f.Value));
                var msg = $"The following fields are required: {string.Join(", ", missingFields.Select(f => f.Label))}";
                await FormsApp.Current.MainPage.DisplayAlert("Missing Required Fields", msg, "OK");
            }
        }

        private void CopyCustomFieldsFromCustomerToAppointment()
        {
            if (_appointment != null && _appointment.Customer != null && _appointment.Customer.CustomFields != null && _appointment.Fields != null) {
                foreach (var customerField in _appointment.Customer.CustomFields)
                {
                    var matchingField = _appointment.Fields.FirstOrDefault(g => g.FieldId == customerField.FieldId);
                    if (matchingField != null)
                    {
                        matchingField.Value = customerField.Value;
                    }
                }
            }
        }

        private async Task SearchCustomers()
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
            ShowAddButton = false;
            ShowSaveButton = true;
        }

       

        private async Task CustomerSelected(Customer.Customer arg)
        {
            //Populate the fields
            Fields.Single(f => f.Type == FieldType.FirstName).Value = arg.FirstName;
            Fields.Single(f => f.Type == FieldType.LastName).Value = arg.LastName;
            Fields.Single(f => f.Type == FieldType.Email).Value = arg.Email;
            Fields.Single(f => f.Type == FieldType.Phone).Value = arg.Phone;
            Fields.Single(f => f.Type == FieldType.Notes).Value = arg.Notes;
            
            ShowCustomerForm = true;
            ShowNoCustomerWarning = false;
            ShowAddButton = false;
            ShowSaveButton = true;
        }


        public string SearchTerm { get => _searchTerm; set { _searchTerm = value; RaisePropertyChanged(() => SearchTerm); } }

        public bool ShowCustomerForm { get => _showCustomerForm; set { _showCustomerForm = value; RaisePropertyChanged(() => ShowCustomerForm); RaisePropertyChanged(() => ShowSearchForm); } }
        public bool ShowSearchForm { get => !_showCustomerForm; }
        public bool ShowAddButton { get => _showAddButton; set { _showAddButton = value; RaisePropertyChanged(() => ShowAddButton); } }
        public bool ShowSaveButton { get => _showSaveButton; set { _showSaveButton = value; RaisePropertyChanged(() => ShowSaveButton); RaisePropertyChanged(() => SaveCustomerText); } }
        public bool ShowNoCustomerWarning { get => _showNoCustomerWarning; set { _showNoCustomerWarning = value; RaisePropertyChanged(() => ShowNoCustomerWarning); } }

        public List<Customer.Customer> Customers { get => _customers; set { _customers = value; RaisePropertyChanged(() => Customers); } }
        public List<UnifiedField> Fields { get => _fields; set { _fields = value; RaisePropertyChanged(() => Fields); } }

        private bool _showAddButton;
        private bool _showSaveButton;

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
            ShowCustomerForm = false;
            ShowSaveButton = false;
            var fieldRules = FormsApp.MerchantFieldRules;
            Fields = new List<UnifiedField>
            {
                new UnifiedField(FieldType.FirstName, null, string.Empty, "First Name", Rest.Dto.CustomFieldType.TextField, fieldRules.StandardFieldRules.FirstNameRequired),
                new UnifiedField(FieldType.LastName, null, string.Empty, "Last Name", Rest.Dto.CustomFieldType.TextField, fieldRules.StandardFieldRules.LastNameRequired),
                new UnifiedField(FieldType.Email, null, string.Empty, "Email Address", Rest.Dto.CustomFieldType.TextField, fieldRules.StandardFieldRules.EmailRequired),
                new UnifiedField(FieldType.Phone, null, string.Empty, "Phone", Rest.Dto.CustomFieldType.TextField, fieldRules.StandardFieldRules.PhoneRequired),
                new UnifiedField(FieldType.Notes, null, string.Empty, "Customer Notes", Rest.Dto.CustomFieldType.LargeTextField, false),
            };
            Fields.AddRange(fieldRules.CustomFields.Select(f => new UnifiedField(f)));
        }

       
    }
}
