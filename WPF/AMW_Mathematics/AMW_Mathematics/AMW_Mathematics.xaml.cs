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
        private Dictionary<string, string> SymbolsAndValues;
        private ViewPlot ViewPlot;
        
        public MainWindow()
        {
            InitializeComponent();
            SymbolsAndValues = new Dictionary<string, string>();
            List<ChartListView> DataListView = new List<ChartListView>(); //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListView { LabelChartValue = "1" }); //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);  //osobna klasa jeszcze nie wiem jaka :-)
            DataSetChLV.Height = 20;  //osobna klasa jeszcze nie wiem jaka :-)
            Maxima.Eval("load (\"functs\")");                                       //załadowanie functs(potrzebne do kilku funkcji #Ł
            
        }
        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e) 
        {
            string Expresion = ExpressionField.Text;
            Expresion = SaveValuesOfVariables(Expresion);
            Expresion = CheckVariablesinExpresion(Expresion);
            if(Expresion.Contains(":=")==false) Expresion = AddToNumberDot(Expresion);
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

        private string AddToNumberDot(string Expresion)
        {
           string pom = "";
           for(int i = 0; i < Expresion.Length; i++)
           {
               if(Char.IsNumber(Expresion[i]) == true)
               {
                   for(int j = i; j < Expresion.Length; j++)
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
                                if (pom.Contains(".")!= true)
                                {
                                    Expresion = Expresion.Insert(j, ".0");
                                    pom = "";
                                    i = j+2;
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
                                Expresion = Expresion.Insert(j+1, ".0");
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)
                                {
                                    Expresion = Expresion.Insert(j+1, ".0");
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
            ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(),klawisz.Content.ToString());
            TipBox.Text = klawisz.ToolTip.ToString();
            TipBox.Foreground = Brushes.Green;
            
        }

        private void PlotChart_Click(object sender, RoutedEventArgs e)
        {
            var _ListBox = ChartListFunction as ListBox;
            string value;
            foreach(var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem); //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container); //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "FunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name); //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                value = _Control.Text;
            }
            List<DataToChart> DataToChart = new List<DataToChart>();
            DataToChart.Add(new DataToChart { Axis = 90, Ayis = 5 });
            DataToChart.Add(new DataToChart { Axis = 92, Ayis = 6 });
            DataToChart.Add(new DataToChart { Axis = 95, Ayis = 7 });
            DataToChart.Add(new DataToChart { Axis = 97, Ayis = 8 });
            DataToChart.Add(new DataToChart { Axis = 99, Ayis = 9 });
            ViewPlot = new ViewPlot(DataToChart);           
            DataContext = ViewPlot;
        }//trzeba dokonczyc

        public List<Control> AllChildren(DependencyObject parent) 
        {
            var _List = new List<Control> { };
            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(parent);i++) 
            {
                var _Child = VisualTreeHelper.GetChild(parent, i); //wprowadzenie do zmiennej dziecka Elementu ListView #M
                if (_Child is Control) //sprawdzenie czy jest dziecko jest kontrolką #M
                    _List.Add(_Child as Control); //Jeśli tak dodananie go do listy #M
                _List.AddRange(AllChildren(_Child)); //Rekurencyjne sprawdzenie czy dziecko ListView nie ma dzieci które też są kontrolkami #M
            }
            return _List; //zwrócenie listy Kontrolek ListView #M
        }//funkcja wyszukuje wszysktie kontrolki znajdujące się w danej Liście #M

        private void AddExpresionToPlot_Click(object sender, RoutedEventArgs e)
        {
            List<ChartListView> DataListView = new List<ChartListView>();
            DataListView.Add(new ChartListView { LabelChartValue = "2" });
            ChartListFunction.Items.Add(DataListView);
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
            switch(keysender.Name) //sprawdzenie który przycisk został wciśnięty i na podstawie tego wyświetlenie odpowiednich pól w liście ListViewChart #M
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

        private void Variable_Click(object sender, RoutedEventArgs e)               //Okno zmiennych (tymczasowe) #Ł
        {
            VariableWindow test = new VariableWindow();
            test.Show();
        }
    }
}
