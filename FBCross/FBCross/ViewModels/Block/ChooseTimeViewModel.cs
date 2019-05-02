using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Block
{
    public class ChooseTimeViewModel : ViewModelBase
    {
        private IMvxNavigationService _navigationService;
        private BlockViewModel _block;
        private bool _isStartTime;
        private TimeSpan _time;


        public ChooseTimeViewModel(IMvxNavigationService navigationService, BlockViewModel block, bool isStartTime)
        {
            _navigationService = navigationService;
            _block = block;
            _isStartTime = isStartTime;

            TimeSpan time;
            if (_isStartTime)
            {
                time = _block.StartTime;
            }
            else
            {
                time = _block.EndTime;
            }

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            var intHours = Convert.ToInt32(time.Hours);
            var minutes = Convert.ToInt32(time.Minutes);
            var hours = intHours > 12 ? intHours - 12 : intHours;
            if (hours < 10)
                todaycollection.Add("0" + hours.ToString());
            else
                todaycollection.Add(hours.ToString());
            if (minutes < 10)
                todaycollection.Add("0" + minutes.ToString());
            else
                todaycollection.Add(minutes.ToString());
            todaycollection.Add(time.TotalHours > 11 ? "PM" : "AM");
            SelectedTime = todaycollection;
        }

        public TimeSpan Time { get => _time; set { _time = value; RaisePropertyChanged(() => Time); } }

        private ObservableCollection<object> _selectedTime;

        public ObservableCollection<object> SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                if (_selectedTime.Count == 3)
                {
                    var dateString = string.Format("2019-01-01 {0}:{1} {2}", _selectedTime[0], _selectedTime[1], _selectedTime[2]);
                    Time = Convert.ToDateTime(dateString).TimeOfDay;
                }
                RaisePropertyChanged("SelectedTime");
            }
        }

        public IMvxAsyncCommand DoneCommand => new MvxAsyncCommand(Done);

        private async Task Done()
        {
            if (_isStartTime)
                _block.StartTime = Time;
            else
                _block.EndTime = Time;
            await _navigationService.Close(this);
        }
        
        public string PageTitle {  get { return _isStartTime ? "Choose Block Start Time" : "Choose Block End Time"; } }
    }
}
