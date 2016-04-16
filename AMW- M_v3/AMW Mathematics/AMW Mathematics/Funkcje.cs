using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics
{
    class Funkcje
    {
        Liczwyraz li;
        string wejscie2 = "";
        int ilenawiasow = 0;
        int ileusunac = 0;
        double wynikfunkcji;
        int dodatkowo = 0; //zmienna okreslajaca ile liter ma funkcja
        int dod = 2; //zmienna okreslajaca od jakiego elementu wyrazenie ma zostac usuwane w celu usuniecia funkcji i zastapienia jej liczba
        public void przypiszwartosczmiennej(ref string wejscie, ref  int j) //przypisanie calej funkcji do zmiennej
        {
            for (int k = j + 1; k <= wejscie.Length; k++)// przypisanie zawartosci funkcji do zmiennej 
            {
                if (wejscie[k].ToString() == "(")
                {
                    ilenawiasow++; //zliczenie nawiasów w celu ustalenia gdzie jest nawias zamykajacy funkcje 
                }
                if (wejscie[k].ToString() == ")")
                {
                    ilenawiasow--;
                    if (ilenawiasow == 0) //0 swiadczy o tym ze wystapil nawias zamykajacy funkcje 
                    {
                        wejscie2 += wejscie[k].ToString() + "=";
                        ileusunac = ileusunac + 1;
                        break;
                    }
                }
                wejscie2 += wejscie[k].ToString();
                ileusunac = ileusunac + 1;
            }
        }
        public bool wstawwynikfunkcji(ref string wejscie, ref bool poczatekfunckja,  ref bool liczbaujemna, ref int j ) //wprwoadzenie obliczonej funkcji do wyrażenia
        {
            if (wynikfunkcji < 0)
            {
                wynikfunkcji = wynikfunkcji * -1;
                liczbaujemna = true;
            }
            if (poczatekfunckja == true) //jesli na poczatku wysapi funkcja jesli 
            {
                wejscie = wejscie.Remove(0, ileusunac + dodatkowo); //usuniecie funkcji 
                wejscie = wejscie.Insert(0, wynikfunkcji.ToString()); //podstawienie pod pierwszy wyraz funkcj obliczononej liczby 
                poczatekfunckja = false;
                return true;
            }
            else
            {
                wejscie = wejscie.Remove(j - dod, ileusunac + dodatkowo ); //usuniecie funkcji o parametr dodatkowo opisany wyzej 
                wejscie = wejscie.Insert(j - dod, wynikfunkcji.ToString()); //podstawienie pod pierwszy wyraz funkcj obliczononej liczby z parameterem dod opisanym wyzej
                return true;
            }
        }
        public bool liczfunkcje(string funkcja, int j, ref string wejscie, ref bool liczbaujemna, ref bool poczatekfunckja)
        {
            switch (funkcja) //sprawdzenie czy na liscie znajudje sie funkcja jesli tak wykonanie operacji i zwrocenie wartosci true w przeciwnym wypadku swraca false 
            {
                case "sin":
                    dodatkowo = 3; //ilosc znakwo zjakiej skalda sie fukcja
                    dod = 2;  // liczba potrzebna do usuniecia ciagu znakow funkcji 
                    przypiszwartosczmiennej(ref wejscie, ref j);
                    li = new Liczwyraz();
                     wynikfunkcji = li.liczwyrazenie(wejscie2); //rekurencja obliczajaca wartosc wyrazenia zjadujacego sie w funkcji 
                    wynikfunkcji = Math.Sin(wynikfunkcji);
                    return wstawwynikfunkcji(ref wejscie, ref poczatekfunckja,  ref liczbaujemna, ref j);

                case "cos":
                    dodatkowo = 3;
                    dod = 2;
                    przypiszwartosczmiennej(ref wejscie, ref j);
                    li = new Liczwyraz();
                    wynikfunkcji = li.liczwyrazenie(wejscie2);  
                    wynikfunkcji = Math.Cos(wynikfunkcji);
                    return wstawwynikfunkcji(ref wejscie, ref poczatekfunckja, ref liczbaujemna, ref j);
                case "tan":
                    dodatkowo = 3;
                    dod = 2;
                    przypiszwartosczmiennej(ref wejscie, ref j);
                    li = new Liczwyraz();
                    wynikfunkcji = li.liczwyrazenie(wejscie2);  
                    wynikfunkcji = Math.Tan(wynikfunkcji);
                    return wstawwynikfunkcji(ref wejscie, ref poczatekfunckja, ref liczbaujemna, ref j);
                case "cot":
                    dodatkowo = 3;
                    dod = 2;
                    przypiszwartosczmiennej(ref wejscie, ref j);
                    li = new Liczwyraz();
                    wynikfunkcji = li.liczwyrazenie(wejscie2);  
                    wynikfunkcji = Math.Atan(wynikfunkcji);
                    return wstawwynikfunkcji(ref wejscie, ref poczatekfunckja, ref liczbaujemna, ref j);
                case "sqrt":
                    dodatkowo = 4;
                    dod = 3;
                    przypiszwartosczmiennej(ref wejscie, ref j);
                    li = new Liczwyraz();
                    wynikfunkcji = li.liczwyrazenie(wejscie2);  
                    wynikfunkcji = Math.Sqrt(wynikfunkcji);
                    return wstawwynikfunkcji(ref wejscie, ref poczatekfunckja, ref liczbaujemna, ref j);
            }
            return false;
        }
    }
}
