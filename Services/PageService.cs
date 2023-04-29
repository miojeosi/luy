using CalendarApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CalendarApp.Services
{
    public class PageService
    {
        private Stack<Page> _pages = new();

        public event Action<Page?>? PageChanged;
        public Page? CurrentPage { get; private set; }

        public void ChangePage<TPage>(object? data = null)
            where TPage : Page, new()
        {
            Page page = new TPage();
            if (data != null && page.DataContext is BaseViewModel vm)
                vm.SendData(data);

            if(CurrentPage != null)
                _pages.Push(CurrentPage);

            CurrentPage = page;
            OnPageChanged(page);
        }

        public void Back(bool reload = true)
        {
            if (_pages.Count > 0)
            {
                Page last = _pages.Pop();
                if (reload)
                    last = (Page)Activator.CreateInstance(last.GetType());

                CurrentPage = last;
                OnPageChanged(CurrentPage);
            }
            else
            {
                throw new ArgumentException("history is empty");
            }
        }


        private void OnPageChanged(Page? page)
        {
            PageChanged?.Invoke(page);
        }

    }
}
