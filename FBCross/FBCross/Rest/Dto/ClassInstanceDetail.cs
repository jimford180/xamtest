using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ClassInstanceDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsCancelled { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int SlotsUsed { get; set; }
        public int TotalSlots { get; set; }
        public List<BookingDetail> Bookings { get; set; }
        public List<WaitListDetail> WaitListParties { get; set; }
        public int NumberOfSlotsHeldForPayment { get; set; }
        public string EmployeeName { get; set; }
        public string Error { get; set; }
        public bool RequiresPayment { get; set; }
        public bool NoShowFee { get; set; }
        public decimal Price { get; set; }
        public bool LockPrice { get; set; }
        public bool ShowConfirmationForLocationChange { get; set; }
        public int? LocationId { get; set; }
        public string MasterRecurringDays { get; set; }

        public int? EmployeeId { get; set; }
        public int? SecondEmployeeId { get; set; }

        public int ServiceId { get; set; }
    }
}
