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
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class RootPage : MvxMasterDetailPage<RootViewModel>
    {
        public RootPage()
        {
            InitializeComponent();
            Title = "Root Page";
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            
        }
    }
}