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
namespace AMW_Mathematics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Keyboard keyboard = new Keyboard();                                 //obiekt klasy Keyboard do obsługi wirtualnego "telefonu" #Ł
        private Expression phrase = new Expression();                               //obiekt klasy Expression do rozwiązywania podanych wyrażeń #Ł
        private ViewPlot ViewPlot;
        private DataToChartsLine DataToCharts;
        private ChartListViewLine chartlist = new ChartListViewLine();
        private ZoomIN zoomin = new ZoomIN();
        List<string> ListFunction = new List<string>();                             //lista funkcji przechodzących przez oś X do wykresu #M
        List<string> ListFunction1 = new List<string>();                            //lista funkcji nie przechodzących przez oś X do wykresu #M
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
            string Expresion = ExpressionField.Text;
            Expresion = phrase.SaveValuesOfVariables(Expresion, ExpressionField);
            Expresion = phrase.CheckVariablesinExpresion(Expresion);
            if (Expresion.Contains(":=") == false) Expresion = phrase.AddToNumberDot(Expresion);
            Expresion = Maxima.Eval(Expresion);
            Expresion = Expresion.Replace(":=", " = ");
            ResultList.Items.Add(Expresion);
            ExpressionField.Clear();
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
            ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());
            TipBox.Text = klawisz.ToolTip.ToString();
            TipBox.Foreground = Brushes.Green;
        }

        private void PlotChart_Click(object sender, RoutedEventArgs e)              //Funckja generująca wykres podanej funkcji #M
        {
            List<DataToChartsLine> DataToChartList = new List<DataToChartsLine>();
            DataToChartList.Clear();
            ListFunction.Clear();
            ListFunction1.Clear();
            var _ListBox = ChartListFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "FunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                ListFunction.Add(_Control.Text);                                                                                            //dodanie do listy funkcji występującej w TextBox #M
            }
            DataToChartList = DataToCharts.CountYwithX(ListFunction1, ListFunction, DataToChartList, DataToCharts, new MainWindow(), -6, 6); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            ViewPlot = new ViewPlot(DataToChartList);
            DataContext = ViewPlot;
            zoomin.zoomi = 7;                                                                                                                //poniżej ustawione zostały poarametry potrzebne do generowania kolejnnych puntow wykresu funkcji #M                                                                                                              
            zoomin.zoomj = -7;
            zoomin.startminzoom = 0.1;
            zoomin.endminzoom = 0.05;
            zoomin.countzoom = 0.01;
            zoomin.roundtozoom = 2;
            DataToCharts.zoommax = 0;

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
                    break;
                case "EqualizationAndFunctionsChB":
                    EqualizationAndFunctionsChLV.Height = 428;
                    DataSetChLV.Height = 20;
                    ParametricChLV.Height = 20;
                    InequalitiesChLV.Height = 20;
                    TraceChLV.Height = 20;
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

            List<DataToChartsLine> DataToChartList = new List<DataToChartsLine>();                                                                                                                                                                                                               //stworzenie listy punktow funkcji ktora będzie przekazywana do modelu wykresy #M
            DataToChartList = DataToCharts.CountYwithXWithUpdataTwoLine(ListFunction1, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi, zoomin.startminzoom, zoomin.endminzoom, zoomin.countzoom, zoomin.roundtozoom); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            int i = ViewPlot.UpdateModelZoomIN(DataToChartList, 0);                                                                                                                                                                                                                    //przekazanie do modelu wartości funkcji podzielonch na dwa wykresy nie przecinającego osi X #M                                                                                                              
            Plot.InvalidatePlot(true);//odświerzenei wykresu 
            DataToChartList.Clear();
            DataToChartList = DataToCharts.CountYwithXWithUpdata(ListFunction, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi);                                                                                        //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            ViewPlot.UpdateModelZoomIN(DataToChartList, i);                                                                                                                                                                                                                             //przekazanie do modelu wartości funkcji X #M                             
            Plot.InvalidatePlot(true);
            zoomin.zoomj = zoomin.zoomj - 1;                                                                                                                                                                                                                                            //zwiększenei punktów startowych dla przy kolejnym zwiększani wykresu #M 
            zoomin.zoomi = zoomin.zoomi + 1;                                                                                                                                                                                                                                            //zwiększenei punktów startowych dla przy kolejnym zwiększani wykresu #M 
            if (DataToCharts.zoommax == 0)                                                                                                                                                                                                                                              //zmniejszanie począwszy od 0.1 tak długo aż zoommax będzie różny od 0 oznacza to że półap zmniejszania X osiągnał swoją wartość końcową i nie da go bardzej pomniejszyć 
            {
                var helpzomm = zoomin.startminzoom;
                zoomin.startminzoom = zoomin.endminzoom;                                                                                                                                                                                                                                //ustalenie początku zwiększania dla X mniejszych od 0.1 #M
                zoomin.endminzoom = helpzomm / 10;                                                                                                                                                                                                                                      //ustalenie końca zwiększania dla X mniejszych od 0.1 #M
                if (zoomin.startminzoom.ToString().Contains("1") == true)                                                                                                                                                                                                               //warunek odpowiedzialny za ustalenie skali o ile ma się zwiększać X w danym obiegu pętli #M
                {
                    zoomin.countzoom = zoomin.countzoom / 10;                                                                                                                                                                                                                           //zmniejszenie skali zwiększania X #M
                    zoomin.roundtozoom++;
                }
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            List<DataToChartsLine> DataToChartList = new List<DataToChartsLine>();
            if (zoomin.endminzoom < 0.5)                                                        //dzięki warunkowi określam 
            {
                if (DataToCharts.zoommax == 0)                                                  //warunek sprawdzający czy możliwe jest jeszcze pomniejszenie 0.1 X #M
                {
                    var helpzomm = zoomin.endminzoom;
                    zoomin.endminzoom = zoomin.startminzoom;
                    zoomin.startminzoom = helpzomm * 10;
                }
                zoomin.zoomj = zoomin.zoomj + 1;                                                                                                                                                                                                                                            //zwiększanie X #M
                zoomin.zoomi = zoomin.zoomi - 1;                                                                                                                                                                                                                                           //zwiększanie X #<
                if (zoomin.startminzoom.ToString().Contains("1") != true)                                                                                                                                                                                                                  //warunek sprawdzający czy należy zmienić zakres w pomniejszaniu mniejszym od 0.1 #M
                {
                    zoomin.countzoom = zoomin.countzoom * 10;
                    zoomin.roundtozoom--;
                }
                DataToChartList = DataToCharts.CountYwithXWithUpdataTwoLine(ListFunction1, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi, zoomin.startminzoom, zoomin.endminzoom, zoomin.countzoom, zoomin.roundtozoom); //generowanie punktów dla funkcji która nie przechodzi przez oś X #M

                Plot.InvalidatePlot(true);                                                                                                                                                                                                                                                 //odświerzanie wykresu #M
                if (DataToCharts.zoommax > 0)                                                                                                                                                                                                                                              //sprawdzenie czy licznik pomniejszania jest większy od 0 jeśl jest to należy aktualizować wykres bez parametrów pomniejszających skale x o <0.1 i od liczniaka odjąc 2 dzięki czemu dla funkcji nie przekraczający X Y nie wzrośnie nam do bardzo dużych liczb #M
                {
                    DataToCharts.zoommax = DataToCharts.zoommax - 2;
                    ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, 0.0);
                }
                else ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, zoomin.startminzoom);                                                                                                                                                                 //gdy wartość x będzie już mieściła się w przedziałach x < 0.1 to oznacza że można zacząc pomniejszania z uwzględnienie pomniejszania o < 0.1 #M
            }
            DataToChartList = DataToCharts.CountYwithXWithUpdata(ListFunction, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi);                                                                                            //aktualizwanie wykresu który przecina oś X #M
            ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, 0.0);                                                                                                                                                                                          //aktualizwanie wykresu o wygenerowane punkty #M
            Plot.InvalidatePlot(true);                                                                                                                                                                                                                                                      //aktualizowanie wykresu #M
        }             //Funkcja zwiększająca wykres #M

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

            }
            if (ChartsOverLap.IsSelected)
                FormatujOverLap.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
