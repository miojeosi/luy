using System;
using System.IO;

namespace CalendarApp.Services
{
    public abstract class DataSerializer
    {
        public DataSerializer()
        {
        }

        public abstract T Deserialize<T>(Func<Stream> readStream) where T:class;
        public abstract void Serialize<T>(T data, Func<Stream> writeStream) where T:class;
    }
}
