using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class WaitListRequest
    {
        public string ClassInstanceSlug { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public bool RemindByEmail { get; set; }
        public bool RemindBySms { get; set; }
        public int NumberOfSlots { get; set; }
        public int? UserId { get; set; }
        public bool MadeByMerchant { get; set; }
        public string TimeZone { get; set; }
        public string Notes { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
        public string CustomFields { get; set; }
        public int? LanguageId { get; set; }
    }
}
