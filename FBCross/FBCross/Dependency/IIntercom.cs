using System;
using System.Collections.Generic;
using System.Text;

namespace FBCross.Dependency
{
    public interface IIntercom
    {
        void RegisterLoggedInUser(string email);
        void DeregisterUser();
    }
}
