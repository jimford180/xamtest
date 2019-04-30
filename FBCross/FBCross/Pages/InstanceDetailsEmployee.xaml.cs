using FBCross.ViewModels.Appointment;
using FBCross.ViewModels.Instance;
using MvvmCross.Forms.Views;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstanceDetailsEmployee : MvxContentPage<ChooseInstanceEmployeeViewModel>
	{
		public InstanceDetailsEmployee()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SelectAppointmentService)).Assembly,
            "FBCross.Styles.global.css"));
        }
	}
}