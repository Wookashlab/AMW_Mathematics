using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Windows.Media;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MahApps.Metro.Controls;


namespace AMW_Mathematics.Function
{
    class AppTheme                                              //Klasa odpowiedzialna za operacje związane z zmianą motywu (zapis,odczyt,generowanie kodów kolorów) #Ł
    {


        public String accentColor = "Blue";                     //Nazwa koloru akcentu - domyślnie niebieski #Ł
        public String themeColor = "BaseLight";                 //Nazwa motywu - domyślnie jasny #Ł
        public String accentColorCode = "#FF41B1E1";            //Kod koloru akcentu- domyślnie niebieski #Ł
        public String borderColor = "#3341B1E1";                //Kod koloru ramek - domyślnie jaśnieszy niebieski #Ł
        
        public void LoadTheme()                                                     //Odczytywanie pliku z zapisanym motywem #Ł
        {
            try                                                    //Sprawdzenie czy istnieje taki plik #Ł
            {               
                using (StreamReader sr = new StreamReader(@"theme"))        
                {
                    string[] lines = System.IO.File.ReadAllLines(@"theme");             //Wpisuje wartość pliku do tablicy #Ł
                    accentColor = lines[0];                                // Zapisuje w zmiennej akcent #Ł
                    themeColor = lines[1];                               //Zapisuje w zmiennej motyw #Ł
                    Accent(accentColor);                                                //Generuje kod koloru #Ł
                    GetBorderColor(accentColorCode);
                }
            }
            catch (Exception e)
            {
                string war = e.ToString();
                using (StreamWriter outputFile = new StreamWriter(@"theme", false))  //Jeśli nie istnije - utworzenie pliku z domyślnymi kolorami #Ł
                {
                    SaveThem();                                                     //Wywołanie funkci zapisy #Ł
                }
            }
          
            
            
        }
        public void SaveThem()                                                  //Zapisanie koloru aplikacji do pliku #Ł 
        {
            try                                                    //Sprawdzenie czy istnieje taki plik #Ł
            {
                using (StreamWriter outputFile = new StreamWriter(@"theme", false))  //Stworzenie lub nadpisanie pliku zawierającego nowe informacje o akcencie i motywie #Ł
                {
                    outputFile.Write(accentColor + "\n" + themeColor);              //Zapisanie do pliku 
                }
            }
            catch (Exception e)
            {
                string war = e.ToString();
                MessageBox.Show("Unfortunately you can not save your theme. Run the program as an administrator and try again", "Can not save your theme");   //Informacja o błędzie jeżeli nie można zapisać motywu do pliku #Ł
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
                case "Purple":
                    accentColorCode = "#FF837AE5";
                    break;
                case "Lime":
                    accentColorCode = "#FFB6D033";
                    break;
                case "Emerald":
                    accentColorCode = "#FF33A133";
                    break;
                case "Teal":
                    accentColorCode = "#FF33BCBA";
                    break;
                case "Cobalt":
                    accentColorCode = "#FF3373F2";
                    break;
                case "Indigo":
                    accentColorCode = "#FF8833FF";
                    break;
                case "Violet":
                    accentColorCode = "#FFBB33FF";
                    break;
                case "Magenta":
                    accentColorCode = "#FFE0338F";
                    break;
                case "Crimson":
                    accentColorCode = "#FFB53351";
                    break;
                case "Amber":
                    accentColorCode = "#FFF3B53B";
                    break;
                case "Yellow":
                    accentColorCode = "#FFFEE538";
                    break;
                case "Olive":
                    accentColorCode = "#FF8A9F83";
                    break;
                case "Steel":
                    accentColorCode = "#FF83919F";
                    break;
                case "Mauve":
                    accentColorCode = "#FF9180A1";
                    break;
                case "Taupe":
                    accentColorCode = "#FF9F9471";
                    break;
                case "Sienna":
                    accentColorCode = "#FFB37557";
                    break;
            }

        }
        public void GetBorderColor(string tColor)                          //Funkcja generująca kolor ramek Ł#
        {
            borderColor = "#33" + tColor.Substring(3);                     //rozjaśninie kodu koloru akcentu #Ł

        }

    }
}
