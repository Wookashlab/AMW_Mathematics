using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics
{
    class Keyboard
    {

        public string Click(string name, string content)   //funkcja ustalająca co ma wyświetlić sie po kliknięciu przycisku odpowiedizalnego za funckje #Ł
        {
            char tester = name[name.Length - 1];
            switch (tester)
            {
                case '1':                                   //funckja z otwartym nawiasem #Ł
                    return content + "(";
                case '2':                                   //funckja bez nawiasu #Ł
                    return content;
                case '3':                                   //inna !!DO NAPISANIA!! #Ł
                    return Other(name);
                default:
                    return "DOPISZ NUMER DO NAZWY";                          //jeśli wystąpi błąd program nie wysypie się (chce przeniść wyświetlanie gdzie indziej później #Ł 
            }


        }

        public int ShowHide(string tab)                     //funckja ustalająca, którą zakładkę program ma schować po kliknięciu +/- #Ł
        {
            switch (tab)
            {
                case "Calculus":                            //Rachunek różniczkowy
                    return 1;
                case "Statistic":                           //Statystyka
                    return 2;
                case "Trigonometry":                        //Trygonometria
                    return 3;
                case "LinearAlgebra":                       //Algebra liniowa
                    return 4;
                case "Standard":                            //Statystyka
                    return 5;
                case "Favorite":                            //Ulubione
                    return 6;
            }
            return 0;
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
        public string Other(string name)
        {
            switch (name)
            {
                case "Derivative3":
                    return "diff(";
                case "DerivativeS3":
                    return "diff(,2)";
                case "Integrate3":
                    return "integrate(";
                case "IntegrateO3":
                    return "integrate(,x,,)";


            }
            return "DODAJ DO OTHER";
        }
    }
}
      

