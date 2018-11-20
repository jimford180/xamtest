using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Appointment
{
    public class AvailableTimeViewModel : ViewModelBase
    {
        private DateTime _dateTime;

        public DateTime DateTime { get => _dateTime; set { _dateTime = value; RaiseAllPropertiesChanged(); } }
        public string Time
        {
            get => _dateTime.ToString("hh:mm tt");
        }
    }
}
