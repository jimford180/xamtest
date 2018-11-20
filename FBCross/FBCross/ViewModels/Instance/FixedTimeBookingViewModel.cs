namespace FBCross.ViewModels.Instance
{
    public class FixedTimeBookingViewModel : ViewModelBase
    {
        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(() => FirstName); } }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(() => LastName); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(() => Email); } }
        public string Phone { get => _phone; set { _phone = value; RaisePropertyChanged(() => Phone); } }

        public string TopLineText { get => string.Format("{0} {1}", _firstName, _lastName); }
        public string BottomLineText { get => string.Format("{0} | {1}", _email, _phone); }

        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;

    }
}
