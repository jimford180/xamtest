using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Customer
{
    public class Customer : ViewModelBase
    {
        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(() => FirstName); RaisePropertyChanged(() => FullName); } }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(() => LastName); RaisePropertyChanged(() => FullName); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(() => Email); RaisePropertyChanged(() => ContactSummary); } }
        public string Phone { get => _phone; set { _phone = value; RaisePropertyChanged(() => Phone); RaisePropertyChanged(() => ContactSummary); } }
        public string Notes { get => _notes; set { _notes = value; RaisePropertyChanged(() => Notes); } }
        public int Id { get => _id; set { _id = value; RaisePropertyChanged(() => Id); } }
        public List<UnifiedField> CustomFields { get => _customFields; set { _customFields = value; } }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        public string ContactSummary
        {
            get { return string.Format("{0} {1}", Email, Phone); }
        }

        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _notes;
        private List<UnifiedField> _customFields;
    }
}
