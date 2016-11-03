using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AMW_Mathematics.E_ModelView;
using MaximaSharp;
using AMW_Mathematics.Function;
using AMW_Mathematics.E_Model;
using System.Windows.Media;
using System.IO;
namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for EquationSolver.xaml
    /// </summary>
    public partial class EquationSolver : MetroWindow
    {
        EqualizationSolverFunction equalizationsolverfunction = new EqualizationSolverFunction();               //Stworzenie obiektu klasy equalizationsolvefunction w celu załadowania do widoku danych #M

        List<string> VariableList = File.ReadAllLines(@"VariableList").ToList();                               //Zczytanie listy zmiennych z pliku #Ł

        private int countresultsolving = 0;

        public EquationSolver(string borderColor)
        {
            InitializeComponent();
            MainBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));
            SelectBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));

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
                    count = index - ListViewExp.Items.Count + 1;                                                   //obliczenie ile kontenerów należy dodać do listy #M
                    for (int i = 1; i <= count; i++)                                                              //pętla odpowiadajaca za dodanie kontenera #M
                    {
                        int val = ListViewExp.Items.Count + 1;                                                    //zadeklarowana zmienna umożliwa nam poprawne wprowadzenie do Welmarka ID kontenera #M
                        ListViewExp.Items.Add(new ListExpresionView { Exp = "", Watermark = "Equation " + val }); //Dynamiczne dodanie kontenera do listy z parametrami klasy ListExpresionView #M
                    }
                }
                index = CoEquations.SelectedIndex;                                                                //ponowne przypisanie do zmiennej index wartości wciśniegego przycisku w polu combobox #M
                if (ListViewExp.Items.Count > index + 1)                                                          //sprawdznie czy ilość kontenerów w liście jest większa niż ilość kontenerów którą wskazuje pole Combobox #M
                {                                                                                                 //w przpadlu gdy liczba kontenerów jest mniejsza należy ją zmniejszyć w liście #M
                    count = (ListViewExp.Items.Count - 1) - (index);                                               //określenie liczby kontenerów które należy usunąć z listy 
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

        private void Solve_Click(object sender, RoutedEventArgs e)                                                          //funcja odpowiadająca za obliczenie równania #M
        {
            List<ExpresionsToSolve> lisetexpresiontosolve = new List<ExpresionsToSolve>();                                  //stowrzenie listy klasy ExpresionToSolve która będzie przechowywać wartośc elementu równania wraz z zmiennymi które w nim wystąpiły #M
            foreach (var item in ListViewExp.Items)                                                                         //Pętla sczytująca elementy równania z widoku Listy i wprowadzające je do listy ExpresionToSolve na której później będziemy operować aby wyliczć wartośc wyrażenia #M
            {
                string variable = "";                                                                                       //zmienna do której zostanie załadowane niewiadome występujące w danym elemencie równania #M
                var c = item as ListExpresionView;                                                                          //rzutowanie obiektu item który jest nieznanego typu na obiekt typu  ListExpresionView i przypisanie go do zmiennej c dzięki czemu otrzymamy możliwość odowłania się do konkretnego atrybutu obiektu item w liście klasu ExpresionsToSolve #M
                if (c == null)                                                                                              //sprawdzenie czy rzutowanie przebiegło prawidłowo #M
                {
                    var w = item as List<ListExpresionView>;                                                                //jeśli nie rzutowanie obiektu item na Listę klasy ListExpresionView #M
                    if (w[0].Exp != "")                                                                                     //dodanie do listy obliczanych elementów równaniń tylko tych kontenerów, w których znajduje się jakieś równania dzięki temu zabezpieczamy możliwość zostawienia pustego konteneru przez użytkownika #M
                    {
                        variable = equalizationsolverfunction.ExpresionFindVariable(w[0].Exp, variable, VariableList);       //funcja zapisuje do zmiennej wszystkie niewiadome jakie wystąpiły w elemencie równania #M
                        lisetexpresiontosolve.Add(new ExpresionsToSolve { expresion = w[0].Exp, variable = variable });     //dodanie do listy elemetu równania wraz z jego niewiadomymi #M

                    }
                }
                else                                                                                                        //w przypadku gdy rzutowanie zmiennej powiodło się w zmiennej c znajduje się obiekt klasy ExpresionsToSolve dzięki czemu możemy odowłać się do jego atrybutów #M
                {
                    if (c.Exp != "")                                                                                         //dodanie do listy obliczanych elementów równaniń tylko tych kontenerów, w których znajduje się jakieś równania dzięki temu zabezpieczamy możliwość zostawienia pustego konteneru przez użytkownika #M
                    {
                        variable = equalizationsolverfunction.ExpresionFindVariable(c.Exp, variable, VariableList);          //funcja zapisuje do zmiennej wszystkie niewiadome jakie wystąpiły w elemencie równania #M
                        lisetexpresiontosolve.Add(new ExpresionsToSolve { expresion = c.Exp, variable = variable });        //dodanie do listy elemetu równania wraz z jego niewiadomymi #M

                    }
                }
            }
            string result = equalizationsolverfunction.solve(lisetexpresiontosolve, "");
            if (result != "Mistake in Equation")
            {
                SolverResultList.Items.Insert(0, new ResultExpresionView { ResultSolving = result, Index = countresultsolving });    //wywołanie funkcji solve której parametrem jest lista elementów równania wraz z niewiadomymi efektem wykonania funkcji jest zwrócenie do zmiennej typu string rozwiązania równania #M  
                                                                                                                                     //następnie dodanie do listy wyników wyniku funkcji solve oraz pozycji w liscie #M

                equalizationsolverfunction.ResizeListView(ref SolverResultList);                                                    //funcja odpowiadająca za dostoswanie wielkości itemów do listy #M
                SolverResultList.Items.Refresh();
                countresultsolving = countresultsolving + 1;
            }
            else MessageBox.Show("Mistake in Equation");                                                                         //Pokazanie wiadomości o błędnej deklaracji równania #M
            SolverExample.Visibility = Visibility.Hidden;
        }

        private void RemoveEquation_Click(object sender, RoutedEventArgs e)                 //Metoda odpowiadająca za usunięcie z listy rozwiązania równania #M
        {
            int index = 0;                                                                  
            var key = (Button)sender;                                                       //przypisanie do zmiennej key controli wciśniętego przycisku #M
            foreach(var item in (SolverResultList as ListBox).Items)                        //Przeszukanie listy rozwiązanych równań względem wciśniętej kontroli #M
            {
                if(int.Parse(key.Tag.ToString())==(item as ResultExpresionView).Index)      //Jeśli wciśnięta kontrola odpowiada elementowi na liście zapisanie idexu rozwiązania równania znajdującego się na liście #M
                {
                    index = SolverResultList.Items.IndexOf(item);
                }
            }
            SolverResultList.Items.RemoveAt(index);                                         //Usunięcie elementu listy o znalezionym wcześniej indeksie #M
        }
    }
}