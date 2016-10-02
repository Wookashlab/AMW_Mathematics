using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics
{
    class Keyboard
    {
      
        public string Click (string name, string content)   //funkcja ustalająca co ma wyświetlić sie po kliknięciu przycisku odpowiedizalnego za funckje #Ł
        {
            char tester = name[name.Length -1];
            switch (tester) { 
                case '1':                                   //funckja z otwartym nawiasem #Ł
                    return content + "(";
                case '2':                                   //funckja bez nawiasu #Ł
                    return content;
                case '3':                                   //inna !!DO NAPISANIA!! #Ł
                    return "????";                          
                default:
                    return "błąd";                          //jeśli wystąpi błąd program nie wysypie się (chce przeniść wyświetlanie gdzie indziej później #Ł 
            }


            }

        public int ShowHide(string tab)                     //funckja ustalająca, którą zakładkę program ma schować po kliknięciu +/- #Ł
        {
            switch (tab)
            {
                case "Trigonometry":                        //Trygonometria
                    return 1;
                case "Statistic":                           //Statystyka
                    return 2;
                default:                                    //Pozostałe (obecnie standard)
                    return 3;

            }
        }
        public string Mark(string mark)                     //funckja zminiająca znak + na - i vice versa #Ł
        {
            switch (mark)
                { 
            case "-":
            return "+";
               default:
            return "-";
            }
        }

        }
    }
