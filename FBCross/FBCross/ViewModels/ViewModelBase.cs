using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace FBCross.ViewModels
{
    public class ViewModelBase : MvxViewModel
    {
        public ViewModelBase()
        {

        }

        private bool _loading { get; set; }
        public  bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                RaisePropertyChanged(() => Loading);
            }
        }
    }
}
