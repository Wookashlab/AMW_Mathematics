using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics
{
    class Liczwyraz
    {
        bool liczbaujemna = false;
        int piorytet = 0;
        int piorytet1 = 0;
        int nawiasy = 0;
        int status;
        int pobierajwiecej = 0;
        bool sprawdzktorynawias = true; //zmienna odpowiadajaca za sprawdzenie nawiasów || 
        bool zmienna = true;
        bool poczatekfunckja = false; //do rozpoznania czy na poczatku znajduje sie funkcja 
        string zapiszliczbe = "";
        List<double> stosliczb = new List<double>();
        List<string> stosznakow = new List<string>();
        int i;
        string wejscie;
        public double liczwyrazenie(string wej) //glowna funkcja odpowiadajaca za liczenie wyrazenia 
        {
            wejscie = wej;
            for (i = 0; i < wejscie.Length; i++)
            {
                if (i == 0 && wejscie[i].ToString() == "-")
                {
                    wejscie = wejscie.Remove(i, 1);
                    liczbaujemna = true;
                }
                if(Char.IsDigit(wejscie[i]) != true && wejscie[i].ToString() != "-" && i == 0 && wejscie[i].ToString() != "(" && wejscie[i].ToString() != "|") //gdy na poczatku wystepuje funkcja bez nawoasu 
                {
                    poczatekfunckja = true; // potrzebne do okreslenia czy funckja wysapiła na poczatku czy w srodku wyrazenia 
                    rozpoznajfunkcjeioblicz();
                    
                }
                if (Char.IsDigit(wejscie[i]) == true)
                {
                    rozpoznajjakatolibcza();
                }
                else
                {
                    rozpoznajoperatoridodajpiorytet();
                    liczwyrazenie();
                }
            }
            return stosliczb[0];
        }
        private void rozpoznaj_znak_i_oblicz(List<double> stosliczb, List<string> stosznakow) //modufikacja elementow stosu (obliczanie)
        {
            if (stosznakow[stosznakow.Count - 1][0].ToString() == ")" && stosznakow[stosznakow.Count - 2][0].ToString() == "(" || stosznakow[stosznakow.Count - 1][0].ToString() == "|" && stosznakow[stosznakow.Count - 2][0].ToString() == "|")//sprawdzenie czy dwa ostatnie elementy na stosie to nawiasy jak tak to je usuwamy 
            {
                if (stosznakow[stosznakow.Count - 1][0].ToString() == ")" && stosznakow[stosznakow.Count - 2][0].ToString() == "(") //nowa czesc kodu 
                {
                    stosznakow.Remove(stosznakow[stosznakow.Count - 1]);
                    stosznakow.Remove(stosznakow[stosznakow.Count - 1]);
                }
                else
                {
                    stosznakow.Remove(stosznakow[stosznakow.Count - 1]);
                    stosznakow.Remove(stosznakow[stosznakow.Count - 1]);
                    if(stosliczb[stosliczb.Count-1] < 0)
                    {
                        stosliczb[stosliczb.Count - 1] = stosliczb[stosliczb.Count - 1] * -1;
                    }
                }
            }
            else
            {
                string znak = stosznakow[stosznakow.Count - 2][0].ToString();
                switch (znak)
                {
                    case "-":
                        stosliczb[stosliczb.Count - 1] = stosliczb[stosliczb.Count - 2] - stosliczb[stosliczb.Count - 1]; //wykonanie operacji na ostatnim i przed ostatnim elementem stosu wynik zapisywany na ostatnim elemencie stosu 
                        stosznakow.Remove(stosznakow[stosznakow.Count - 2]);
                        stosliczb.RemoveAt(stosliczb.Count - 2);
                        break;
                    case "+":
                        stosliczb[stosliczb.Count - 1] = stosliczb[stosliczb.Count - 2] + stosliczb[stosliczb.Count - 1];
                        stosznakow.Remove(stosznakow[stosznakow.Count - 2]);
                        stosliczb.RemoveAt(stosliczb.Count - 2);
                        break;
                    case "/":
                        stosliczb[stosliczb.Count - 1] = stosliczb[stosliczb.Count - 2] / stosliczb[stosliczb.Count - 1];
                        stosznakow.Remove(stosznakow[stosznakow.Count - 2]);
                        stosliczb.RemoveAt(stosliczb.Count - 2);
                        break;
                    case "*":
                        stosliczb[stosliczb.Count - 1] = stosliczb[stosliczb.Count - 2] * stosliczb[stosliczb.Count - 1];
                        stosznakow.Remove(stosznakow[stosznakow.Count - 2]);
                        stosliczb.RemoveAt(stosliczb.Count - 2);
                        break;
                    case "^":
                        stosliczb[stosliczb.Count - 1] = Math.Pow(stosliczb[stosliczb.Count - 2], stosliczb[stosliczb.Count - 1]);
                        stosznakow.Remove(stosznakow[stosznakow.Count - 2]);
                        stosliczb.RemoveAt(stosliczb.Count - 2);
                        break;
                }
            }
        }
        private void liczujemnedlaoperatorow() // wprzypadku gdy liczba okarze sie ujemna zmienna przyjmuje wartosc true 
        {
            if (wejscie[i + 1].ToString() == "-") //sprawdzenie czy kolejny znak po występującym operatorze jest równy - oznacza to liczbe ujemna 
            {
                wejscie = wejscie.Remove(i + 1, 1);
                liczbaujemna = true;
            }
        }
        private void liczwyrazenie()
        {
            do // petla wykonuje sie do tego momentu az zgodnosc nizszy piorytet->wyzszy piorytet zostanie stad w petli występuje instrukcja break zachowana lub na stosie zostanie tylko jedna wartosc 
            {
                if (stosznakow.Count > 1)
                {
                    if (stosznakow[stosznakow.Count - 1] == "=") // w przypadku gdy na stosie zostana jeszcze kilka operatorow a wystapi juz znak = 
                    {
                        rozpoznaj_znak_i_oblicz(stosliczb, stosznakow);
                        zmienna = false;
                    }
                    if (zmienna == true) //warunek zabezpieczajacy przed wejsciem gdy na szczycie stosu znajduje sie operator = 
                    {
                        if (int.Parse(stosznakow[stosznakow.Count - 2][1].ToString()) > int.Parse(stosznakow[stosznakow.Count - 1][1].ToString())) //sprawdzenie czy na stosie jest zachowana zasada niszy piorytet -> wyzszy jesli nie jest to pobranie operatorow ze stosu i dokonanie na nich dzialan 
                        {
                            rozpoznaj_znak_i_oblicz(stosliczb, stosznakow);
                        }
                        if (int.Parse(stosznakow[stosznakow.Count - 2][1].ToString()) == int.Parse(stosznakow[stosznakow.Count - 1][1].ToString())) //sprawdzenie czy na stosie jest zachowana zasada niszy piorytet -> wyzszy jesli nie jest to pobranie operatorow ze stosu i dokonanie na nich dzialan
                        {
                            //wystepuja tutaj dwa warunki gdyz najpierw sprawdzana jest pierwsza wartosc liczby piorytetowej np 01 02 sprawdzane jest 0 z 0 a pozniej sprawdzane jest 1 z 2 
                            if (int.Parse(stosznakow[stosznakow.Count - 2][2].ToString()) >= int.Parse(stosznakow[stosznakow.Count - 1][2].ToString())) //sprawdzenie czy na stosie jest zachowana zasada niszy piorytet -> wyzszy jesli nie jest to pobranie operatorow ze stosu i dokonanie na nich dzialan
                            {
                                rozpoznaj_znak_i_oblicz(stosliczb, stosznakow);
                            }
                            else break; //break sluzy do wyjscia z petli jak jest wiecej niz jeden operator na stosie a zasada niszy->wyzszy jest zachowana
                        }
                        else
                        {
                            if (stosznakow[stosznakow.Count - 1][0].ToString() != ")") //w przypadku gdy jest zamkniety nawias ale zostaly na stosie jakies operatory nie mozna wykonac break tylko sciagnac je wszystkie gdyz zawsze trzeba sciagac od poczatku nawiasu do konca nawiasu 
                                break;
                        }

                    }
                }
                else break;
            }
            while (stosznakow.Count != 1);
        }
        private void rozpoznajoperatoridodajpiorytet()
        {
            if (wejscie[i].ToString() == ")")
            {
                piorytet = piorytet - 4;
                piorytet1 = piorytet + 4;
                if (piorytet1 < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + piorytet1.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + piorytet1.ToString());
                }
                if (piorytet == 0)
                {
                    nawiasy = 0;  //brak nawiasow piorytet ustawiany jest na 0 
                }
                else
                {
                    nawiasy = 4 + piorytet - 4; //zakonczenie operacji w nawiasie powoduje zmniejszenie piorytetu dzieki niej pozniej moze powstac nowy nawias o tym samym piorytecie
                }
                liczujemnedlaoperatorow();
            }

            if (wejscie[i].ToString() == "|") 
            {
                if (sprawdzktorynawias == true ) //warunek odpowiadajacy za spawdzenie czy jest to nawias zamykajacy czy otwierajacy
                {
                    piorytet1 = piorytet + 4;
                    if (piorytet1 < 10)
                    {
                        stosznakow.Add(wejscie[i].ToString() + "0" + piorytet1.ToString());
                    }
                    else
                    {
                        stosznakow.Add(wejscie[i].ToString() + piorytet1.ToString());
                    }
                    nawiasy = 4 + piorytet; //zmienna nawias sluzy do zwiekszenia piorytetu liczb wystepujacych w nawiasach zmienia sie w zaleznosci waznosci nawiasu 
                    piorytet = piorytet + 4; //zmienna piorytet sluzy do zwiekszenia piorytetu kolejno występujących nawiasów 
                    pobierajwiecej++; //chyba nie potrzebna jakiś pozostałość boje sie ruszać bo może sie zjebać :D 
                    liczujemnedlaoperatorow();
                    rozpoznajfunkcjeioblicz();
                    sprawdzktorynawias = false;
                }
                else
                {
                    piorytet = piorytet - 4;
                    piorytet1 = piorytet + 4;
                    if (piorytet1 < 10)
                    {
                        stosznakow.Add(wejscie[i].ToString() + "0" + piorytet1.ToString());
                    }
                    else
                    {
                        stosznakow.Add(wejscie[i].ToString() + piorytet1.ToString());
                    }
                    if (piorytet == 0)
                    {
                        nawiasy = 0;  //brak nawiasow piorytet ustawiany jest na 0 
                    }
                    else
                    {
                        nawiasy = 4 + piorytet - 4; //zakonczenie operacji w nawiasie powoduje zmniejszenie piorytetu dzieki niej pozniej moze powstac nowy nawias o tym samym piorytecie
                    }
                    liczujemnedlaoperatorow();
                    sprawdzktorynawias = true;
                }
            }
            if (wejscie[i].ToString() == "(")
            {
                piorytet1 = piorytet + 4;
                if (piorytet1 < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + piorytet1.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + piorytet1.ToString());
                }
                nawiasy = 4 + piorytet; //zmienna nawias sluzy do zwiekszenia piorytetu liczb wystepujacych w nawiasach zmienia sie w zaleznosci waznosci nawiasu 
                piorytet = piorytet + 4; //zmienna piorytet sluzy do zwiekszenia piorytetu kolejno występujących nawiasów 
                pobierajwiecej++; //chyba nie potrzebna jakiś pozostałość boje sie ruszać bo może sie zjebać :D 
                liczujemnedlaoperatorow();
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "+")
            {
                status = nawiasy + 1;
                if (status < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + status.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + status.ToString());
                }
                liczujemnedlaoperatorow();
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "-")
            {
                status = nawiasy + 1;

                if (status < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + status.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + status.ToString());
                }
                liczujemnedlaoperatorow(); //funkcja sprawdzajaca czy liczba znajdujaca sie po znaku jest ujemna jesli tak zmienna odpowiadajaca za okreslenie liczby ujemnej przyjmuje wartost true 
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "*")
            {
                status = nawiasy + 2;
                if (status < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + status.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + status.ToString());
                }
                liczujemnedlaoperatorow();
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "/")
            {
                status = nawiasy + 2;
                if (status < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + status.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + status.ToString());
                }
                liczujemnedlaoperatorow();
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "^")
            {
                status = nawiasy + 3;
                if (status < 10)
                {
                    stosznakow.Add(wejscie[i].ToString() + "0" + status.ToString());
                }
                else
                {
                    stosznakow.Add(wejscie[i].ToString() + status.ToString());
                }
                liczujemnedlaoperatorow();
                rozpoznajfunkcjeioblicz();
            }
            if (wejscie[i].ToString() == "=")
            {
                stosznakow.Add(wejscie[i].ToString());
            }
        }
        private void rozpoznajjakatolibcza() //sprawdzenie czy liczba jest typu int czy dobule 
        {
            for (int j = i; i < wejscie.Length; j++) //petla ma na celu sprawdzenie czy w lczbie znajuje sie przecinek
            {
                if (Char.IsDigit(wejscie[i]) == true || (wejscie[i]).ToString() == ",") // sprawdzenie czy w liczbie znajudje sie przecinek
                {
                    if (wejscie[i].ToString() == ",") //sprawdzenie czy w liczbie znajudje sie przecinek
                    {
                        zapiszliczbe = "";
                        for (int k = i - 1; k >= 0; k--) //petla ma za zadanie wczytanie do zmiennej wszystkich wartosci przed przecinkiem az do peirwszego znaku oznaczajacego operacje 
                        {
                            zapiszliczbe += int.Parse(wejscie[k].ToString());
                            if (k == 0 || Char.IsDigit(wejscie[k - 1]) == false)
                            {
                                zapiszliczbe = new string(zapiszliczbe.ToCharArray().Reverse().ToArray());
                                break;
                            }
                        }
                        //  zapiszliczbe = int.Parse(wejscie[i].ToString());
                        zapiszliczbe += char.Parse(wejscie[i].ToString());
                        zapiszliczbe += int.Parse(wejscie[i + 1].ToString()); //zapis liczby zmiennoprzecinowkej do zmiennej 
                        i = i + 2;
                    }
                    else
                    {
                        zapiszliczbe += int.Parse(wejscie[i].ToString()); //przypisanie wszstkich pozostałych wartosci znajdujacych sie po przecinku do zmiennej 
                        i++;
                    }
                }
                else
                {
                    i = i - 1;
                    break;
                }
            }
            if (liczbaujemna == true)
            {
                double licz = double.Parse(zapiszliczbe.ToString()) * -1; //warunek sprawdzajacy czy iczba jest ujemna 
                stosliczb.Add(licz);
                zapiszliczbe = "";
                liczbaujemna = false;
            }
            else
            {
                stosliczb.Add(double.Parse(zapiszliczbe.ToString())); //po wczytaniu liczby zmiennoprzecinokwej lub gdy liczba okazala sie dziesietna zapisanie jej na stos 
                zapiszliczbe = "";
            }
        }
        private void rozpoznajfunkcjeioblicz()
        {
            Funkcje f = new Funkcje();
            string funkcja = "";
            if (poczatekfunckja ==  true) //jesli wystapi na poczatlu funckja nie poprzedozna nawiasem w przeciwny wykonuj normalnie jak zawsze 
            {
                for (int j = i ; j < wejscie.Length; j++) 
                {
                    funkcja += wejscie[j].ToString();
                    if (f.liczfunkcje(funkcja, j, ref wejscie, ref liczbaujemna, ref poczatekfunckja) == true) 
                    {
                        break;
                    }
                }
            }
            else
            {
                if (Char.IsDigit(wejscie[i + 1]) != true && wejscie[i + 1].ToString() != "(" && wejscie[i + 1].ToString() != "|") //w przyadku gdy po operatorze wystapi znak nie będący ( oznacza to ze wystąpiła funkcja 
                {
                    for (int j = i + 1; j < wejscie.Length; j++) //wczytuje kolejne elementy i sprawdza czy na liscie znajuje sie taka funckja 
                    {
                        funkcja += wejscie[j].ToString();
                        if (f.liczfunkcje(funkcja, j, ref wejscie, ref liczbaujemna, ref poczatekfunckja) == true) //warunek odpowiadajacy za sprawdzenie jaka to funkcja
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
