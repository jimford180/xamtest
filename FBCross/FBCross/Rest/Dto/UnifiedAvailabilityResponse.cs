using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class UnifiedAvailabilityResponse : ResponseBase
    {
        public ScheduleType ScheduleType { get; set; }
        public List<AvailableTime> AvailableTimes { get; set; }
    }

    public enum ScheduleType
    {
        Fixed,
        Flexible,
        None
    }
    public class AvailableTime
    {
        public DateTime DateTime { get; set; }
        public string ClassInstanceSlug { get; set; }
    }
}
