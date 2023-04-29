using CalendarApp.Models;
using CalendarApp.Services;
using MvvmHelpers.Commands;
using System;
using System.Linq;
using System.Windows.Input;

namespace CalendarApp.ViewModels
{
    public class DayInfoViewModel: BaseViewModel
    {
        private readonly PageService pageService;
        private readonly DayInfoService dayInfoService;
        private bool canSave;

        public ICommand BackCommand { get; }
        public ICommand SaveCommand => new Command(Save, () => canSave);
        public ICommand FirstSelectedCommand { get; }
        public DayInfo? DayInfo { get; private set; }

        public Option[] Options { get; set; } = new Option[]
        {
            new Option{Text = "Турник", Uri = "\\Resources\\Images\\turnik.png"},
            new Option{Text = "Пробежка", Uri = "\\Resources\\Images\\running.png"},
            new Option{Text = "Отжимания", Uri = "\\Resources\\Images\\ot.png"},
            new Option{Text = "Упражнения на пресс", Uri = "\\Resources\\Images\\press.png"}
        };

        public DayInfoViewModel(PageService pageService, DayInfoService dayInfoService)
        {
            this.pageService = pageService;
            this.dayInfoService = dayInfoService;
            BackCommand = new Command(Back);
            FirstSelectedCommand = new Command<Option>(FirstSelected);
        }

        public override void SendData(object? data)
        {
            if(data is DayInfo dayInfo)
            {
                DayInfo = dayInfo;

                if(dayInfo.Selections != null)
                {
                    for (int i = 0; i < dayInfo.Selections.Length; i++)
                    {
                        Options[i].IsSelected = dayInfo.Selections[i];
                    }
                }

            }
        }

        private void FirstSelected(Option? option)
        {
            if (DayInfo?.UriImage == null && option.IsSelected)
            {
                DayInfo.UriImage = option?.Uri;
            }
            canSave = true;
            OnPropertyChanged(nameof(SaveCommand));
        }

        private void Back()
        {
            pageService.Back(false);
        }

        private void Save()
        {
            DayInfo.Selections = Options.Select(x => x.IsSelected).ToArray();
            dayInfoService.SaveData(DayInfo);
            pageService.Back();
        }
    }
}