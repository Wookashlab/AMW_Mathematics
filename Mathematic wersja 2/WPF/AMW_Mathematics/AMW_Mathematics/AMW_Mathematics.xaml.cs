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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MaximaSharp;
using AMW_Mathematics.ModelView;
using System.Data;
using ExpressionPlotterControl;
namespace AMW_Mathematics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private GraphingHelp HelpWzory = new GraphingHelp(1);                                //obiekt klasy GraphingHelp do wyświetlana pomocy w zakładce "Wykresy" #Ł
        private GraphingHelp HelpZestawy = new GraphingHelp(2);                                //obiekt klasy GraphingHelp do wyświetlana pomocy w zakładce "Wykresy" #Ł
        private Keyboard keyboard = new Keyboard();                                 //obiekt klasy Keyboard do obsługi wirtualnego "telefonu" #Ł
        private Expreson phrase = new Expreson();                               //obiekt klasy Expression do rozwiązywania podanych wyrażeń #Ł
        private ViewPlot ViewPlot;
        private DataToChartsLine DataToCharts;
        private ChartListViewLine chartlist = new ChartListViewLine();
        List<string> ListFunction = new List<string>();                             //lista funkcji przechodzących przez oś X do wykresu #M
        List<DataToChartsLine> DataToChartList = new List<DataToChartsLine>();
        ChartLineView ChartLineWiew = new ChartLineView();
        public MainWindow()
        {
            chartlist.CountFunction = "1";
            InitializeComponent();
            DataToCharts = new DataToChartsLine();                                                       //stworzenie nowego obiektu kalsy ChartToData w celu dodania do listy możliwych zmiennych w wykresie #M
            List<ChartListViewLine> DataListView = new List<ChartListViewLine>();                           //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });      //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);                                                  //osobna klasa jeszcze nie wiem jaka :-)
            DataSetChLV.Height = 20;                                                                    //osobna klasa jeszcze nie wiem jaka :-)
            ParametricChLV.Height = 20;
            InequalitiesChLV.Height = 20;
            TraceChLV.Height = 20;
            Maxima.Eval("load (\"functs\")");                                                           //załadowanie functs do Maximy(potrzebne do kilku funkcji) #Ł


            List<ChartListViewPoint> DataToViewPoint = new List<ChartListViewPoint>();                  //dodanie wartości do zakładmi Ustaw Dane
            DataToViewPoint.Add(new ChartListViewPoint { LabelChartPointValue = "1" });
            PointChartListFunction.Items.Add(DataToViewPoint);

            List<ChartListViewParametric> DataToViewParametric = new List<ChartListViewParametric>();
            DataToViewParametric.Add(new ChartListViewParametric { LabelChartParametricValue = "1" });
            ParametricChartListFunction.Items.Add(DataToViewParametric);

            List<ChartListViewInequalities> DataToViewInequalities = new List<ChartListViewInequalities>();
            DataToViewInequalities.Add(new ChartListViewInequalities { LabelChartInequalitiesValue = "1" });
            InequalitiesChartListFunction.Items.Add(DataToViewInequalities);
        }
        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressionField.Text != "")
            {
                string Expresion = ExpressionField.Text;
                Expresion = phrase.SaveValuesOfVariables(Expresion, ExpressionField);
                Expresion = phrase.CheckVariablesinExpresion(Expresion);
                if (Expresion.Contains(":=") == false) Expresion = phrase.AddToNumberDot(Expresion);
                Expresion = Maxima.Eval(Expresion);
                Expresion = Expresion.Replace(":=", " = ");
                ResultList.Items.Add(ExpressionField.Text + "=\n" + Expresion);
                ResultList.SelectedIndex = ResultList.Items.Count - 1;
                ResultList.ScrollIntoView(ResultList.Items[ResultList.Items.Count - 1]);
                ExpressionField.Clear();
            }
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)               //Funckja wprowadzająca cyfry i znaki z kalwiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            ExpressionField.Text = ExpressionField.Text + wartosc;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)                  //Funckja czyszcząca okno wprowadzania #Ł
        {
            ExpressionField.Text = "";
        }

        private void kBack_MouseDown(object sender, MouseButtonEventArgs e)         //Funckja cofająca z klawiatury "telefonu" #Ł
        {
            if (ExpressionField.Text.Length > 0)
            {
                ExpressionField.Text = ExpressionField.Text.Substring(0, ExpressionField.Text.Length - 1);
            }
        }
        private void Function_Click(object sender, RoutedEventArgs e)               //Funckja do prowadznie funckji z klawiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            if (worksheetTab.IsSelected)                                            //Wprowadzanie wartości przycisku na zakładce "Arkusz" #Ł
            {
                ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());
                TipBox.Text = klawisz.ToolTip.ToString();
                TipBox.Foreground = Brushes.Green;
            }
            if (ChartsOverLap.IsSelected)
            {
                //Gdzie ma wprowadzić wartość w zakładce "wykresy" #Ł
            }
        }
        bool ToogleGridLineView = true;
        private void PlotChart_Click(object sender, RoutedEventArgs e)              //Funckja generująca wykres podanej funkcji #M
        {
            GraphHelpGrid.Visibility = Visibility.Hidden;
            expPlotterControl.Visibility = Visibility.Visible;
            DataToChartList.Clear();
            ListFunction.Clear();
            var _ListBox = ChartListFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "FunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                ListFunction.Add(_Control.Text);                                                                                            //dodanie do listy funkcji występującej w TextBox #M
            };
            ChartLineWiew.DrawChartLine(expPlotter, -5, 5, -5, 5, ListFunction, ToogleGridLineView);
            ToogleGridLineView = false;
        }
        public List<Control> AllChildren(DependencyObject parent)                   //Funkcja wyszukuje wszysktie kontrolki znajdujące się w danej Liście #M
        {
            var _List = new List<Control> { };
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);                  //wprowadzenie do zmiennej dziecka Elementu ListView #M
                if (_Child is Control)                                              //sprawdzenie czy jest dziecko jest kontrolką #M
                    _List.Add(_Child as Control);                                   //Jeśli tak dodananie go do listy #M
                _List.AddRange(AllChildren(_Child));                                //Rekurencyjne sprawdzenie czy dziecko ListView nie ma dzieci które też są kontrolkami #M
            }
            return _List;                                                           //zwrócenie listy Kontrolek ListView #M
        }

        private void AddExpresionToPlot_Click(object sender, RoutedEventArgs e)
        {
            chartlist.CountFunction = (int.Parse(chartlist.CountFunction) + 1).ToString();
            List<ChartListViewLine> DataListView = new List<ChartListViewLine>();
            DataListView.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });
            ChartListFunction.Items.Add(DataListView);
        }

        private void RemoveFunctionFromChart_Click(object sender, RoutedEventArgs e)
        {
            if (ChartListFunction.Items.Count > 1)
            {
                var item = ChartListFunction.Items[ChartListFunction.Items.Count - 1];
                ChartListFunction.Items.Remove(item);
            }
            chartlist.CountFunction = (int.Parse(chartlist.CountFunction) - 1).ToString();
        }

        private void Tab_Click(object sender, RoutedEventArgs e)                    //Funckja po wciśnieciu + lub - na karcie #Ł
        {
            int wartosc;
            var klawisz = (Button)sender;
            if (klawisz.Content.ToString() == "-") wartosc = -1;            //ustalnie czy karta ma być zmniejszona czy zwiększona #Ł
            else wartosc = 1;
            switch (keyboard.ShowHide(klawisz.Name.ToString()))             //zmiana szerokości odpowiedniej karty (mozna poszerzyć o nowe) #Ł
            {
                case 1:
                    CalculusTab.Height = 66 + wartosc * 39;
                    break;
                case 2:
                    StatisticTab.Height = 90 + wartosc * 63;
                    break;
                case 3:
                    TrigonometryTab.Height = 90 + wartosc * 63;
                    break;
                case 4:
                    LinearAlgebraTab.Height = 90 + wartosc * 63;
                    break;
                case 5:
                    StandardTab.Height = 120 + wartosc * 93;
                    break;
                case 6:
                    FavoriteTab.Height = 90 + wartosc * 63;
                    break;

            }
            klawisz.Content = keyboard.Mark(klawisz.Content.ToString());   //zmiana znaku + na - i vice versa #Ł
        }
        private void ShowElementListViewCharts(object sender, RoutedEventArgs e)    //Funckja po wciśnięciu + otwiera kartę w liście ListViewChart #M
        {
            var keysender = (Button)sender; //pobranie nazwy przycisku danej karty
            switch (keysender.Name) //sprawdzenie który przycisk został wciśnięty i na podstawie tego wyświetlenie odpowiednich pól w liście ListViewChart #M
            {
                case "DataSetChB":
                    DataSetChLV.Height = 428;
                    EqualizationAndFunctionsChLV.Height = 20;
                    ParametricChLV.Height = 20;
                    InequalitiesChLV.Height = 20;
                    TraceChLV.Height = 20;
                    GraphHelpGrid.DataContext = HelpZestawy;
                    break;
                case "EqualizationAndFunctionsChB":
                    EqualizationAndFunctionsChLV.Height = 428;
                    DataSetChLV.Height = 20;
                    ParametricChLV.Height = 20;
                    InequalitiesChLV.Height = 20;
                    TraceChLV.Height = 20;
                    GraphHelpGrid.DataContext = HelpWzory;
                    break;
                case "ParametricChB":
                    ParametricChLV.Height = 428;
                    EqualizationAndFunctionsChLV.Height = 20;
                    DataSetChLV.Height = 20;
                    InequalitiesChLV.Height = 20;
                    TraceChLV.Height = 20;
                    break;
                case "InequalitiesChB":
                    InequalitiesChLV.Height = 428;
                    ParametricChLV.Height = 20;
                    EqualizationAndFunctionsChLV.Height = 20;
                    DataSetChLV.Height = 20;
                    TraceChLV.Height = 20;
                    break;
                case "TraceChB":
                    TraceChLV.Height = 74;
                    InequalitiesChLV.Height = 20;
                    ParametricChLV.Height = 20;
                    EqualizationAndFunctionsChLV.Height = 20;
                    DataSetChLV.Height = 20;
                    break;
            }
        }
        private void ZoomIN_Click(object sender, RoutedEventArgs e)                 //Funkcja zmniejszająca wykres #M
        {
            ChartLineWiew.ButtonZoomIn(expPlotter, ZoomInSeries);
        }
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            ChartLineWiew.ButtonZoomOut(expPlotter, ZoomOutSeries);
        }
        private void ChartLineTopPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineWiew.MoveUPChart(expPlotter);
        }
        private void ChartLineDownPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineWiew.MoveDownChart(expPlotter);
        }
        private void ChartLineRightPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineWiew.MoveRightChart(expPlotter);
        }
        private void ChartLineLeftPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineWiew.MoveLeftChart(expPlotter);
        }

        private void Variable_Click(object sender, RoutedEventArgs e)               //Funkcja otwierająca okno z nowymi zmiennymi #Ł
        {
            VariableWindow variable = new VariableWindow();
            variable.Show();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)   //Funkcja obsługująca zmiane karty Arkusz<>Wykresy #Ł
        {
            if (worksheetTab.IsSelected)
            {
                FormatujOverLap.Visibility = System.Windows.Visibility.Collapsed;
                HomeTab.IsSelected = true;
                GraphHelpGrid.DataContext = HelpWzory;

            }
            if (ChartsOverLap.IsSelected)
                FormatujOverLap.Visibility = System.Windows.Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)                         //Funkcja obsługująca usuwanie pozycji z listy Result #Ł
        {
            ResultList.Items.Remove(ResultList.SelectedItem);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)                           //Funkcja obsługjąca edycję wprowadzonych danych #Ł
        {
            string[] partExpression;
            partExpression = ResultList.SelectedItem.ToString().Split('=');
            ExpressionField.Text = partExpression[0];

        }
    }
}
