using System;
using System.Collections.Generic;

namespace FBCross.Rest.Dto
{
    public class Customer
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MembershipNumber { get; set; }
        public string LastBookingTime { get; set; }
        public DateTime? LastBookingDate { get; set; }
        public int NumberOfBookings { get; set; }
        public int NumberOfSpaces { get; set; }
        public string UserStatus { get; set; }
        public bool IsConcierge { get; set; }
        public string Error { get; set; }
        public int EngagementScore { get; set; }
        public string EngagementExplain { get; set; }
        public int EngagementScoreLastMonth { get; set; }
        public string EngagementExplainLastMonth { get; set; }
        public string StoredPaymentMask { get; set; }
        public string Notes { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
        public List<string> Attachments { get; set; }
        public List<int> AssignedEmployeeIds { get; set; }

    }
}
