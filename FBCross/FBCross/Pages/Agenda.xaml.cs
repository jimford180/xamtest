using FBCross.ViewModels.Agenda;
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
    [MvxTabbedPagePresentation(TabbedPosition.Tab, Title="Agenda", Icon = "imgToday.png")]
    public partial class Agenda : MvxContentPage<AgendaViewModel>
	{
		public Agenda ()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(Agenda)).Assembly,
            "FBCross.Styles.global.css"));

        }
	}
}