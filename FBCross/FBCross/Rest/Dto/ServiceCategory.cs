using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ServiceCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> EmployeeIds { get; set; }
    }
}
