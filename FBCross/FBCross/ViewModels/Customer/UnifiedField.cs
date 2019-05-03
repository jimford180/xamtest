using FBCross.Rest.Dto;
using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Customer
{
    public class UnifiedField : ViewModelBase
    {
        private const string _chooseOneText = "Choose one...";
        private string _value;
        private bool _isPickerOpen;
        private bool _required;

        public FieldType Type { get; set; }
        public int? FieldId { get; set; }
        public string Value { get => _value; set { _value = value; RaisePropertyChanged(() => Value); RaisePropertyChanged(() => LabelValue); RaisePropertyChanged(() => LabelColor); } }
        public string Label { get; set; }
        public bool Required { get => _required; set { _required = value; RaisePropertyChanged(() => Required); } }

        public CustomFieldType FieldType { get; set; }
        public ObservableCollection<string> DropdownOptions { get; set; }
        public ObservableCollection<CheckboxListItem> CheckBoxItems { get ;set; }

        public string Keyboard
        {
            get
            {
                if (Type == ViewModels.Customer.FieldType.Email)
                    return "Email";
                if (Type == ViewModels.Customer.FieldType.Phone)
                    return "Telephone";
                return "Default";
            }
        }

        public bool IsPickerOpen { get => _isPickerOpen; set { _isPickerOpen = value; RaisePropertyChanged(() => IsPickerOpen); RaisePropertyChanged(() => LabelValue); } }
        public string LabelValue { get
            {
                if (!string.IsNullOrEmpty(Value) && Value != _chooseOneText)
                {
                    return Value;
                }
                return Label + (_required ? "*" : "");
            }
        }
        public string LabelColor
        {
            get
            {
                if (!string.IsNullOrEmpty(Value) && Value != _chooseOneText)
                {
                    return "#000000";
                }
                return "#666666";
            }
        }
        public IMvxCommand OpenPickerCommand => new MvxCommand(OpenPicker);

        public void UpdateValueForCheckboxItems()
        {
            var value = string.Join(",", CheckBoxItems.Where(i => i.Checked).Select(i => i.Text));
            Value = value;
        }

        private void OpenPicker()
        {
            IsPickerOpen = true;
            if (FieldType == CustomFieldType.DateField)
            {
                var dateString = string.Format("{0} {1} {2}", _selectedDate[0], _selectedDate[1], _selectedDate[2]);
                if (Convert.ToDateTime(dateString) == new DateTime(1990, 1, 1))
                {
                    ObservableCollection<object> todaycollection = new ObservableCollection<object>();
                    todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
                    if (DateTime.Now.Date.Day < 10)
                        todaycollection.Add("0" + DateTime.Now.Date.Day);
                    else
                        todaycollection.Add(DateTime.Now.Date.Day.ToString());
                    todaycollection.Add(DateTime.Now.Date.Year.ToString());
                    SelectedDate = todaycollection;
                }
            }
        }
        private ObservableCollection<object> _selectedDate;
        private CustomField f;

        public ObservableCollection<object> SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                if (_selectedDate.Count == 3)
                {
                    var dateString = string.Format("{0} {1} {2}", _selectedDate[0], _selectedDate[1], _selectedDate[2]);
                    if (Convert.ToDateTime(dateString) != new DateTime(1990, 1, 1)) {
                        Value = Convert.ToDateTime(dateString).ToShortDateString();
                    }
                }
                RaisePropertyChanged("SelectedDate");

            }
        }

        public UnifiedField()
        {

        }
        public UnifiedField(FieldType type, int? fieldId, string value, string label, CustomFieldType fieldType, bool required, List<string> dropdownOptions = null)
        {
            Type = type;
            FieldId = fieldId;
            Value = value;
            Label = label;
            FieldType = fieldType;
            Required = required;
            if (dropdownOptions != null && dropdownOptions.Any())
            {
                var optionsWithLabel = new List<string> { _chooseOneText };
                optionsWithLabel.AddRange(dropdownOptions);
                DropdownOptions = new ObservableCollection<string>(optionsWithLabel);
                Value = _chooseOneText;
                if (FieldType == CustomFieldType.MultiCheckbox)
                {
                    CheckBoxItems = new ObservableCollection<CheckboxListItem>(dropdownOptions.Select(i => new CheckboxListItem(i, () => { UpdateValueForCheckboxItems(); })));
                }
            }
        }

        public UnifiedField(CustomField f)
        {
            Type = ViewModels.Customer.FieldType.Custom;
            FieldId = f.Id;
            Value = string.Empty;
            Label = f.Label;
            FieldType = f.FieldType;
            Required = f.IsRequired;
        }
    }
    public enum FieldType
    {
        FirstName,
        LastName,
        Email,
        Phone,
        Notes,
        Custom
    }

}
