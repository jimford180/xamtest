using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class Schedule
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? SecondEmployeeId { get; set; }
        public List<int> ServiceIds { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<int> RoomIds { get; set; }
        public int BufferTimeInMinutes { get; set; }
        public bool UsesRooms { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? LocationId { get; set; }
        public string TimeZone { get; set; }

        public string ExternalEmployeeId { get; set; }
        public string ExternalLocationId { get; set; }

        public bool Recurs { get; set; }
        public List<ScheduleDay> Days { get; set; }
        public List<Dates> Dates { get; set; }

        public bool Delete { get; set; }
    }
    public class ScheduleDay
    {
        public int Day { get; set; }
        public List<Hour> Hours { get; set; }
    }

    public class Hour
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class Dates
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
