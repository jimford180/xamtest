using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Data
{
    public class MasterSchedule
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? SecondEmployeeId { get; set; }
        public string ServiceIds { get; set; }
        public int BufferTimeInMinutes { get; set; }
        public bool UsesRooms { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? LocationId { get; set; }
        public string TimeZone { get; set; }

        public string ExternalEmployeeId { get; set; }
        public string ExternalLocationId { get; set; }

        public bool Recurs { get; set; }
    }
}
