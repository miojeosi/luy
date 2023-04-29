using CalendarApp.ViewModels;

namespace CalendarApp.Models
{
    public class Option: BaseViewModel
    {
        public string? Text { get; set; }
        public string Uri { get; set; }
        public bool IsSelected { get; set; }
    }
}
