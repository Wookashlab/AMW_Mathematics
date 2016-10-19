using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace AMW_Mathematics.Function
{
    [Serializable()]
    class Serialization
    {
        public static class ConcurrentSerializer<T>
        {
            private static object obj = new object();

            public static void Serialize(string path, T obje)
            {
                lock (obj)
                {
                    if (obje != null)
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create))
                        {
                            var bf = new BinaryFormatter();
                            bf.Serialize(fs, obje);
                        }
                    }
                }

            }
            public static T Deserialize(string path)
            {
                T temp = default(T);

                lock (obj)
                {
                    if (System.IO.File.Exists(path))
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
                        {
                            if (fs.Length > 0)
                            {
                                var bf = new BinaryFormatter();
                                return (T)bf.Deserialize(fs);
                            }
                        }
                    }
                    return temp;
                }

            }
        }
    }
}
