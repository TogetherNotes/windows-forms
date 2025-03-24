using TogetherNotes.Models;

namespace TogetherNotes.ViewModel
{
    class SettingVM :Utils.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public decimal TransactionAmount
        {
            get { return _pageModel.TransactionValue; }
            set { _pageModel.TransactionValue = value; OnPropertyChanged(); }
        }

        public SettingVM()
        {
            _pageModel = new PageModel();
            TransactionAmount = 5638;
        }
    }
}