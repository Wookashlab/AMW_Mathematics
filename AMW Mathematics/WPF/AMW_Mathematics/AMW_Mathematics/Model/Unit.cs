using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace AMW_Mathematics.Model
{
    class Unit      //Klasa zawierająca model wszystkich jednostek wczytywany do okna konwertera jednostek #Ł
    {
        //Tablice z nazwy obsługiwanych jednostek w zależności od kategorii #Ł
        string[] Length = { "microns", "milimeters", "centimeters", "meters", "kilometers", "inches", "rods", "feet", "yards", "fathoms", "miles", "nautical miles", "astronomical units" };
        string[] Area = { "squere milimeters", "squere centimeters", "squere meters", "hectares", "squere kilometers", "squere inches", "squere feet", "squere yards", "acres", "square miles"};
        string[] Mass = {"miligrams","grams","kilograms","tonnes","ounces","pounds","short tons","long tons"};
        string[] Temperature = { "degrees Celsius", "degrees Fahrenheit", "Kelvins" };
        string[] Time = { "microseconds", "miliseconds", "seconds", "minutes", "hours", "days" };
        string[] Velocity = { "meters/second", "miles/hour", "feet/hour", "kilometers/hour" };


        string[] Main;     //Tablica do kótrej przekazywana jest tablica z używaną jednostką #Ł

    public Unit(ComboBox UnitComboFrom, ComboBox UnitComboTo, string selectedUnit)  // Konstruktor wpisujący w comobboxy odpowiednie wartości jednostek #Ł
        {
            Main = WitchUnit(selectedUnit);                                         //Wpisanie do tablicy odpowienie jedostki #Ł
            UnitComboFrom.Items.Clear();                                            //Wyczyszczenie itemów z Combobox-a pierwszej jednostki #Ł
            foreach (string i in Main)                                             //Dodanie nazw jednostek do Combobox-a pierwszej jednostki dla wszyskitch elementów listy #Ł
            {
                ComboBoxItem eList = new ComboBoxItem();
                eList.Content = i;
                UnitComboFrom.Items.Add(eList);
            }
            UnitComboTo.Items.Clear();                                              //Wyczyszczenie itemów z Combobox-a drugiej jednostki #Ł
            foreach (string i in Main)                                               //Dodanie nazw jednostek do Combobox-a drugiej jednostki dla wszyskitch elementów listy #Ł
            {
                ComboBoxItem eList = new ComboBoxItem();
                eList.Content = i;
                UnitComboTo.Items.Add(eList);
            }

        }
        string[] WitchUnit(string Unit)      //Funkcja zwracająca odpowiednią tablicę w zależności od kategorii #Ł
        {
            switch (Unit)
            { 
                case "Length":
                    return Length;
                case "Area":
                    return Area;
                case "Mass":
                    return Mass;
                case "Temperature":
                    return Temperature;
                case "Time":
                    return Time;
                case "Velocity":
                    return Velocity;
                default:
                    return Main;
             
            }
        }
         
        
    }
    
}
