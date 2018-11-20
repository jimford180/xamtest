using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ClassInstanceUpdateResponse : ResponseBase
    {
        public int TotalSlots { get; set; }
        public bool IsCancelled { get; set; }
    }

    public class ClassInstanceLocationUpdateResponse : ResponseBase
    {

    }
}
