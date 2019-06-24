using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.ViewModels
{
    public class SessionInformation
    {
        public string SessionToken { get; set; }
        public Guid MerchantGuid { get; set; }
        public string Email { get; set; }
    }
}
