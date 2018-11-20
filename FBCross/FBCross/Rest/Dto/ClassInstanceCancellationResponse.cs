using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ClassInstanceCancellationResponse : ResponseBase
    {
        public bool RequiresRefunds { get; set; }
    }
}
