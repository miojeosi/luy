using CalendarApp.Models;
using CalendarApp.Services;
using CalendarApp.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CalendarApp.ViewModels
{
    public class CalendarViewModel: BaseViewModel
    {
        private readonly PageService pageService;
        private readonly DayInfoService dayInfoService;

        public DateTime? SelectedDate { get; set; }

        public ICommand PrevMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand MonthClickCommand { get; }

        public ObservableCollection<DayInfo>? Days { get; private set; }

        public int Month => SelectedDate?.Month ?? 0;


        public CalendarViewModel(PageService pageService, DayInfoService dayInfoService)
        {
            this.pageService = pageService;
            this.dayInfoService = dayInfoService;
            NextMonthCommand = new Command(NextMonth);
            PrevMonthCommand = new Command(PrevMonth);
            MonthClickCommand = new Command<DayInfo>(OnMonthClick);

            SelectedDate = DateTime.Now.Date;
        }

        private void Reload()
        {
            if (SelectedDate.HasValue) 
            {
                var days = dayInfoService.GetData(SelectedDate.Value.Month, SelectedDate.Value.Year);

                var daysArr = Enumerable.
                    Range(1, DateTime.DaysInMonth(SelectedDate.Value.Year, SelectedDate.Value.Month)).
                    Select(x => new DayInfo { Date = new DateTime(SelectedDate.Value.Year, Month, x) }).
                    ToList();

                if (days != null)
                {
                    foreach (var day in days)
                    {
                        daysArr[day.Date.Day - 1] = day;
                    }
                }

                Days = new ObservableCollection<DayInfo>(daysArr);
            }
        }


        public override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if(propertyName == nameof(SelectedDate) && SelectedDate.HasValue)
            {
                Reload();
            }
        }

        private void PrevMonth()
        {
            SelectedDate = SelectedDate?.AddMonths(-1);
        }

        private void NextMonth()
        {
            SelectedDate = SelectedDate?.AddMonths(1);
        }

        private void OnMonthClick(DayInfo param)
        {
            pageService.ChangePage<DayInfoView>(param);
        }
    }
}
