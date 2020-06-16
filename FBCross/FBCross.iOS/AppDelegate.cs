using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MvvmCross.Forms.Platforms.Ios.Core;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.ListView.XForms.iOS;

namespace FBCross.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<App, FormsApp>, App, FormsApp>
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            UINavigationBar.Appearance.TintColor = UIColor.Black;
            new SfCalendarRenderer();
            new SfPickerRenderer();
            SfListViewRenderer.Init();
            SfCheckBoxRenderer.Init();
            Intercom.SetApiKey("ios_sdk-b3c61968e38ab16d11f4dd50972d09c67c6f38aa", "zyx82osk");
            var result = base.FinishedLaunching(uiApplication, launchOptions);
            return result;
        }
    }
}
