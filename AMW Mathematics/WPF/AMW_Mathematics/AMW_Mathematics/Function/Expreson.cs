using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaximaSharp;

namespace AMW_Mathematics
{
    class Expreson                  //Klasa do operacji na wyrażeniu #Ł
    {
        public Dictionary<string, string> SymbolsAndValues = new Dictionary<string, string>();

        public string SaveValuesOfVariables(string Expresion, TextBox ExpressionField)            // Zapisanie wartości zmiennyhc #Ł                                                                                                          //Metoda odpowadająca za sprawdzenie cz wyrażenie które zostało wprowadzone do obliczenia jest deklaracją zmiennej czy wyrażeniem do obliczenia #M 
        {
            if (Expresion.Contains(":=") == true)                                                                                                                                                           //sprawdzenie czy wyrażenie ma zostać podstawione jako zmienna #M
            {
                Expresion = AddToNumberDot(ExpressionField.Text.Substring(Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":")) + 2));                                                   //dodanie do każdej liczby znajdującej się w wyrażeniku kropki z zerem w celu wprowadzenia poprawnego wyrażenia do maximy #M
                Expresion = Maxima.Eval(Expresion);                                                                                                                                                         //obliczenie wartości wyrażenia i przypisanie go do zmiennej #M
                try
                {
                    foreach (var symbol in SymbolsAndValues)                                                                                                                                                //pętla umożliwająca podstawienie wartości niewiadomych znajdujących się w obliczanym wyrażeniu #M                                                                                                                                           
                    {
                        Expresion = Expresion.Replace(symbol.Key, symbol.Value);
                    }
                    SymbolsAndValues[SymbolsAndValues.First(m => m.Key == ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":")))).Key] = Expresion;    //w przypadku gdy wyrażenie występuje podmienienie jego wartości #M
                }
                catch
                {
                    SymbolsAndValues.Add(ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))), Expresion);                                           //dodanie do słownika nowej zmiennej #M
                }
                return ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))) + ":=" + Expresion;                                                      //zwrócenie obliczonego wyrażenia do do textboxa #M
            }
            else return Expresion;                                                                                                                                                                          //zwrócenie całego wyraenia w przypadku gdy nie jest zmienną #M

        }

        public string AddToNumberDot(string Expresion)                        //Dodanie .0 do liczb w wyrażeniu      #Ł                                                                                                                                        //metoda odpowiadająca za dodanie kropi i zera do każdej liczby całkowitej w celu poprawnego wprowadzenia do Maximy 
        {
            string pom = "";
            for (int i = 0; i < Expresion.Length; i++)             //pętla szukająca w wyrażeniu liczb całkowitych #Ł
            {                                                                                                                                                                                               //wprzypadku gdy z
                if (Char.IsNumber(Expresion[i]) == true)       //sprawdzenie czy znak jest liczbą #Ł
                {
                    for (int j = i; j < Expresion.Length; j++)      //w przypadku gdy jest sprawdzenie czy jest to liczba całkowita #Ł
                    {
                        if (Char.IsNumber(Expresion[j]) != true && Expresion[j] != '.')     //w przypadku gdy jest całkowita dodanie do liczby ciągu .0  #Ł
                        {
                            if (pom.Length == 1)
                            {
                                Expresion = Expresion.Insert(j, ".0");                    //dodanie do wyrażenia .0 w przypadku gdy liczba jest od 0-9    #Ł                                                                                                    
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)          //dodanie do wyrażenia .0 w przypadku gdy liczba jest większa od 9  #Ł
                                {
                                    Expresion = Expresion.Insert(j, ".0");	//wstawienie w indeksie j „.0” #Ł
                                    pom = "";				//wyczyszczenie zmiennej pomocniczej #Ł
                                    i = j + 2;				//przesunięcie licznika pętli o 2 #Ł
                                    break;
                                }
                                else
                                {
                                    pom = "";				//wyczyszczenie zmiennej pomocniczej #Ł
                                    i = j;				//przywrócenie licznika pętli do wartości i #Ł
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pom = pom + Expresion[j];	// dopisanie do zmiennej pomocniczej znaku z pozycji j #Ł
                        }
                        if (i + 1 == Expresion.Length)	//sprawdzenie czy jest to koniec przedostatni znak w ciągu #Ł
                        {
                            if (pom.Length == 1)		//sprawdzenie czy w zmiennej pomocniczej znajduje się tylko jeden znak #Ł
                            {
                                Expresion = Expresion.Insert(j + 1, ".0");	///wstawienie w indeksie j+1 „.0” #Ł
                                i = i + 2;				//przesunięcie licznika pętli o 2 #Ł
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)
                                {
                                    Expresion = Expresion.Insert(j + 1, ".0");  	///wstawienie w indeksie j+1 „.0” #Ł
                                    pom = "";				//wyczyszczenie zmiennej pomocniczej #Ł
                                    i = j + 2;				//przesunięcie licznika pętli o j+ 2	 #Ł
                                    break;
                                }
                                else
                                {
                                    pom = "";				//wyczyszczenie zmiennej pomocniczej #Ł
                                    i = j;				//ustawienie licznika i na wartości j #Ł
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return Expresion;                                //zwrócenie wartości wyrażenia  #Ł
        }


        public string CheckVariablesinExpresion(string Expresion)                                                                                                           //metoda odpowadająca sprawdzeniem czy wyrażenie nie jest deklaracją zmiennej i wprowadzanie do wyrażenia wartości zmiennych #M
        {
            if (Expresion.Contains(":=") != true)
            {
                foreach (var symbol in SymbolsAndValues)                                                                                                                    //pętla odpowada za wprowadzenie do wyrażenia wartości zmiennych #M
                {
                    Expresion = Expresion.Replace(symbol.Key, symbol.Value);
                }
            }
            return Expresion;
        }
    }
}
