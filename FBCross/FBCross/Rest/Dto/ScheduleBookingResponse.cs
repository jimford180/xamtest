using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ScheduleBookingResponse : ResponseBase
    {
        public ScheduleBookingDetail Details { get; set; }
        public Guid ConfirmationGuid { get; set; }
        public string ConfirmAdminOverBook { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

    }
}
