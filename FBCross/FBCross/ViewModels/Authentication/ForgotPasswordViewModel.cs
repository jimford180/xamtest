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
    public class ForgotPasswordViewModel : ViewModelBase
    {

        public ForgotPasswordViewModel(IMvxNavigationService navigationService, IForgotPassword forgotPasswordService)
        {
            _navigationService = navigationService;
            _forgotPasswordService = forgotPasswordService;
        }
        private string _email;
        private readonly IMvxNavigationService _navigationService;
        private readonly IForgotPassword _forgotPasswordService;

        public string Email { get => _email; set { _email = value; RaisePropertyChanged(() => Email); } }

        public IMvxCommand SubmitCommand => new MvxCommand(Submit);


        private async void Submit()
        {
            Loading = true;
            ;
            var resetResult = await _forgotPasswordService.Post(new Rest.Dto.ForgotPasswordRequest { Email = _email });
            Loading = false;
            if (resetResult.IsSuccessful && !string.IsNullOrEmpty(resetResult.Data.Message))
            {
                await FormsApp.Current.MainPage.DisplayAlert("Password Reset", resetResult.Data.Message, "Okay");
                await _navigationService.Navigate<LoginViewModel>();
            }
            else
            {
                await FormsApp.Current.MainPage.DisplayAlert("Could not reset password", "An error occurred, and we could not reset your password. Please try again.", "Okay");
            }

        }
    }

   
}
