using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class BookingRequest
    {
        public string BookingTokenToCancel { get; set; }
        public string ClassInstanceSlug { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public bool RemindByEmail { get; set; }
        public bool RemindBySms { get; set; }
        public int NumberOfSlots { get; set; }

        public int NumberOfRecurrences { get; set; } // 0 if not recurring
        public string RecurrenceType { get; set; } // Either "count" or "date"
        public DateTime? RecurrenceEndDate { get; set; } // Only used if the above type is "date"
        public string RecurrenceFrequency { get; set; } //"weekly" or "monthly"
        public int RecurrenceInterval { get; set; }
        public bool RecursMonday { get; set; }
        public bool RecursTuesday { get; set; }
        public bool RecursWednesday { get; set; }
        public bool RecursThursday { get; set; }
        public bool RecursFriday { get; set; }
        public bool RecursSaturday { get; set; }
        public bool RecursSunday { get; set; }
        public string CustomerNotes { get; set; }
        public int? UserId { get; set; }
        public string GymGuid { get; set; }
        public string SessionToken { get; set; }
        public string TimeZone { get; set; }
        public bool TimeZoneNET { get; set; }
        public string paymentToken { get; set; }
        public decimal? Price { get; set; }
        public bool? NoShowFee { get; set; }
        public string CreditCardMask { get; set; }
        public string Notes { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
        public string CustomFields { get; set; }
        public bool StoreInVault { get; set; }

        public bool OverBookOk { get; set; }

        public int? LanguageId { get; set; }

        public string RawQueryString { get; set; }
        public string NewPassword { get; set; }

        public int? CustomStatusId { get; set; }

        public string InternalSummary { get; set; }
    }
}
