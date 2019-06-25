using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Binding.Intercom.Android;
using Binding.Intercom.Android.Identity;
using FBCross.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(FBCross.Droid.IntercomAndroid))]
namespace FBCross.Droid
{
    public class IntercomAndroid : IIntercom
    {
        public void DeregisterUser()
        {
            Intercom.Client().SetLauncherVisibility(Intercom.Visibility.Gone);
        }

        public void RegisterLoggedInUser(string email)
        {
            Registration registration = Registration.Create().WithEmail(email);
            Intercom.Client().RegisterIdentifiedUser(registration);
            Intercom.Client().SetLauncherVisibility(Intercom.Visibility.Visible);
        }
    }
}