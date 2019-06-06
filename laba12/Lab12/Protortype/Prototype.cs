using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab12
{
    [Serializable]
    public abstract class Prototype<T>
    {
        public virtual T Clone()
        {
            return (T)MemberwiseClone();    // копирование значимх типов
        }

        public virtual T DeepClone()
        {
            using (var stream = new MemoryStream())
            {
                var context = new StreamingContext(StreamingContextStates.Clone);
                var formatter = new BinaryFormatter { Context = context };
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
