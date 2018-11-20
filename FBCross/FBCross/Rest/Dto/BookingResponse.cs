using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class BookingResponse : ResponseBase
    {
        public BookingDetail Details { get; set; }
        public Guid ConfirmationGuid { get; set; }
        public string ConfirmAdminOverBook { get; set; }
        public string PayWithPayPalToEmail { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
