using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ScheduleBookingRequest
    {
        public Guid EmployeeGuid { get; set; }
        public Guid? SecondEmployeeGuid { get; set; }

        public string ExternalId { get; set; }
        public string EmployeeExternalId { get; set; }
        public string LocationExternalId { get; set; }

        public int? CustomStatusId { get; set; }

        public int? LanguageId { get; set; }

        //Optional for auto assignment decision
        public List<AutoAssignmentOptions> AutoAssignmentEmployees { get; set; }
        public string AutoAssignmentFields { get; set; }

        public List<Guid> ServiceGuids { get; set; }
        public string ServiceGuidsCsv { get; set; }

        public int? Duration { get; set; }
        public int? RoomId { get; set; }
        public int? LocationId { get; set; }
        public string SessionDateTime { get; set; }
        
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public bool RemindByEmail { get; set; }
        public bool RemindBySms { get; set; }
        public int? UserId { get; set; }
        public string GymGuid { get; set; }
        public string SessionToken { get; set; }
        public string TimeZone { get; set; }
        public bool TimeZoneNET { get; set; }
        public string PaymentToken { get; set; }
        public decimal? Price { get; set; }
        public bool? NoShowFee { get; set; }
        public string CreditCardMask { get; set; }
        public string Notes { get; set; }
        public string CustomerNotes { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
        public string CustomFields { get; set; }
        public bool Cancelled { get; set; }

        public bool OverBookOk { get; set; }

        public bool IssueRefund { get; set; }
        public bool ChargeNoShowFee { get; set; }
        public bool NoShow { get; set; }

        public bool ChargeStoredCard { get; set; }
        public decimal AmountToCharge { get; set; }
        public string Memo { get; set; }

        public bool Cancellable { get; set; }

        public bool IsSeries { get; set; }

        public int PeriodStep { get; set; }
        public string PeriodType { get; set; }
        public int Recurrences { get; set; }
        public List<string> DateTimes { get; set; }
        public string RawQueryString { get; set; }
        public string NewPassword { get; set; }
        public bool InlinePayment { get; set; }
        public string AppliedPromoCode { get; set; }
        public bool SuppressEmail { get; set; }

        public string InternalSummary { get; set; }

    }
}
