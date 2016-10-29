using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMW_Mathematics.Function
{
    class AppTheme
    {


        public String accentColor = "Blue", themeColor = "BaseLight", accentColorCode = "#FF41B1E1";

        public void LoadTheme()                                                 //Odczytywanie pliku z zapisanym motywem #Ł
        {
            string[] lines = System.IO.File.ReadAllLines(@"theme");             //Wpisuje wartość pliku do tablicy #Ł
            accentColor = lines[0];                                // Zapisuje w zmiennej akcent #Ł
            themeColor = lines[1];                               //Zapisuje w zmiennej motyw #Ł
            Accent(accentColor);                                                //Generuje kod koloru #Ł
            
            
        }
        public void SaveThem()                                                  //Zapisanie koloru aplikacji do pliku #Ł 
        {
            System.IO.File.Delete(@"theme");                                    //Usunięcie pliku z informacjami o akcencie i matywie aplikacji #Ł
            using (StreamWriter outputFile = new StreamWriter(@"theme", true))  //Zapisanie do poliku nowych informacji o akcencie i pliku #Ł
            {
                outputFile.Write(accentColor + "\n" + themeColor);
            }
        }



        public void Accent(string color)                                    //Generuje kod koloru akcentu #Ł
        {
            switch (color)
            {
                case "Red":
                    accentColorCode = "#FFEA4333";
                    break;
                case "Blue":
                    accentColorCode = "#FF41B1E1";
                    break;
                case "Green":
                    accentColorCode = "#FF80BA45";
                    break;
                case "Orange":
                    accentColorCode = "#FFFB8633";
                    break;
                case "Pink":
                    accentColorCode = "#FFF68ED9";
                    break;
                case "Brown":
                    accentColorCode = "#FF9B7B56";
                    break;
                //case "blue":
                //    accentColor = "#FF41B1E1";
                //    break;
                //    case "blue":
                //    accentColor = "#FF41B1E1";
                //    break;
                //    case "blue":
                //    accentColor = "#FF41B1E1";
                //    break;

                    // “Purple”, “Lime”, “Emerald”, “Teal”, “Cyan”, “Cobalt”, “Indigo”, “Violet”,  “Magenta”, “Crimson”, “Amber”, “Yellow”, “Olive”, “Steel”, “Mauve”, “Taupe”, “Sienna”


        }

        }
        public void Theme (string color)                                    //Reczej nie potrzebbe ?? #Ł
        {
            switch (color)
            {
                case "blue":
                    themeColor = "#FF41B1E1";
                    break;

            }

        }

    }
}
