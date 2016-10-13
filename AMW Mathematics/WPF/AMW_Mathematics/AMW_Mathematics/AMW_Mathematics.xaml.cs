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
using AMW_Mathematics.Function;

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
        private ChartPointView ViewPlot;
        private DataToPointChartView DataToCharts = new DataToPointChartView();
        private ChartListViewLine chartlist = new ChartListViewLine();
        List<string> ListFunctionLine = new List<string>();                             //lista funkcji przechodzących przez oś X do wykresu #M
        List<string> ListFunctionPoint = new List<string>();                            //lista funkcji nie przechodzących przez oś X do wykresu #M
        List<DataToPointChartView> DataToChartList = new List<DataToPointChartView>();
        ChartLineView ChartLineView = new ChartLineView();
        PointChartFunction pointchartfunction = new PointChartFunction();
        private string typechart;
        public string betweenWindows = "";
        public MainWindow()
        {
            chartlist.CountFunction = "1";
            DataToCharts.CountFunction = 1;
            InitializeComponent();                                                 //stworzenie nowego obiektu kalsy ChartToData w celu dodania do listy możliwych zmiennych w wykresie #M
            List<ChartListViewLine> DataListView = new List<ChartListViewLine>();                           //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });      //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);                                                  //osobna klasa jeszcze nie wiem jaka :-)
            PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = DataToCharts.CountFunction.ToString(), Index = DataToCharts.CountFunction });
            DataSetChLV.Height = 20;                                                                    //osobna klasa jeszcze nie wiem jaka :-)
            ParametricChLV.Height = 20;
            InequalitiesChLV.Height = 20;
            TraceChLV.Height = 20;
            Maxima.Eval("load (\"functs\")");                                                           //załadowanie functs do Maximy(potrzebne do kilku funkcji) #Ł
            for (int i = 1; i < 2; i++)
            {
                
            }
            List<ChartListViewParametric> DataToViewParametric = new List<ChartListViewParametric>();
            DataToViewParametric.Add(new ChartListViewParametric { LabelChartParametricValue = "1" });
            ParametricChartListFunction.Items.Add(DataToViewParametric);

            List<ChartListViewInequalities> DataToViewInequalities = new List<ChartListViewInequalities>();
            DataToViewInequalities.Add(new ChartListViewInequalities { LabelChartInequalitiesValue = "1" });
            InequalitiesChartListFunction.Items.Add(DataToViewInequalities);

            for (int i = 1; i < 21; i++)
            {
                ListChartPointW.Items.Add(new ListPointChartView { ContentLabel = i.ToString(), XName = "X" + "i", YName = "Y" + i });
            }           
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
                if (Expresion.Contains("error"))
                {
                    if (Expresion.Contains("syntax"))
                    {
                        System.Windows.MessageBox.Show("Wystąpił błąd w składni wyrażenia popraw działanie i spróbuj ponownie", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (Expresion.Contains("rat"))
                {
                    string [] rat = Expresion.Split('=');
                    Expresion = rat.Last()+"     [WIP]";
                }
                ResultList.Items.Add(ExpressionField.Text + "=\n" + Expresion);
                ResultList.SelectedIndex = ResultList.Items.Count - 1;
                ResultList.ScrollIntoView(ResultList.Items[ResultList.Items.Count - 1]);
                ExpressionField.Clear();
                TipBox.Text = "Wprowadź wyrażenie i naciśnij Wprowadź";
                TipBox.Foreground = Brushes.Black;
            }
        }

        private void Keyboard_Click(object sender, RoutedEventArgs e)               //Funckja wprowadzająca cyfry i znaki z kalwiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            ExpressionField.Text = ExpressionField.Text + wartosc;
            ExpressionField.Focus();
            ExpressionField.SelectionStart = ExpressionField.Text.Length;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)                  //Funckja czyszcząca okno wprowadzania #Ł
        {
            ExpressionField.Text = "";
            TipBox.Text = "Wprowadź wyrażenie i naciśnij Wprowadź";
            TipBox.Foreground = Brushes.Black;
        }

        private void kBack_MouseDown(object sender, MouseButtonEventArgs e)         //Funckja cofająca z klawiatury "telefonu" #Ł
        {
            if (ExpressionField.Text.Length > 0)
            {
                ExpressionField.Text = ExpressionField.Text.Substring(0, ExpressionField.Text.Length - 1);
                ExpressionField.Focus();
                ExpressionField.SelectionStart = ExpressionField.Text.Length;
            }
        }
        private void Function_Click(object sender, RoutedEventArgs e)               //Funckja do prowadznie funckji z klawiatury "telefonu" #Ł
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            VariablesPopOut.IsOpen = false;
            if (worksheetTab.IsSelected)                                            //Wprowadzanie wartości przycisku na zakładce "Arkusz" #Ł
            {
                ExpressionField.Text = ExpressionField.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());
                TipBox.Text = klawisz.ToolTip.ToString();
                TipBox.Foreground = Brushes.Green;
                ExpressionField.Focus();
                ExpressionField.SelectionStart = ExpressionField.Text.Length;
            }
            if (ChartsOverLap.IsSelected)
            {
                var _ListBox = ChartListFunction as ListBox;
                foreach (var _ListBoxItem in _ListBox.Items)
                {
                    var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                    var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                    var _Name = "FunctionTextBox";
                    var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                    ListFunctionLine.Add(_Control.Text);                                                                                            //dodanie do listy funkcji występującej w TextBox #M
                    _Control.Text = _Control.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());
                };

                //Gdzie ma wprowadzić wartość w zakładce "wykresy" #Ł
            }
        }
        bool ToogleGridLineView = true;
        private void PlotChart_Click(object sender, RoutedEventArgs e)              //Funckja generująca wykres podanej funkcji #M
        {

            var chartbutton = (Button)sender;
            switch (chartbutton.Name)
            {
                case "PlotChart":
                    GraphHelpGrid.Visibility = Visibility.Hidden;
                    expPlotterControl.Visibility = Visibility.Visible;
                    ListFunctionLine.Clear();
                    var _ListBox = ChartListFunction as ListBox;
                    foreach (var _ListBoxItem in _ListBox.Items)
                    {
                        var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = "FunctionTextBox";
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                        ListFunctionLine.Add(_Control.Text);                                                                                            //dodanie do listy funkcji występującej w TextBox #M
                    };
                    ChartLineView.DrawChartLine(expPlotter, -5, 5, -5, 5, ListFunctionLine, ToogleGridLineView);
                    ToogleGridLineView = false;
                    typechart = "line";
                    break;
                case "PointPlotChart":
                    GraphHelpGrid.Visibility = Visibility.Hidden;
                    expPlotterControl.Visibility = Visibility.Hidden;
                    ListFunctionPoint.Clear();
                    List<DataToPointChartView> DataToChartPoint;
                    _ListBox = PointChartListFunction as ListBox;
                    foreach (var _ListBoxItem in _ListBox.Items)
                    {
                        var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = "PointFunctionTextBox";
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                        ListFunctionPoint.Add(_Control.Text);                                                                                            //dodanie do listy funkcji występującej w TextBox #M
                    }
                    DataToChartPoint = pointchartfunction.DataListFunction(new List<DataToPointChartView>(), ListFunctionPoint);
                    ViewPlot = new ChartPointView(DataToChartPoint);
                    DataContext = ViewPlot;
                    typechart = "point";
                    break;
            }
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
            Button buttonclicked = ((Button)sender);
            switch(buttonclicked.Name)
            {
                case "PointAddExpresionToPlot":
                    DataToCharts.CountFunction = DataToCharts.CountFunction + 1;
                    PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = DataToCharts.CountFunction.ToString(), Index = DataToCharts.CountFunction });
                    break;
                case "AddExpresionToPlot":
                    chartlist.CountFunction = (int.Parse(chartlist.CountFunction) + 1).ToString();
                    List<ChartListViewLine> DataListView = new List<ChartListViewLine>();
                    DataListView.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });
                    ChartListFunction.Items.Add(DataListView);
                    break;
            }
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
            switch (typechart)
            {
                case "line":
                    ChartLineView.ButtonZoomIn(expPlotter, ZoomInSeries);
                    break;
                case "point":
                    ViewPlot.UpdateModelZoomIn(Plot,ZoomInSeries);                                                                                                                                                                                                                             //przekazanie do modelu wartości funkcji X #M                             
                    break;
            }
        }
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            switch (typechart)
            {
                case "line":
                    ChartLineView.ButtonZoomOut(expPlotter, ZoomOutSeries);
                    break;
                case "point":
                    ViewPlot.UpdateModelZoomOut(Plot, ZoomOutSeries);
                    break;
            }
        }
        private void ChartLineTopPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineView.MoveUPChart(expPlotter);
            //ViewPlot.MoveUPChart(Plot);

        }
        private void ChartLineDownPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineView.MoveDownChart(expPlotter);
            //ViewPlot.MoveDownChart(Plot);
        }
        private void ChartLineRightPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineView.MoveRightChart(expPlotter);
            //ViewPlot.MoveRightChart(Plot);
        }
        private void ChartLineLeftPosition_Click(object sender, RoutedEventArgs e)
        {
            ChartLineView.MoveLeftChart(expPlotter);
            //ViewPlot.MoveLeftChart(Plot);
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
            {
                FormatujOverLap.Visibility = System.Windows.Visibility.Visible;
                GraphHelpGrid.Visibility = Visibility.Visible;
                expPlotterControl.Visibility = Visibility.Hidden;

            }

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

        private void kMin_MouseDown(object sender, MouseButtonEventArgs e)                  //Klawisz minimalizacji na klawiaturze #Ł
        {
            if (Calculus.Content.ToString() == "-")
            {
                CalculusTab.Height = 27;
                Calculus.Content = keyboard.Mark(Calculus.Content.ToString());
            }
            if (Statistic.Content.ToString() == "-")
            {
                StatisticTab.Height = 27;
                Statistic.Content = keyboard.Mark(Statistic.Content.ToString());
            }
            if (Trigonometry.Content.ToString() == "-")
            {
                TrigonometryTab.Height = 27;
                Trigonometry.Content = keyboard.Mark(Trigonometry.Content.ToString());
            }
            if (LinearAlgebra.Content.ToString() == "-")
            {
                LinearAlgebraTab.Height = 27;
                LinearAlgebra.Content = keyboard.Mark(LinearAlgebra.Content.ToString());
            }
            if (Standard.Content.ToString() == "-")
            {
                StandardTab.Height = 27;
                Standard.Content = keyboard.Mark(Standard.Content.ToString());
            }
            if (Favorite.Content.ToString() == "-")
            {
                FavoriteTab.Height = 27;
                Favorite.Content = keyboard.Mark(Favorite.Content.ToString());
            }


        }

        private void Variable_Click(object sender, RoutedEventArgs e)                   //Funkcja wywołania popout-u z zmeinnymi #Ł
        {
            VariablesPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            VariablesPopOut.HorizontalOffset = point.X;
            VariablesPopOut.VerticalOffset = point.Y-20;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)                      //Funkcja wywołania popout-u z menu #Ł
        {
            MenuPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            MenuPopOut.HorizontalOffset = point.X-10;
            MenuPopOut.VerticalOffset = point.Y - 20;
        }
        int indextextboxpointlist;
        private void Button_Click(object sender, RoutedEventArgs e)                 //Funkcja wywołania popout-u z zestawem danych #Ł
        {
            DataSetsPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            DataSetsPopOut.HorizontalOffset = point.X - 10;
            DataSetsPopOut.VerticalOffset = point.Y - 20;
            Button buttonclicked = ((Button)sender);
            indextextboxpointlist = int.Parse(buttonclicked.Tag.ToString()) - 1;

            List<TextBox> ListTextBox = new List<TextBox>();
            var _ListBox = PointChartListFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "PointFunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                ListTextBox.Add(_Control);                                                                                         //dodanie do listy funkcji występującej w TextBox #M
            }
            List<DataToPointChartView> DataToChartPoint;
            DataToChartPoint = pointchartfunction.DataListFunction(new List<DataToPointChartView>(), new List<string>() { ListTextBox[indextextboxpointlist].Text });
            List<TextBox> ListTextboxInPomList = new List<TextBox>();
            var _ListBoxs = ListChartPointW as ListBox;
            foreach (var _ListBoxItems in _ListBoxs.Items)
            {
                var _Containers = _ListBoxs.ItemContainerGenerator.ContainerFromItem(_ListBoxItems);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Containers);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "XValue";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                _Control.Text = "";
                ListTextboxInPomList.Add(_Control);
                _Name = "YValue";
                _Control = (TextBox)_Children.First(c => c.Name == _Name);
                _Control.Text = "";//dodanie do listy funkcji występującej w TextBox #M
                ListTextboxInPomList.Add(_Control);
            }
            int i = 0;
            foreach (var Point in DataToChartPoint)
            {
                ListTextboxInPomList[i].Text = Point.dataX.ToString();
                ListTextboxInPomList[i + 1].Text = Point.dataY.ToString();
                i = i + 2;
            }
        }

        private void DataSetCancel_Click(object sender, RoutedEventArgs e)          //Funkcja obsługująca Anuluj na popoucie DataSets #Ł
        {
            DataSetsPopOut.IsOpen = false;
        }

        private void DataSetOk_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> ListTextBox = new List<TextBox>();
            string function = "{";
            var _ListBox = ListChartPointW as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "XValue";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                if(_Control.Text != "") function += "{" + _Control.Text + ",";
                _Name = "YValue";
                _Control = (TextBox)_Children.First(c => c.Name == _Name);
                if (_Control.Text != "") function += _Control.Text + "}" + ",";                                //dodanie do listy funkcji występującej w TextBox #M
            }
            int index= function.LastIndexOf(",");
            function = function.Remove(index, 1);
            function = function + "}";
            _ListBox = PointChartListFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "PointFunctionTextBox";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                ListTextBox.Add(_Control);                                                                                         //dodanie do listy funkcji występującej w TextBox #M
            }
            ListTextBox[indextextboxpointlist].Text = function;
        }
    }
}
