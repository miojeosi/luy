using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace CalendarApp.Services
{
    internal class JsonSerializerService : DataSerializer
    {
        public override T? Deserialize<T>(Func<Stream> streamFunc)
            where T : class
        {
            using var stream = streamFunc();
            using StreamReader reader = new(stream);
            string json = reader.ReadToEnd();

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Ошибка чтения json-файла", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            return null;
        }

        public override void Serialize<T>(T data, Func<Stream> writeStream)
            where T : class
        {
            string json = JsonConvert.SerializeObject(data);
            using var stream = writeStream();
            using StreamWriter writer = new(stream);
            writer.Write(json);
        }
    }
}
