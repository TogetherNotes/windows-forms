using System;
using TogetherNotes.Models;

namespace TogetherNotes.ViewModel
{
    class MapVM :Utils.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public DateTime DisplayOrderDate
        {
            get { return _pageModel.OrderDate; }
            set { _pageModel.OrderDate = value; OnPropertyChanged(); }
        }

        public MapVM()
        {
            _pageModel = new PageModel();
            
        }
    }
}