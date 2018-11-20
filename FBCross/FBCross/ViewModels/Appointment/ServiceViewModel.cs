using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Appointment
{
    public class ServiceViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private Guid _serviceGuid;

        public int Id { get => _id; set { _id = value; RaisePropertyChanged(() => Id); } }
        public string Name { get => _name; set { _name = value; RaisePropertyChanged(() => Name); } }
        public Guid ServiceGuid { get => _serviceGuid; set { _serviceGuid = value; RaisePropertyChanged(() => ServiceGuid); } }
    }
}
