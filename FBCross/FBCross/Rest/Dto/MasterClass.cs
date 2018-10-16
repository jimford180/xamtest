using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class MasterClass
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public Guid ServiceGuid { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool Recurs { get; set; }
        public int? NumberRecurrences { get; set; }
        public string Time { get; set; }
        public int Slots { get; set; }
        public List<string> SessionTimesMonday { get; set; }
        public List<string> SessionTimesTuesday { get; set; }
        public List<string> SessionTimesWednesday { get; set; }
        public List<string> SessionTimesThursday { get; set; }
        public List<string> SessionTimesFriday { get; set; }
        public List<string> SessionTimesSaturday { get; set; }
        public List<string> SessionTimesSunday { get; set; }
        public List<string> OneOffDateTimes { get; set; }
        public int? CategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServiceDuration { get; set; }
        public bool ServiceRequiresPayment { get; set; }
        public bool ServiceNoShowFee { get; set; }
        public decimal ServicePrice { get; set; }
        public int ServiceId { get; set; }
        public string ServiceColor { get; set; }

        public List<DayOfWeek> DaysOfRecurrence { get; set; }

        public int RecurWeeks { get; set; }
        public Guid GymGuid { get; set; }
        public string EmployeeName { get; set; }
        public int? EmployeeUserId { get; set; }
        public int? SecondEmployeeId { get; set; }
        public int? LocationId { get; set; }
        public bool AllowWaitlist { get; set; }
        public bool ConfirmBookingsManually { get; set; }
        public string CutOffDate { get; set; }
        public DateTime? CutOffDateTime { get; set; }
        public string PostBookingInstructions { get; set; }

        public bool Delete { get; set; }
    }
    }
