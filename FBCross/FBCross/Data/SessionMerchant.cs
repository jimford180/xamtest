﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Data
{
    public class SessionMerchant
    {
        public Guid MerchantGuid { get; set; }
        public string MerchantName { get; set; }
        public string SessionToken { get; set; }
    }
}