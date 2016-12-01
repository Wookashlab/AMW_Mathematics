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
                    return content + "()";
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
                case "Complex":                             //Liczby zespolone
                    return 7;

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
        public string Other(string name)                    //obsługa funkcji złożonych #Ł
        {
            switch (name)
            {
                case "Derivative3":                         //pochodna #Ł
                    return "diff()";
                case "DerivativeS3":                        //pochodna drugiego stopnia #Ł          
                    return "diff(,,2)";
                case "Integrate3":                          //całka #Ł
                    return "integrate()";
                case "IntegrateO3":                         //całka oznaczona #Ł
                    return "integrate(,x,,)";
                case "Infinity3":                           //nieskończoność #Ł
                    return "inf";
                case "Addition3":                           // suma(sigma) #Ł
                    return "sum(,i,,)";
                case "Multiplication3":                     //iloczyn (pi) #Ł
                    return "product(,i,,";
                case "Sort3":                               //sortowanie liczb #Ł
                    return "sort([])";
                case "Absolute3":                           //wartość bezwzględna #Ł
                    return "abs()";
                case "Logarithm3":                          //logartym o podstawie 10 #Ł
                    return "log()/log(10)";
                case "LogarithmN3":                         //logarytm o podstawie e #Ł
                    return "log()";
                case "Pi3":                                 //liczba pi #Ł
                    return "%pi";
                case "SquareRoot3":                          //pierwiastek kwadratowy #Ł
                    return "sqrt()";
                case "Fraction3":                            // ułamek zwykły #Ł
                    return "/";
                case "LessE3":                               // mniejsze-równe #Ł
                    return "<=";
                case "MoreE3":                                // większe-równe #Ł
                    return ">=";
                case "Nequal3":                             //nierówność #Ł
                    return "#";
                case "Limit3":                              //granica #Ł
                    return "limit()";
                case "ComplexAbsolute3":                     //moduł liczby zespolonej #Ł
                    return "cabs()";
                case "RealPart3":                           //cześć rzeczywista #Ł
                    return "realpart()";
                case "ImaginaryPart3":                      //cześć urojona #Ł
                    return "imagpart()";
                case "Conjugate3":                          //sprzęrzenie zwrotne liczby zepspolonej #Ł
                    return "conjugate()";
                case "Polar_Rect3":                         //zamiana liczby zespolonej na formę rectagle #Ł
                    return "rectform()";
                case "Rect_Polar3":                         //zamiana liczby zespolonej na formę  polar #Ł
                    return "polarform()";
            }
            return "DODAJ DO OTHER";                        //Wyświetlenie błędu informującego, że funkcja nie jest dodana do tej listy #Ł
        }
    }
}


