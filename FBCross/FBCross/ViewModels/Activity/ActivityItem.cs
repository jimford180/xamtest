using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Activity
{
    public class ActivityItem : MvxViewModel
    {
        private string _actionText;
        private string _description;
        private string _classSessionSlug;
        private int _bookingType;

        public string ActionText { get => _actionText; set { _actionText = value; RaisePropertyChanged(() => ActionText); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged(() => Description); } }

        public string ClassSessionSlug { get => _classSessionSlug; set => _classSessionSlug = value; }
        public int BookingType { get => _bookingType; set => _bookingType = value; }
    }
}
