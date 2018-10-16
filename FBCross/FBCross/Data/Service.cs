using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Data
{
    public class Service
    {
        public int Id { get; set; }
        public Guid ServiceGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryId { get; set; }
        public string Location { get; set; }
        public int Duration { get; set; }
        public bool ConfirmBookingsManually { get; set; }
        public bool AllowWaitList { get; set; }
        public bool RequiresPayment { get; set; }
        public bool NoShowFee { get; set; }
        public bool IsAvailableWithPackageOnly { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice { get; set; }
        public bool AllowOverlap { get; set; }
        public int AllowOverlapAfterMinutes { get; set; }
        public string LegendColor { get; set; }
        public string PriceType { get; set; }
        public bool PurchaseIsOptional { get; set; }

        public int? MinBookingTimeBeforeApptMinutes { get; set; }
        public int? MaxBookingTimeBeforeApptDays { get; set; }
        public int? MinTimeBeforeAppointmentToEditMinutes { get; set; }

        public bool Delete { get; set; }

        public int? RateLimitAmount { get; set; }
        public bool RateLimitIsPercentage { get; set; }
    }
}
