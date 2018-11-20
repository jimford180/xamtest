using FBCross.ViewModels.Authentication;
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
    [MvxContentPagePresentation(WrapInNavigationPage = false, NoHistory =true, Title ="Choose Merchant")]
    public partial class MerchantSelectionPage : MvxContentPage<MerchantChoiceViewModel>
	{
		public MerchantSelectionPage()
		{
			InitializeComponent ();
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(MerchantSelectionPage)).Assembly,
            "FBCross.Styles.global.css"));
            BackgroundColor = Color.FromRgb(13, 130, 219);
            

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        
    }
}