using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Appointment
{
    public class AvailableTimeViewModel : ViewModelBase
    {
        private DateTime _dateTime;
        private string _classInstanceSlug;

        public DateTime DateTime { get => _dateTime; set { _dateTime = value; RaiseAllPropertiesChanged(); } }

        public string ClassInstanceSlug { get => _classInstanceSlug; set { _classInstanceSlug = value; RaisePropertyChanged(() => ClassInstanceSlug); } }

        public string Time
        {
            get => _dateTime.ToString("hh:mm tt");
        }
    }
}
