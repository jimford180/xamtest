using System;

namespace FBCross.ViewModels.Customer
{
    public class CheckboxListItem : ViewModelBase
    {
        private string _itemText;
        private Action _callback;
        private bool _checked;

        public CheckboxListItem(string itemText, Action callback)
        {
            _itemText = itemText;
            _callback = callback;
        }

        public string Text { get => _itemText; set { _itemText = value; RaisePropertyChanged(() => Text); } }
        public bool Checked { get => _checked; set { _checked = value; RaisePropertyChanged(() => Checked); _callback(); } }
    }
}