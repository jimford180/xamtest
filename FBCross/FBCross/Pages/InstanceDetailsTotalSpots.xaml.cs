using FBCross.ViewModels.Instance;
using MvvmCross.Forms.Views;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstanceDetailsTotalSpots : MvxContentPage<TotalSpotsViewModel>
	{
		public InstanceDetailsTotalSpots()
		{
			InitializeComponent ();
            Title = "Choose number of spots";
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SelectAppointmentService)).Assembly,
            "FBCross.Styles.global.css"));
        }
	}
}