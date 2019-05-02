using FBCross.ViewModels.Agenda;
using MvvmCross.Forms.Views;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAgendaEmployee : MvxContentPage<ChooseEmployeeViewModel>
	{
		public SelectAgendaEmployee()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(SelectAppointmentService)).Assembly,
            "FBCross.Styles.global.css"));
        }
	}
}