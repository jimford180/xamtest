using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class WaitListDetail
    {
        public int WaitListId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberId { get; set; }
        public int NumberOfSlots { get; set; }
        public bool CurrentlyHoldingSlots { get; set; }
        public DateTime? HoldingSlotsUntil { get; set; }
        public string Notes { get; set; }
        public List<CustomBookingField> CustomBookingFields { get; set; }
    }
}
