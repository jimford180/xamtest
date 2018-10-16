using FBCross.ViewModels.Navigation;
using MvvmCross.Forms.Presenters.Attributes;
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
	[MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class LeftMenu : MvxContentPage<MenuViewModel>
    {
		public LeftMenu ()
		{
            Title = "FlexBooker";
            InitializeComponent ();
		}
	}
}