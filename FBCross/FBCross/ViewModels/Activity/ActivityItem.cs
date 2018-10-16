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

        public string ActionText { get => _actionText; set { _actionText = value; RaisePropertyChanged(() => ActionText); } }
        public string Description { get => _description; set { _description = value; RaisePropertyChanged(() => Description); } }
    }
}
