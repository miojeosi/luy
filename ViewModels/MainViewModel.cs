using CalendarApp.Services;
using CalendarApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalendarApp.ViewModels
{
    public class MainViewModel: BaseViewModel 
    {
        public Page CurrentPage { get; private set; }

        public MainViewModel(PageService pageService)
        {
            pageService.PageChanged += PageService_PageChanged;
            pageService.ChangePage<CalendarView>();
        }

        private void PageService_PageChanged(Page page)
        {
            CurrentPage = page;
        }
    }
}
