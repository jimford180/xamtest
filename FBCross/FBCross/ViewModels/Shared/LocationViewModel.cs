using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Shared
{
    public class LocationViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        public int Id { get => _id; set { _id = value; RaisePropertyChanged(() => Id); } }
        public string Name { get => _name; set { _name = value; RaisePropertyChanged(() => Name); } }
    }
}
