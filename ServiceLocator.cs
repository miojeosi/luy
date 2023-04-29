using CalendarApp.Services;
using CalendarApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CalendarApp
{
    public class ServiceLocator
    {
        private static IServiceProvider? serviceProvider;

        public MainViewModel? MainViewModel => serviceProvider?.GetRequiredService<MainViewModel>();
        public CalendarViewModel? CalendatViewModel => serviceProvider?.GetRequiredService<CalendarViewModel>();
        public DayInfoViewModel? DayInfoViewModel => serviceProvider?.GetRequiredService<DayInfoViewModel>();

        public static void Init()
        {
            ServiceCollection services = new();


            services.AddSingleton<MainViewModel>();
            services.AddTransient<CalendarViewModel>();
            services.AddTransient<DayInfoViewModel>();


            services.AddSingleton<PageService>();
            services.AddTransient<DayInfoService>();
            services.AddTransient<DataSerializer, JsonSerializerService>();
            
            serviceProvider = services.BuildServiceProvider();
        }



    }
}
