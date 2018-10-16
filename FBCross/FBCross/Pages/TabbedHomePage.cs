using FBCross.ViewModels.Month;
using FBCross.ViewModels.Navigation;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace FBCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, Title ="Tabbed")]
    //[MvxTabbedPagePresentation(TabbedPosition.Root)]
    public class TabbedHomePage : MvxTabbedPage<TabbedHomeViewModel>
	{
		public TabbedHomePage ()
		{

            Title = "FlexBooker";
        }
        private bool _firstTime = true;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (_firstTime)
            {
                //ViewModel.ShowInitialViewModelsCommand.Execute();
                ViewModel.ShowInitialViewModelsCommand.ExecuteAsync(null);
                _firstTime = false;
            }
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            this.CurrentPage = this.Children[1];
            MessagingCenter.Subscribe<MonthViewModel>(this, "DateSelected", GoToDayView);
        }

        private void GoToDayView(MonthViewModel obj)
        {
            this.CurrentPage = this.Children[1];
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            
        }
        
    }
}