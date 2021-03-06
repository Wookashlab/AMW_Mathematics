﻿using MahApps.Metro.Controls;
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
using MahApps.Metro.Controls.Dialogs;
namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for EquationSolver.xaml
    /// </summary>
    public partial class EquationSolver : MetroWindow
    {
        private EqualizationSolverFunction equalizationsolverfunction = new EqualizationSolverFunction();              //Stworzenie obiektu klasy equalizationsolvefunction w celu załadowania do widoku danych #M

        private List<string> VariableList = File.ReadAllLines(@"VariableList").ToList();                               //Zczytanie listy zmiennych z pliku #Ł

        ResultExpresionView result = new ResultExpresionView();

        public List<ResultExpresionView> EquationList = new List<ResultExpresionView>();

        public List<ResultExpresionView> ToSerializeResultEquation = new List<ResultExpresionView>();                   //publiczna lista stworzona w celu serializacji #M

        private FunctionToAllPlot functiontoplot = new FunctionToAllPlot();

        private int countresultsolving = 0;

        private bool EditChange = false;

        public EquationSolver(string borderColor)
        {
            InitializeComponent();
            List<string> w = new List<string>();
            MainBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));
            SelectBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));

            ListViewExp.Items.Add(new List<ListExpresionView> {
                                 new ListExpresionView { Exp = "", Watermark = "Equation 1" }                     //Dynamiczne dodanie do widoku listy wyrażenia przedstawionego jako instancja klasy ListExpresionView #M
            });
        }

        public EquationSolver(string borderColor, List<int> indexEquation, List<int> indexResult, List<string> Equations, List<string> Result) //dokończyć
        {
            InitializeComponent();
            for(int i = 0; i < indexEquation.Count; i++)
            {
                EquationList.Add(new ResultExpresionView { Index = indexEquation[i], ResultSolving = Equations[i] });
            }
            for(int i = 0; i < indexResult.Count; i++)
            {
                SolverResultList.Items.Insert(0, new ResultExpresionView { ResultSolving = Result[i], Index = indexResult[i] });
            }
            for (int i = 0; i < indexResult.Count; i++)
            {
                ToSerializeResultEquation.Insert(0, new ResultExpresionView { ResultSolving = Result[i], Index = indexResult[i] });
            }
            List<string> w = new List<string>();
            MainBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));
            SelectBorder.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(borderColor));

            ListViewExp.Items.Add(new List<ListExpresionView> {
                                 new ListExpresionView { Exp = "", Watermark = "Equation 1" }                     //Dynamiczne dodanie do widoku listy wyrażenia przedstawionego jako instancja klasy ListExpresionView #M
            });
        }

        private void CoEquations_SelectionChanged(object sender, SelectionChangedEventArgs e)                     //Metoda odpowiedzialna za dodanie lub też odjęcie kontenera umożliwającego wprowadzenie elementu równania #M
        {
            if (EditChange == false)
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
            countresultsolving = SolverResultList.Items.Count;
            if (result != "Mistake in Equation")
            {
                SolverResultList.Items.Insert(0, new ResultExpresionView { ResultSolving = result, Index = countresultsolving });    //wywołanie funkcji solve której parametrem jest lista elementów równania wraz z niewiadomymi efektem wykonania funkcji jest zwrócenie do zmiennej typu string rozwiązania równania #M  
                                                                                                                                     //następnie dodanie do listy wyników wyniku funkcji solve oraz pozycji w liscie #M

                ToSerializeResultEquation.Insert(0, new ResultExpresionView { ResultSolving = result, Index = countresultsolving }); //lista wyników równania stworzona w celu serializacji #M
                equalizationsolverfunction.ResizeListView(ref SolverResultList);                                                    //funcja odpowiadająca za dostoswanie wielkości itemów do listy #M
                foreach (var item in lisetexpresiontosolve)
                {
                    EquationList.Add(new ResultExpresionView { ResultSolving = item.expresion, Index = countresultsolving });
                }
                SolverResultList.Items.Refresh();            }
            else MessageBox.Show("Mistake in Equation");                                                                         //Pokazanie wiadomości o błędnej deklaracji równania #M
            SolverExample.Visibility = Visibility.Hidden;
        }

        private void RemoveEquation_Click(object sender, RoutedEventArgs e)                 //Metoda odpowiadająca za usunięcie z listy rozwiązania równania #M
        {
            int index = 0;
            var key = (Button)sender;                                                       //przypisanie do zmiennej key controli wciśniętego przycisku #M
            foreach (var item in (SolverResultList as ListBox).Items)                        //Przeszukanie listy rozwiązanych równań względem wciśniętej kontroli #M
            {
                if (int.Parse(key.Tag.ToString()) == (item as ResultExpresionView).Index)      //Jeśli wciśnięta kontrola odpowiada elementowi na liście zapisanie idexu rozwiązania równania znajdującego się na liście #M
                {
                    index = SolverResultList.Items.IndexOf(item);
                }
            }
            SolverResultList.Items.RemoveAt(index);                                         //Usunięcie elementu listy o znalezionym wcześniej indeksie #M
        }

        private void ShowEquation_Click(object sender, RoutedEventArgs e)                           //metoda odpowiedzialna za wyświetlenie wszystkich elementów równania na podstawie wciśniętego klawisza #M
        {
            var key = (Button)sender;                                                                                   //przypisanie do zmiennej klawisza który był wciśnięty #M                            
            int start = EquationList.FindIndex(m => m.Index == int.Parse(key.Tag.ToString()));                          //znalezienie indexu pierwszego elementu równania #M
            int end = EquationList.FindLastIndex(m => m.Index == int.Parse(key.Tag.ToString()));                        //znalezienie indexu ostatniego elementu równania #M
            string result = "";
            foreach (var item in equalizationsolverfunction.GetElementEquation(EquationList, start, end))                  //pętla po Liście równań wprowadzajaca do zmiennej result wartości elementów równania #M
            {
                result = result + "\n" + item.ResultSolving;
            }
            this.ShowMessageAsync("Equations", result);                                                                 //wyświetlenie elementów równania #M
        }

        private void EditEquation_Click(object sender, RoutedEventArgs e)                                                                               //funkcja wprowadzająca równania do TextBox'ów w celu ich edycji #M
        {
            var key = (Button)sender;                                                                                                                   //przypisanie do zmiennej wciśniętego klawisza #M
            int count = EquationList.FindAll(m => m.Index == int.Parse(key.Tag.ToString())).Count;                                                      //zliczenie liczby elemetów równania #M                                                
            ListViewExp.Items.Clear();                                                                                                                  //wyczyszczenie listy w której znajdują się elementy równiań #M
            int welmarkindex = 1;                                                                                                                       //ustawienie welmarka textboxa na wartość 1 #M
            foreach (var item in EquationList)                                                                                                           //pętla wprowadzająca do wcześniej wyczyszcznonej listy elemety równania #M
            {
                if (item.Index == int.Parse(key.Tag.ToString()))                                                                                         //warunek sprawdzający czy dany element należy do tego równania jeśli tak wprowadzany jest on na listę #M
                {
                    ListViewExp.Items.Add(new ListExpresionView { Exp = item.ResultSolving, Watermark = "Equation " + welmarkindex.ToString() });       //dodanie do widoku listy elementu rowniania wraz z welmarkiem (opisem) #M
                }
                welmarkindex = welmarkindex + 1;
            }
            EditChange = true;                                                                                                                          //ustawienie zmiennej sterujacej na true w celu uniknięcia konflunktu ponownego oddania textboxów na podstawie zmiany itemu comboboxa #M
            CoEquations.SelectedIndex = count - 1;                                                                                                      //ustawienie itemu comboboxa na podstawie zmiennej cout item świadczy o ilości textboxów w liście do których można wprowadzić elementy równiania #M
            EditChange = false;                                                                                                                         //ustawienie zmiennej sterującej na false zasada działania opisana wyżej #M
        }
        private void EquationSolver_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }
    }
}