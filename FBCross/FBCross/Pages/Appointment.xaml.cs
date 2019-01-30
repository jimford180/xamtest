using FBCross.ViewModels.Appointment;
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
    public partial class Appointment : MvxContentPage<AppointmentViewModel>
    {
        public Appointment()
        {
             InitializeComponent();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(InstanceDetails)).Assembly,
            "FBCross.Styles.global.css"));

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<AppointmentViewModel>(this, "FailGoToDateTimeChoice", ShowAlert);
        }

        private void ShowAlert(AppointmentViewModel obj)
        {
            DisplayAlert("Cannot Choose Date/Time", "Please choose a service and employee before choosing a time.", "OK");
        }
    }
}