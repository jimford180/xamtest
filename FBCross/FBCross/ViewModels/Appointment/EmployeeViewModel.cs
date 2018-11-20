using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels.Appointment
{
    public class EmployeeViewModel : ViewModelBase
    {
        private int _id;
        private string _name;
        private Guid _employeeGuid;

        public int Id { get => _id; set { _id = value; RaisePropertyChanged(() => Id); } }
        public string Name { get => _name; set { _name = value; RaisePropertyChanged(() => Name); } }
        public Guid EmployeeGuid { get => _employeeGuid; set { _employeeGuid = value; RaisePropertyChanged(() => EmployeeGuid); } }
    }
}
