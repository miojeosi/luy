using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CalendarApp.Services
{
    public class DayInfoService
    {
        private readonly DataSerializer serializer;

        public DayInfoService(DataSerializer serializer)
        {
            this.serializer = serializer;
        }

        public IEnumerable<DayInfo>? GetData(int month, int year)
        {
            var days = serializer.Deserialize<SerializeModel>(() => File.OpenRead("data.json"));

            DateTime min = new DateTime(year, month, 1).Date;
            DateTime max = min.AddMonths(1);

            return days?.DaysInfo?.Where(x => x.Date >= min && x.Date < max);
        }

        internal void SaveData(DayInfo? dayInfo)
        {
            var data = serializer.Deserialize<SerializeModel>(() => File.OpenRead("data.json")) ?? new SerializeModel() { DaysInfo = new List<DayInfo>()};

            var obj = data.DaysInfo.FirstOrDefault(x => x.Date == dayInfo?.Date);

            if(obj != null)
            {
                obj.Selections = dayInfo?.Selections;
            }
            else
            {
                data.DaysInfo?.Add(dayInfo);
            }

            serializer.Serialize(data, () => new FileStream("data.json", FileMode.Create, FileAccess.Write));
        }
    }
}
