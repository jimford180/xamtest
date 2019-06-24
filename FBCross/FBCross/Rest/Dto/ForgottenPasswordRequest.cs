using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Rest.Dto
{
    public class ForgotPasswordRequest
    {
        public string Error { get; set; }
        public string SuccessMessage { get; set; }
        public string Email { get; set; }
    }
}
