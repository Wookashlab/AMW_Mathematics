using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AMW_Mathematics.E_ModelView;
using MaximaSharp;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for EquationSolver.xaml
    /// </summary>
    public partial class EquationSolver : MetroWindow
    {
        List<string> VariableList = new List<string> //Lista dostępnych zmiennych które można wprowadzić do wyrażenia #M
        {
             "A"
            ,"B"
            ,"C"
            ,"D"
            ,"E"
            ,"F"
            ,"G"
            ,"H"
            ,"I"
            ,"J"
            ,"K"
            ,"L"
            ,"M"
            ,"N"
            ,"O"
            ,"P"
            ,"R"
            ,"S"
            ,"T"
            ,"U"
            ,"W"
            ,"Y"
            ,"Y"
            ,"Z"
            ,"a"
            ,"b"
            ,"v"
            ,"d"
            ,"e"
            ,"f"
            ,"g"
            ,"h"
            ,"i"
            ,"j"
            ,"k"
            ,"l"
            ,"m"
            ,"n"
            ,"o"
            ,"p"
            ,"r"
            ,"s"
            ,"t"
            ,"u"
            ,"w"
            ,"x"
            ,"y"
            ,"z"
        };
        public EquationSolver()
        {
            InitializeComponent();
            ListViewExp.Items.Add(new List<ListExpresionView> {
                                 new ListExpresionView { Exp = "", Watermark = "Equation 1" }                     //Dynamiczne dodanie do widoku listy wyrażenia przedstawionego jako instancja klasy ListExpresionView #M
            });
       }
        private void CoEquations_SelectionChanged(object sender, SelectionChangedEventArgs e)                     //Metoda odpowiedzialna za dodanie lub też odjęcie kontenera umożliwającego wprowadzenie elementu równania #M
        {
            try                                                                                                   //obsługa błedu umożliwa uniknięcie nam przypadu gdy na liście combobox zostanie wybrany element który nie istnieje #M 
            {
                int index = CoEquations.SelectedIndex;                                                            //sprawdzenie indexu contenera mówiącego nam o ilości wyrażeń. Zmienna index mówi nam o ilości kontenerów w które można będzie wprwadzić wyrażenia #M
                int count;
                if (ListViewExp.Items.Count <= index && ListViewExp.Items.Count < 6)                              //sprawdzenie czy lista dostępnych kontenerów jest mniejsza od liczby kontenerów które chcemy wyświetlić jak i mniejsza od maksymalnej ilości kontenerów #M
                {                                                                                                 //jeśli liczba jest mniejsza oznacza to że należy dodać kontener #M
                    count = index - ListViewExp.Items.Count +1;                                                   //obliczenie ile kontenerów należy dodać do listy #M
                    for (int i = 1; i <= count; i++)                                                              //pętla odpowiadajaca za dodanie kontenera #M
                    {
                        int val = ListViewExp.Items.Count + 1;                                                    //zadeklarowana zmienna umożliwa nam poprawne wprowadzenie do Welmarka ID kontenera #M
                        ListViewExp.Items.Add(new ListExpresionView { Exp = "", Watermark = "Equation " +  val}); //Dynamiczne dodanie kontenera do listy z parametrami klasy ListExpresionView #M
                    }
                }
                index = CoEquations.SelectedIndex;                                                                //ponowne przypisanie do zmiennej index wartości wciśniegego przycisku w polu combobox #M
                if (ListViewExp.Items.Count > index + 1)                                                          //sprawdznie czy ilość kontenerów w liście jest większa niż ilość kontenerów którą wskazuje pole Combobox #M
                {                                                                                                 //w przpadlu gdy liczba kontenerów jest mniejsza należy ją zmniejszyć w liście #M
                    count = (ListViewExp.Items.Count-1)  - (index);                                               //określenie liczby kontenerów które należy usunąć z listy 
                    do
                    {

                        ListViewExp.Items.RemoveAt(ListViewExp.Items.Count - 1);
                        ListViewExp.Items.Refresh();
                        count = count - 1;
                    }
                    while (count > 0);                                                                            //pętla usuwająca kontenery z widoku listy na podstawie zmiennej count która wcześniej określiła ile konenerów ma zostać usuniętych #M
                }
            }
            catch { }
        }
        private void Solve_Click(object sender, RoutedEventArgs e)                                                      //funcja odpowiadająca za obliczenie równania #M
        {
            List<ExpresionsToSolve> lisetexpresiontosolve = new List<ExpresionsToSolve>();                              //stowrzenie listy klasy ExpresionToSolve która będzie przechowywać wartośc elementu równania wraz z zmiennymi które w nim wystąpiły #M
            foreach (var item in ListViewExp.Items)                                                                     //Pętla sczytująca elementy równania z widoku Listy i wprowadzające je do listy ExpresionToSolve na której później będziemy operować aby wyliczć wartośc wyrażenia #M
            {
                string variable = "";                                                                                   //zmienna do której zostanie załadowane niewiadome występujące w danym elemencie równania #M
                var c = item as ListExpresionView;                                                                      //rzutowanie obiektu item który jest nieznanego typu na obiekt typu  ListExpresionView i przypisanie go do zmiennej c dzięki czemu otrzymamy możliwość odowłania się do konkretnego atrybutu obiektu item w liście klasu ExpresionsToSolve #M
                if (c == null)                                                                                          //sprawdzenie czy rzutowanie przebiegło prawidłowo #M
                {
                    var w = item as List<ListExpresionView>;                                                            //jeśli nie rzutowanie obiektu item na Listę klasy ListExpresionView #M
                    if (w[0].Exp != "")                                                                                 //dodanie do listy obliczanych elementów równaniń tylko tych kontenerów, w których znajduje się jakieś równania dzięki temu zabezpieczamy możliwość zostawienia pustego konteneru przez użytkownika #M
                    {
                        variable = ExpresionFindVariable(w[0].Exp, variable);                                           //funcja zapisuje do zmiennej wszystkie niewiadome jakie wystąpiły w elemencie równania #M
                        lisetexpresiontosolve.Add(new ExpresionsToSolve { expresion = w[0].Exp, variable = variable });//dodanie do listy elemetu równania wraz z jego niewiadomymi #M
                    }
                }
                else                                                                                                    //w przypadku gdy rzutowanie zmiennej powiodło się w zmiennej c znajduje się obiekt klasy ExpresionsToSolve dzięki czemu możemy odowłać się do jego atrybutów #M
                {
                    if(c.Exp != "")                                                                                     //dodanie do listy obliczanych elementów równaniń tylko tych kontenerów, w których znajduje się jakieś równania dzięki temu zabezpieczamy możliwość zostawienia pustego konteneru przez użytkownika #M
                    {
                        variable = ExpresionFindVariable(c.Exp, variable);                                              //funcja zapisuje do zmiennej wszystkie niewiadome jakie wystąpiły w elemencie równania #M
                        lisetexpresiontosolve.Add(new ExpresionsToSolve { expresion = c.Exp, variable = variable });    //dodanie do listy elemetu równania wraz z jego niewiadomymi #M
                    }                   
                }
            }
            MessageBox.Show(solve(lisetexpresiontosolve, ""));                                                          //wywołanie funkcji solve której parametrem jest lista elementów równania wraz z niewiadomymi efektem wykonania funkcji jest zwrócenie do zmiennej typu string rozwiązania równania #M                                                   
        }
        private string solve(List<ExpresionsToSolve> lisetexpresiontosolve, string expres)
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
                        expres = Maxima.Eval(expres);                                                                           //obliczenie wartości równania przez maxime #M
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
        private string ExpresionFindVariable(string expresion, string variable)                                                 //funcka odpowiadająca za stowrzenie ciągu niewiadomych występujących w elemencie równania #M
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
    }
    public class ExpresionsToSolve                                                                                          //klasa odpowiadająca za stworzenie listy elementów rowniania wraz z występującymi w nich niewiadomymi #M
    {
        public string expresion { get; set; }                                                                               //element równania #M
        public string variable { get; set; }                                                                                //niewiadoma #M
    }
}