using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FBCross.ViewModels.Month
{
    public class MonthViewModel : ViewModelBase
    {
        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get => _selectedDate; set
            {
                _selectedDate = value;
                FormsApp.SelectedDate = value;
                MessagingCenter.Send<MonthViewModel>(this, "DateSelected");
                RaisePropertyChanged(() => SelectedDate);
            }
        }
    }
}
