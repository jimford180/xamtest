using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ScheduleBookingInfo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? SecondEmployeeId { get; set; }
        public string ExternalId { get; set; }
        public string EmployeeExternalId { get; set; }
        public int? CustomStatusId { get; set; }
        public List<int> ServiceIds { get; set; }
        public int? Duration { get; set; }
        public int? RoomId { get; set; }
        public DateTime SessionDateTime { get; set; }
        public string StringDateTime { get; set; }
        public DateTime ConvertedDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public bool RemindByEmail { get; set; }
        public bool RemindBySms { get; set; }
        public int? UserId { get; set; }
        public string GymGuid { get; set; }
        public string TimeZone { get; set; }
        public string Price { get; set; }
        public bool? NoShowFee { get; set; }
        public string CreditCardMask { get; set; }
        public string Notes { get; set; }
        public bool NoShowCharged { get; set; }
        public bool ChargeRefunded { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public string CustomerNotes { get; set; }
        public bool NoShow { get; set; }
        public bool MadeByMerchant { get; set; }
        public string MadeOnDate { get; set; }
        public List<Audit> AuditTrail { get; set; }
        public string StoredPaymentMask { get; set; }
        public string AmountPaid { get; set; }
        public bool IsSeries { get; set; }
        public int NumberOfOthersInSeries { get; set; }
        public List<OtherBookingSeriesDates> OtherSeriesDates { get; set; }
        public int PeriodStep { get; set; }
        public string PeriodType { get; set; }
        public int Recurrences { get; set; }
        public int? LocationId { get; set; }
        public string LocationExternalId { get; set; }
        public string LocationTimeZone { get; set; }
        public int? LanguageId { get; set; }
        public bool HasUsedPurchasedPackage { get; set; }
        public string PackageUsageNote { get; set; }
        public string EditUrl { get; set; }
        public string InternalSummary { get; set; }
    }
}
