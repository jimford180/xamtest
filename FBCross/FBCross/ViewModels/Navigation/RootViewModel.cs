using FBCross.Rest;
using FBCross.ViewModels.Agenda;
using FBCross.ViewModels.Authentication;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FBCross.ViewModels.Navigation
{
    public class RootViewModel : ViewModelBase
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly IMerchantState _merchantStateService;
        public RootViewModel(IMvxNavigationService navigationService, IMerchantState merchantStateService)
        {
            _navigationService = navigationService;
            _merchantStateService = merchantStateService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            RedirectIfNecessary();
            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async void RedirectIfNecessary()
        {
            var allTokens = await FormsApp.Database.Sessions.GetEntitiesAsync();
            if (!allTokens.Any())
            {
                await _navigationService.Navigate<LoginViewModel>();
            }
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<TabbedHomeViewModel>();
        }

        public override async void Start()
        {
            base.Start();
            Loading = true;
            var sessionInfo = await FormsApp.GetSessionTokenAndMerchantGuid();
            var response = await _merchantStateService.Get(sessionInfo.MerchantGuid, sessionInfo.SessionToken);
            if (response.IsSuccessful)
            {
                var employees = response.Data.Employees.Select(e => Mapper.Map<Data.Employee>(e));
                var services = response.Data.Services.Select(s => Mapper.Map<Data.Service>(s));
                await FormsApp.Database.Employees.RemoveAll();
                await FormsApp.Database.Employees.CreateManyAsync(employees);
                await FormsApp.Database.Services.RemoveAll();
                await FormsApp.Database.Services.CreateManyAsync(services);
            }

            Loading = false;
        }
    }
}
