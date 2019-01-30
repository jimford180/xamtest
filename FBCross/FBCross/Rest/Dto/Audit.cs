using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class Audit
    {
        public string Description { get; set; }
        public string ActionTakenDate { get; set; }
        public string ClassSessionSlug { get; set; }
        public int Type { get; set; }
        public int BookingType { get; set; }
        public int? EmployeeId { get; set; }
    }
}
