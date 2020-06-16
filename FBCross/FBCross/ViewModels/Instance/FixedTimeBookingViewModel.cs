using System.Collections.Generic;

namespace FBCross.ViewModels.Instance
{
    public class FixedTimeBookingViewModel : ViewModelBase
    {
        public string BookingId { get => _bookingId; set { _bookingId = value; RaisePropertyChanged(() => BookingId); } }
        public string FirstName { get => _firstName; set { _firstName = value; RaisePropertyChanged(() => FirstName); } }
        public string LastName { get => _lastName; set { _lastName = value; RaisePropertyChanged(() => LastName); } }
        public string NumberOfSlots { get => _numberOfSlots; set { _numberOfSlots = value; RaisePropertyChanged(() => NumberOfSlots); } }
        public string Email { get => _email; set { _email = value; RaisePropertyChanged(() => Email); } }
        public string Phone { get => _phone; set { _phone = value; RaisePropertyChanged(() => Phone); } }
        public List<string> CustomFieldValues { get => _customFieldValues; set { _customFieldValues = value; RaisePropertyChanged(() => CustomFieldValues); } }

        public string TopLineText { get => string.Format("{0} {1} ({2})", _firstName, _lastName, _numberOfSlots); }
        public string SecondLineText { get => string.Format("{0} | {1}", _email, _phone); }
        public string ThirdLineText { get => string.Join("|", _customFieldValues); }

        private string _bookingId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        private string _numberOfSlots;
        private List<string> _customFieldValues;

    }
}
