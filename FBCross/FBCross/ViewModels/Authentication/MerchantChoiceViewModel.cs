using FBCross.Data;
using FBCross.Rest;
using FBCross.ViewModels.Navigation;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FBCross.ViewModels.Authentication
{
    public class MerchantChoiceViewModel : ViewModelBase
    {

        public MerchantChoiceViewModel(IMvxNavigationService navigationService, ISessionAuth sessionAuth)
        {
            _navigationService = navigationService;
            
        }
        private List<SessionMerchant> _merchants;
        private readonly IMvxNavigationService _navigationService;

        public List<SessionMerchant> Merchants { get => _merchants; set { _merchants = value; RaisePropertyChanged(() => Merchants); } }


        public IMvxAsyncCommand<SessionMerchant> MerchantSelectedCommand => new MvxAsyncCommand<SessionMerchant>(MerchantSelected);

        private async Task MerchantSelected(SessionMerchant item)
        {
            item.IsCurrent = true;
            await FormsApp.Database.Sessions.UpdateEntityAsync(item);
            await _navigationService.Navigate<RootViewModel>();
        }
        public async override void Start()
        {
            Merchants = await FormsApp.Database.Sessions.GetEntitiesAsync();
        }
    }

   
}
