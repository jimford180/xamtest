using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Navigation
{
    public class MenuItem : ViewModelBase
    {
        private string _name { get; set; }
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }
        private PageType _pageType { get; set; }
        public PageType PageType
        {
            get => _pageType;
            set
            {
                _pageType = value;
                RaisePropertyChanged(() => PageType);
            }
        }
    }
}
