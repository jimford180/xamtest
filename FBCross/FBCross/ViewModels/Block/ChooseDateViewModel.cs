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
    public class ChooseDateViewModel : ViewModelBase
    {
        private IMvxNavigationService _navigationService;
        private BlockViewModel _block;
        private bool _isStartTime;
        private DateTime _date;


        public ChooseDateViewModel(IMvxNavigationService navigationService, BlockViewModel block, bool isStartTime)
        {
            _navigationService = navigationService;
            _block = block;
            _isStartTime = isStartTime;

            DateTime dt;
            if (_isStartTime)
            {
                dt = _block.StartDate;
            }
            else
            {
                dt = _block.EndDate;
            }

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Date.Month).Substring(0, 3));
            if (dt.Date.Day < 10)
                todaycollection.Add("0" + dt.Date.Day);
            else
                todaycollection.Add(dt.Date.Day.ToString());
            todaycollection.Add(dt.Date.Year.ToString());
            SelectedDate = todaycollection;
        }

        public DateTime Date { get => _date; set { _date = value; RaisePropertyChanged(() => Date); } }

        private ObservableCollection<object> _selectedDate;

        public ObservableCollection<object> SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                if (_selectedDate.Count == 3)
                {
                    var dateString = string.Format("{0} {1} {2}", _selectedDate[0], _selectedDate[1], _selectedDate[2]);
                    Date = Convert.ToDateTime(dateString);
                }
                RaisePropertyChanged("SelectedDate");
            }
        }

        public IMvxAsyncCommand DoneCommand => new MvxAsyncCommand(Done);

        private async Task Done()
        {
            if (_isStartTime)
                _block.StartDate = Date;
            else
                _block.EndDate = Date;
            await _navigationService.Close(this);
        }
        
        public string PageTitle {  get { return _isStartTime ? "Choose Block Start Date" : "Choose Block End Date"; } }
    }
}
