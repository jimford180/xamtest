using FBCross.ViewModels.Appointment;
using MvvmCross.Forms.Views;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAppointmentLocation : MvxContentPage<ChooseLocationViewModel>
	{
		public SelectAppointmentLocation()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SelectAppointmentLocation)).Assembly,
            "FBCross.Styles.global.css"));
        }
	}
}