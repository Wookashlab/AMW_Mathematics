using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaximaSharp;
using AMW_Mathematics.E_Model;
using AMW_Mathematics.Function;
using System.Windows.Controls;

namespace AMW_Mathematics.Function
{
    class EqualizationSolverFunction
    {
        public string solve(List<ExpresionsToSolve> lisetexpresiontosolve, string expres)
        {
            try
            {
                bool goodexpresioncheck = true;                                                                                 //deklaracja zmiennej dzięki której jesteśmy w stanie stwierdzić czy równanie został dobrze sformułowane przez użytkownika #M
                var list = lisetexpresiontosolve.Select(m => m.variable);                                                       //przy użyciu LINQ przypisanie do zmiennej typu object listy zmiennych występujacych w poszczególnych elementach równania #M 
                var c = list.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).ToString();                       //przypisanie do zmiennej typu object ciągu wszystich zmiennych które wystąpiły w równaniu #M 
                int index = list.ToList().FindIndex(m => m.Contains(c.ToString()));                                             //przypisanie do zmiennej index pozycli w liście tego elementu równania w którym wystąpiły wszystkie zmienne #M
                foreach (var item in list.ToList())                                                                             //pętla należy do sprawdzenia czy rowanie zostalo dobrze zdeklarowane poprzez sprawdzenie czy w elementach równania nie występują źle zdeklarowane niewiadomoe #M
                {                                                                                                               //przeszukanie całej listy elementów równania i sprawdzenie czy nie występują w nim niewiadome których nie ma #M
                    for (int i = 0; i < item.ToString().Length; i++)                                                            //pętla sprawdzająca czy w elemencie równania nie znajdują się niezdeklarowane zmienne #M
                    {
                        if (c.ToString().Contains(item.ToString()[i]) != true)                                                  //gdy warunek nie jest spełniony oznacza to że element równania zawiera niezdeklarowane zmienne #M
                        {
                            goodexpresioncheck = false;                                                                         //ustawenie zmiennej na false powoduje nieobliczanie wartości równia #M
                            break;
                        }
                        if (i == item.ToString().Length - 1 && list.ToList().Count() != c.ToString().Replace(",", "").Length)   //sprawdzenie czy liczba niewiadomych jest równa ilości równań jeśli tak nie jest oznacza to że równanie jest źle sformułowane #M 
                        {
                            goodexpresioncheck = false;                                                                         //ustawenie zmiennej na false powoduje nieobliczanie wartości równia #M            
                            break;
                        }
                    }
                }
                if (goodexpresioncheck == true)                                                                                 //sprawdzenie czy walidacja równania przebiegła prawidołow #M
                {
                    if (lisetexpresiontosolve.Count > 1)                                                                        //Obliczanie równania w przypadku gdy równania składa się z więcej niż jednego elemetu #M
                    {
                        foreach (var item in lisetexpresiontosolve)
                        {
                            expres = expres + "," + item.expresion;                                                             //przypisanie do zmiennej expres wszystkich elementów równania w celu przygotowania ich do wprowadzania do Maximi #M
                        }
                        expres = expres.Remove(0, 1);                                                                           //usunięcie perwszego znaku który był przecinkiem #M
                        expres = "linsolve([" + expres + "],[" + lisetexpresiontosolve[index].variable + "])";                  //przygotowanie równania do wprowadzenia do maximy #M
                        expres = Maxima.Eval(expres);
                        int ind = expres.IndexOf('[');
                        expres = expres.Remove(0, ind);
                        return expres;                                                                                          //zwrócenie obliczonych niewiadomych w postaci stringa #M
                    }
                    if (lisetexpresiontosolve.Count == 1)                                                                       //w przypadku gdy równanie składa się z jednego elementu #M
                    {
                        foreach (var item in lisetexpresiontosolve)
                        {
                            expres = expres + "," + item.expresion;
                        }
                        expres = expres.Remove(0, 1);
                        expres = "solve(" + expres + "," + lisetexpresiontosolve[index].variable + ")";                         //wszystko przebiega tak jak w przypadku równań z wiekszą ilością elementów poza faktem, że do maximy wprowadzamy inny rodzaj funkcji #M
                        expres = Maxima.Eval(expres);
                        int ind = expres.IndexOf('[');
                        expres = expres.Remove(0, ind);
                        return expres;                                                                                          //zwrócenie obliczonych niewiadomych w postaci stringa #M
                    }
                    return "Mistake in Equation";                                                                               //błąd w obliczani będne równanie zwraca komunikat błędu w postaci stringa #M
                }
                else
                {
                    return "Mistake in Equation";                                                                               //błąd w obliczani będne równanie zwraca komunikat błędu w postaci stringa #M
                }
            }
            catch
            {
                return "Mistake in Equation";                                                                                   //błąd w obliczani będne równanie zwraca komunikat błędu w postaci stringa #M
            }
        }
        public string ExpresionFindVariable(string expresion, string variable, List<string> VariableList)                      //funcka odpowiadająca za stowrzenie ciągu niewiadomych występujących w elemencie równania #M
        {
            try
            {
                foreach (var item in VariableList)                                                                              //pętla wprowadzająca do zmiennej typu string niewiadomych występujących w elemencie równania #M                                                                                       
                {
                    if (expresion.Contains(item) == true)                                                                       //Sprawdzenie cz0y w elemenie równania znajduje się niewiadoma z listy #M
                    {
                        variable = variable + "," + item;                                                                       //w przypadku gdy niewiadoma znajduje się w elemecie równania dodanie jej do ciągu niewiadomych
                    }
                }
                variable = variable.Remove(0, 1);                                                                               //usunięcie niepotrzebnych znaków #M
                return variable;                                                                                                //zwrócenie ciągu niewiadomych #M                                                                               
            }
            catch
            {
                return "";
            }

        }
        public void ResizeListView(ref ListView List)           //funckja odpowiadająca za dostosowanie rozmaru itemów do listy #M
        {
            var gridView = List.View as GridView;               //rzutowanie listy na obiekt GridView
            if (gridView != null)
            {
                foreach (var column in gridView.Columns)        //Pętla przebiegająca po nowo dodanym itemie w liście rzutowanyn ma GridView #M
                {
                    if (double.IsNaN(column.Width))             //Warunek odpowiadający za dostosowanie wielkości elementu itemu do listy #M
                    {
                        column.Width = column.ActualWidth;
                    }
                    column.Width = double.NaN;
                }
            }
        }
    }
}
