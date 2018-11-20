using FBCross.ViewModels.Authentication;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = false, NoHistory =true, Title ="Sign In")]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
	{
		public LoginPage ()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(LoginPage)).Assembly,
            "FBCross.Styles.global.css"));
            BackgroundColor = Color.FromRgb(13, 130, 219);
            

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<LoginViewModel>(this, "LoginFailed", ShowAlert);
        }

        private void ShowAlert(LoginViewModel obj)
        {
            DisplayAlert("Login Failed", "Your email address and password combination was not recognized.", "OK");
        }
    }
}