using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Agenda
{
    public class AgendaItem : MvxViewModel
    {
        private string _startTime { get; set; }
        public string StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                RaisePropertyChanged(() => StartTime);
            }
        }
        private string _endTime { get; set; }
        public string EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                RaisePropertyChanged(() => EndTime);
            }
        }
        private string _url { get; set; }
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                RaisePropertyChanged(() => Url);
            }
        }
        private string _title { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }
        private string _employee { get; set; }
        public string Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                RaisePropertyChanged(() => Employee);
            }
        }
        
    }
}
