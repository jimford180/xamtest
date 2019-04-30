using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBCross.ViewModels.Instance
{
    public class TotalSpotsViewModel : ViewModelBase
    {
        private InstanceDetailsViewModel _instance;
        private readonly IMvxNavigationService _navigationService;
        private List<SlotAmount> _allSpots = Enumerable.Range(1, 100).Select(n => new SlotAmount { Number = n }).ToList();
        public TotalSpotsViewModel(InstanceDetailsViewModel instance, IMvxNavigationService navigationService)
        {
            _instance = instance;
            _navigationService = navigationService;
            
        }

        public IMvxAsyncCommand<SlotAmount> SpotsSelectedCommand => new MvxAsyncCommand<SlotAmount>(SpotsSelected);

        private async Task SpotsSelected(SlotAmount arg)
        {
            _instance.TotalSpots = arg.Number;
            _instance.Save();
            await _navigationService.Close(this);

        }

        public List<SlotAmount> AllSpots { get => _allSpots; set { _allSpots = value; RaisePropertyChanged(() => AllSpots); } }
    }
    public class SlotAmount : ViewModelBase
    {
        private int _number;
        public int Number { get => _number; set { _number = value; RaisePropertyChanged(() => Number); } }
        public string Text { get { return _number.ToString() + (_number == 1 ? " person" : " people"); } }
    }
}
