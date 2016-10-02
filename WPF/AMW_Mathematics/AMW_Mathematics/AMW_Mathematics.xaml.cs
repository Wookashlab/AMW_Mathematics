﻿using System;
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
    public class ZoomIN
    {
        public int zoomj { get; set; }
        public int zoomi { get; set; }
        public double startminzoom { get; set; }
        public double endminzoom { get; set; }
        public double countzoom { get; set; }
        public int roundtozoom { get; set; }
    }
    public partial class MainWindow : MetroWindow
    {
        private Keyboard keyboard = new Keyboard();                                 //obiekt klasy Keyboard do obsługi wirtualnego "telefonu" #Ł
        private Dictionary<string, string> SymbolsAndValues;
        private ViewPlot ViewPlot;
<<<<<<< HEAD
        private DataToChart DataToCharts;
        private ChartListView chartlist = new ChartListView();
        private ZoomIN zoomin = new ZoomIN();
=======
        
>>>>>>> FunkcjeV2
        public MainWindow()
        {
            chartlist.CountFunction = "1";
            InitializeComponent();
            SymbolsAndValues = new Dictionary<string, string>();
<<<<<<< HEAD
            DataToCharts = new DataToChart();                                   //stworzenie nowego obiektu kalsy ChartToData w celu dodania do listy możliwych zmiennych w wykresie #M
            List<ChartListView> DataListView = new List<ChartListView>();       //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListView { LabelChartValue = chartlist.CountFunction });      //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);                          //osobna klasa jeszcze nie wiem jaka :-)
            DataSetChLV.Height = 20;                                            //osobna klasa jeszcze nie wiem jaka :-)
=======
            List<ChartListView> DataListView = new List<ChartListView>(); //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListView { LabelChartValue = "1" }); //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);  //osobna klasa jeszcze nie wiem jaka :-)
            DataSetChLV.Height = 20;  //osobna klasa jeszcze nie wiem jaka :-)
            Maxima.Eval("load (\"functs\")");                                       //załadowanie functs(potrzebne do kilku funkcji #Ł
            
>>>>>>> FunkcjeV2
        }
        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e)
        {
            string Expresion = ExpressionField.Text;
            Expresion = SaveValuesOfVariables(Expresion);
            Expresion = CheckVariablesinExpresion(Expresion);
            if (Expresion.Contains(":=") == false) Expresion = AddToNumberDot(Expresion);
            Expresion = Maxima.Eval(Expresion);
            Expresion = Expresion.Replace(":=", " = ");
            ResultList.Items.Add(Expresion);
            ExpressionField.Clear();
        }

        private string SaveValuesOfVariables(string Expresion)
        {
            if (Expresion.Contains(":=") == true) //sprawdzenie wyrażenie ma stworzyć zmienna
            {
                Expresion = AddToNumberDot(ExpressionField.Text.Substring(Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":")) + 2));
                Expresion = Maxima.Eval(Expresion);
                SymbolsAndValues.Add(ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))), Expresion);
                return ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))) + ":=" + Expresion;
            }
            else return Expresion;

        }

        public string AddToNumberDot(string Expresion)
        {
            string pom = "";
            for (int i = 0; i < Expresion.Length; i++)
            {
                if (Char.IsNumber(Expresion[i]) == true)
                {
                    for (int j = i; j < Expresion.Length; j++)
                    {
                        if (Char.IsNumber(Expresion[j]) != true && Expresion[j] != '.')
                        {
                            if (pom.Length == 1)
                            {
                                Expresion = Expresion.Insert(j, ".0");
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)
                                {
                                    Expresion = Expresion.Insert(j, ".0");
                                    pom = "";
                                    i = j + 2;
                                    break;
                                }
                                else
                                {
                                    pom = "";
                                    i = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pom = pom + Expresion[j];
                        }
                        if (i + 1 == Expresion.Length)
                        {
                            if (pom.Length == 1)
                            {
                                Expresion = Expresion.Insert(j + 1, ".0");
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)
                                {
                                    Expresion = Expresion.Insert(j + 1, ".0");
                                    pom = "";
                                    i = j + 2;
                                    break;
                                }
                                else
                                {
                                    pom = "";
                                    i = j;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return Expresion;
        }

        private string CheckVariablesinExpresion(string Expresion)
        {
            if (Expresion.Contains(":=") != true)
            {
                foreach (var symbol in SymbolsAndValues)
                {
                    Expresion = Expresion.Replace(symbol.Key, symbol.Value);
                }
            }
            return Expresion;
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)       //funckja wprowadzająca cyfry i znaki z kalwiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            ExpressionField.Text = ExpressionField.Text + wartosc;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)          //funckja czyszcząca okno wprowadzania #Ł
        {
            ExpressionField.Text = "";
        }

        private void kBack_MouseDown(object sender, MouseButtonEventArgs e) //funckja cofająca z klawiatury "telefonu" #Ł
        {
            if (ExpressionField.Text.Length > 0)
            {
                ExpressionField.Text = ExpressionField.Text.Substring(0, ExpressionField.Text.Length - 1);

            }
        }

        private void Function_Click(object sender, RoutedEventArgs e)       //funckja do prowadznie funckji z klawiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
<<<<<<< HEAD
            ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());
=======
            ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(),klawisz.Content.ToString());
            TipBox.Text = klawisz.ToolTip.ToString();
            TipBox.Foreground = Brushes.Green;
            
>>>>>>> FunkcjeV2
        }

        List<string> ListFunction = new List<string>();

        List<string> ListFunction1 = new List<string>();

        private void PlotChart_Click(object sender, RoutedEventArgs e)
        {
            List<DataToChart> DataToChartList = new List<DataToChart>();
            DataToChartList.Clear();
            ListFunction.Clear();
            ListFunction1.Clear();
            var _ListBox = ChartListFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "FunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                ListFunction.Add(_Control.Text);                                                                            //dodanie do listy funkcji występującej w TextBox #M
            }
            DataToChartList = DataToCharts.CountYwithX(ListFunction1, ListFunction, DataToChartList, DataToCharts, new MainWindow(), -6, 6); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            ViewPlot = new ViewPlot(DataToChartList);
            DataContext = ViewPlot;
            zoomin.zoomi = 7;
            zoomin.zoomj = -7;
            zoomin.startminzoom = 0.1;
            zoomin.endminzoom = 0.05;
            zoomin.countzoom = 0.01;
            zoomin.roundtozoom = 2;
            DataToCharts.zoommax = 0;
        }

        public List<Control> AllChildren(DependencyObject parent)
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
        }                                                                           //funkcja wyszukuje wszysktie kontrolki znajdujące się w danej Liście #M

        private void AddExpresionToPlot_Click(object sender, RoutedEventArgs e)
        {
            chartlist.CountFunction = (int.Parse(chartlist.CountFunction) + 1 ).ToString();
            List<ChartListView> DataListView = new List<ChartListView>();
            DataListView.Add(new ChartListView { LabelChartValue = chartlist.CountFunction });
            ChartListFunction.Items.Add(DataListView);
            //  ViewPlot.UpdateModel();
            //  Plot.InvalidatePlot(true);
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

        private void Tab_Click(object sender, RoutedEventArgs e)            //funckja po wciśnieciu + lub - na karcie #Ł
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

        private void ShowElementListViewCharts(object sender, RoutedEventArgs e)
        {
            var keysender = (Button)sender; //pobranie nazwy przycisku danej karty
            switch (keysender.Name) //sprawdzenie który przycisk został wciśnięty i na podstawie tego wyświetlenie odpowiednich pól w liście ListViewChart #M
            {
                case "DataSetChB":
                    EqualizationAndFunctionsChLV.Height = 20;
                    DataSetChLV.Height = 428;
                    break;
                case "EqualizationAndFunctionsChB":
                    EqualizationAndFunctionsChLV.Height = 428;
                    DataSetChLV.Height = 20;
                    break;
            }
        } //po wciśnięciu + otwiera kartę w liście ListViewChart #M

<<<<<<< HEAD
        private void ZoomIN_Click(object sender, RoutedEventArgs e) //do poprawienia
        {

            List<DataToChart> DataToChartList = new List<DataToChart>();
            DataToChartList = DataToCharts.CountYwithXWithUpdataTwoLine(ListFunction1, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi, zoomin.startminzoom, zoomin.endminzoom, zoomin.countzoom, zoomin.roundtozoom); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            int i = ViewPlot.UpdateModelZoomIN(DataToChartList, 0);
            Plot.InvalidatePlot(true);
            DataToChartList.Clear();
            DataToChartList = DataToCharts.CountYwithXWithUpdata(ListFunction, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            ViewPlot.UpdateModelZoomIN(DataToChartList, i);
            Plot.InvalidatePlot(true);
            zoomin.zoomj = zoomin.zoomj - 1;
            zoomin.zoomi = zoomin.zoomi + 1;
            if (DataToCharts.zoommax == 0)
            {
                var helpzomm = zoomin.startminzoom;
                zoomin.startminzoom = zoomin.endminzoom;
                zoomin.endminzoom = helpzomm / 10;
                if (zoomin.startminzoom.ToString().Contains("1") == true)
                {
                    zoomin.countzoom = zoomin.countzoom / 10;
                    zoomin.roundtozoom++;
                }
            }
        }
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            List<DataToChart> DataToChartList = new List<DataToChart>();
            if (zoomin.endminzoom < 0.5)
            {
                if (DataToCharts.zoommax == 0)
                {
                    var helpzomm = zoomin.endminzoom;
                    zoomin.endminzoom = zoomin.startminzoom;
                    zoomin.startminzoom = helpzomm * 10;
                }
                zoomin.zoomj = zoomin.zoomj + 1;
                zoomin.zoomi = zoomin.zoomi - 1;
                if (zoomin.startminzoom.ToString().Contains("1") != true)
                {
                    zoomin.countzoom = zoomin.countzoom * 10;
                    zoomin.roundtozoom--;
                }
                DataToChartList = DataToCharts.CountYwithXWithUpdataTwoLine(ListFunction1, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi, zoomin.startminzoom, zoomin.endminzoom, zoomin.countzoom, zoomin.roundtozoom); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M

                Plot.InvalidatePlot(true);
                if (DataToCharts.zoommax > 0)
                {
                    DataToCharts.zoommax = DataToCharts.zoommax - 2;
                    ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, 0.0);
                }
                else ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, zoomin.startminzoom);
            }
            DataToChartList = DataToCharts.CountYwithXWithUpdata(ListFunction, DataToChartList, DataToCharts, new MainWindow(), zoomin.zoomj + 1, zoomin.zoomi - 1, zoomin.zoomj, zoomin.zoomi); //zwrócenie do listy obliczonych wartości funkcji w zdanym x #M
            ViewPlot.UpdateModelZoomOUT(DataToChartList, zoomin.zoomi - 1, zoomin.zoomj + 1, 0.0);
            Plot.InvalidatePlot(true);
=======
        private void Variable_Click(object sender, RoutedEventArgs e)               //Okno zmiennych (tymczasowe) #Ł
        {
            VariableWindow test = new VariableWindow();
            test.Show();
>>>>>>> FunkcjeV2
        }
    }
}
