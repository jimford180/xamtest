using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBCross.Dependency;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FBCross.iOS.IntercomIos))]
namespace FBCross.iOS
{
    public class IntercomIos : IIntercom
    {
        public void DeregisterUser()
        {
            Intercom.SetLauncherVisible(false);
        }

        public void RegisterLoggedInUser(string email)
        {
            Intercom.RegisterUserWithEmail(email);
            Intercom.SetLauncherVisible(true);
        }
    }
}