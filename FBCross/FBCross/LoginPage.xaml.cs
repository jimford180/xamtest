using FBCross.Rest;
using FBCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public LoginViewModel Model;
		public LoginPage ()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(AgendaListView)).Assembly,
            "FBCross.Styles.global.css"));
            BackgroundColor = Color.FromRgb(13, 130, 219);
            Model = new LoginViewModel();
            BindingContext = Model;
            loginButton.Clicked += LoginButtonClicked;
		}

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            this.IsBusy = true;
            var sessionAuthClient = new SessionAuth();
            var loginResult = await sessionAuthClient.Get(Model.Email, Model.Password);
            this.IsBusy = false;
            if (loginResult.IsSuccessful && loginResult.Data.Any())
            {
                foreach (var session in loginResult.Data)
                {
                    await App.Database.Sessions.CreateEntityAsync(session);
                }
                await Navigation.PushModalAsync(new MainMasterDetail());
            }
            else
            {
                await DisplayAlert("Login Failed", "Your email address and password combination was not recognized.", "OK");
            }
        }

    }
}