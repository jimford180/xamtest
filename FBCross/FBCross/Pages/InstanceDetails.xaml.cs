using FBCross.ViewModels.Instance;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InstanceDetails : MvxContentPage<InstanceDetailsViewModel>
	{
		public InstanceDetails ()
		{
			InitializeComponent ();
		}
	}
}