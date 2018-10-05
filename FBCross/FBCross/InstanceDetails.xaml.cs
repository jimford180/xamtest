using FBCross.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FBCross
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InstanceDetails : ContentPage
	{
        private string _instanceId;
        InstanceDetailsViewModel model;
		public InstanceDetails (string instanceId)
		{
			InitializeComponent ();
            model = new InstanceDetailsViewModel();
            BindingContext = model;
            _instanceId = instanceId;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            model.Load(_instanceId);
        }
    }
}