using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string HexColorForCalendar { get; set; }
        public bool IsAdmin { get; set; }

        public bool SendDailyRecap { get; set; }

        public string DailyRecapSendTime { get; set; }

        public bool IncludeNotesInDailyRecap { get; set; }

        public bool SendBookingChanges { get; set; }

        public DateTime? LastLogin { get; set; }

        public int? OptionalSortNumber { get; set; }

        public bool ViewReports { get; set; }
        public bool ViewOthersBookings { get; set; }
        public bool EditOthersBookings { get; set; }
        public bool ViewOthersSchedules { get; set; }
        public bool EditOthersSchedules { get; set; }
        public bool EditBusinessSettings { get; set; }
        public bool ViewCustomerList { get; set; }
        public bool EditCustomerList { get; set; }
        public bool EditServices { get; set; }
        public bool EditOwnBookings { get; set; }
        public bool EditOwnSchedules { get; set; }
        public bool EditBookingWidgets { get; set; }

        public string BookingFormUrl { get; set; }
    }
}
