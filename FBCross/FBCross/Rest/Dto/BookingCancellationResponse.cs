using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class BookingCancellationResponse : ResponseBase
    {
        public bool AskAboutOtherAppointmentsInSeries { get; set; }
        public bool AppointmentsInSeriesAction { get; set; }
    }
}
