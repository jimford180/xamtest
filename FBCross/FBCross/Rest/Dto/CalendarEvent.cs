using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class CalendarEvent
    {
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public bool allDay { get; set; }

        public string url { get; set; }
        public string className { get; set; }
        public int slotsUsed { get; set; }
        public string color { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public string textColor { get; set; }
        public bool available { get; set; }
    }
}
