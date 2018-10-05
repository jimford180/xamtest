using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class BookingDetail
    {
        public string BookingId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public bool RemindByEmail { get; set; }
        public bool RemindBySms { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public string CreationInfo { get; set; }
        public int NumberOfSlots { get; set; }
        public decimal PriceCharged { get; set; }
        public bool NoShowFee { get; set; }
        public bool NoShowFeeCharged { get; set; }
        public bool Refunded { get; set; }
        public string ClassSlug { get; set; }
        public string ClassName { get; set; }
        public string Employee { get; set; }
        public string SessionDateTime { get; set; }
        public string Notes { get; set; }
        public string CustomerNotes { get; set; }
        public bool NoShow { get; set; }
        public bool MadeByMerchant { get; set; }
        public string MadeOnDate { get; set; }
        public string StoredPaymentMask { get; set; }

        public bool ChargeStoredCard { get; set; }
        public decimal AmountToCharge { get; set; }
        public string Memo { get; set; }
        public string AmountPaid { get; set; }

        public List<CustomBookingField> CustomBookingFields { get; set; }

        public int? CustomStatusId { get; set; }

        public DateTime? NewTime { get; set; }

        public bool HasUsedPurchasedPackage { get; set; }
        public string PackageUsageNote { get; set; }

        public string InternalSummary { get; set; }
    }
}
