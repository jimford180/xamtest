using FBCross.ViewModels.Block;
using MvvmCross.Forms.Views;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectBlockEmployee : MvxContentPage<ChooseEmployeeViewModel>
	{
		public SelectBlockEmployee()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SelectAppointmentService)).Assembly,
            "FBCross.Styles.global.css"));
        }
	}
}