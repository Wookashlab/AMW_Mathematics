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
        public static class ConcurrentSerializer<T>                                                                     //klasa zapisująca listę do pliku która przyjmuje jako parametr typ listy #M
        {
            private static object obj = new object();

            public static void Serialize(string path, T obje)   
            {
                lock (obj)                                                                                              //blokowanie obiektu na czas serializacji #M
                {
                    if (obje != null)                                                                                   //sprawdzenie czy obiekt który przyjuje lest listą #M
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create))     //stworzenie pliku w danym miejscu o zadanej nazwie #M
                        {
                            var bf = new BinaryFormatter();                                                             //stworzenie instancji klasy BinaryFormatter #M
                            bf.Serialize(fs, obje);                                                                     //binarny zapis zawartości obiektu do stworzonego pliku #M
                        }
                    }
                }

            }
            public static T Deserialize(string path)
            {
                T temp = default(T);

                lock (obj)                                                                                          //zablokowanie obiektu do któego będą przekazywane deserializowane dane #M
                {
                    if (System.IO.File.Exists(path))                                                                //sprawdzenie czy plik deserializwaony istnieje #M
                    {
                        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))   //otworzenie pliku #M
                        {
                            if (fs.Length > 0)
                            {
                                var bf = new BinaryFormatter();                                     
                                return (T)bf.Deserialize(fs);                                                       //binarna deserializacja pliku do listy tak długo jak zawartość pliku jest większa od 0 #M
                            }
                        }
                    }
                    return temp;                                                                                    //zwrócenie listy #M
                }

            }
        }
    }
}
