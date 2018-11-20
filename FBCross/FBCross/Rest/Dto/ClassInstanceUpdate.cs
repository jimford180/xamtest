using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ClassInstanceUpdate
    {
        public int TotalSlots { get; set; }
        public bool RequiresPayment { get; set; }
        public bool NoShowFee { get; set; }
        public decimal Price { get; set; }
        public bool LockPrice { get; set; }

        public int? EmployeeId { get; set; }
        public int? SecondEmployeeId { get; set; }

        public bool ReverseCancellation { get; set; }
    }

    public class ClassInstanceLocationUpdate
    {
        public int LocationId { get; set; }
    }
}
