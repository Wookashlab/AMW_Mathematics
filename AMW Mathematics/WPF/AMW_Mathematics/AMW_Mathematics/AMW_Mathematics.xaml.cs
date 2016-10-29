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
using MahApps.Metro;
using MaximaSharp;
using AMW_Mathematics.ModelView;
using System.Data;
using ExpressionPlotterControl;
using AMW_Mathematics.Function;
using AMW_Mathematics.Model;
using AMW_Mathematics.Windows;

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

        private DataToPointChartView datatopointchartview = new DataToPointChartView();

        private ChartListViewLine chartlist = new ChartListViewLine();

        private List<string> ListFunctionLine = new List<string>();                             //lista funkcji przechodzących przez oś X do wykresu #M

        private List<string> ListFunctionPoint = new List<string>();                            //lista funkcji nie przechodzących przez oś X do wykresu #M

        private List<DataToPointChartView> DataToChartList = new List<DataToPointChartView>();

        private ChartLineView ChartLineView = new ChartLineView();

        private PointChartFunction pointchartfunction = new PointChartFunction();

        private FunctionToAllPlot functiontoallpolot = new FunctionToAllPlot();

        private DataToLineChartView datatolinechartview = new DataToLineChartView();

        private DataToPointList datatopointlist = new DataToPointList();

        private DataToChart datatochart = new DataToChart();

        private DataLayout datalayout = new DataLayout();
        SplashScreen ss = new SplashScreen("img/SplashScreenv4.png");                           //Ustalenei jaki obraz ma być SplashScreenem #Ł
        Function.AppTheme Thememanager = new Function.AppTheme();                               //Instancja klasy obsługującej motyw aplikacji #Ł



        System.Windows.Forms.ToolTip TooltipToLineChart = new System.Windows.Forms.ToolTip();
        public string theme { get; set; }
        public string background { get; set; }
    
        public MainWindow()
        {

         //   ss.Show(true, true);                                                                //Wywołanie splashscreena który zawsze jest na górze i samoistnie się wyłącza po załadowaniu aplikacji #Ł
            InitializeApplication();
            
        }
        public void InitializeApplication()
        {

            chartlist.CountFunction = "1";
            datatopointchartview.CountFunction = 1;
            InitializeComponent();                                                 //stworzenie nowego obiektu kalsy ChartToData w celu dodania do listy możliwych zmiennych w wykresie #M
            List<ChartListViewLine> DataListView = new List<ChartListViewLine>();                           //osobna klasa jeszcze nie wiem jaka :-)
            DataListView.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });      //osobna klasa jeszcze nie wiem jaka :-)
            ChartListFunction.Items.Add(DataListView);                                                  //osobna klasa jeszcze nie wiem jaka :-)
            PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction });
            DataSetChLV.Height = 20;                                                                    //osobna klasa jeszcze nie wiem jaka :-)
            ParametricChLV.Height = 20;
            InequalitiesChLV.Height = 20;
            TraceChLV.Height = 20;
            Maxima.Eval("load (\"functs\")");                                                           //załadowanie functs do Maximy(potrzebne do kilku funkcji) #Ł
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

            datatolinechartview.ShowTooltip = true;
            datatolinechartview.ToogleGridLineView = true;
            datatochart.WhichGraphZoom = "";
            Thememanager.LoadTheme();                                                           //Załadowanie motywu z pliku #Ł
            ThemeChange();                                                                      //Ustawienie motywu #Ł
            RealNumber.IsChecked = true;                                                       //Włączenie trybu "Liczby rzeczywiste" #Ł
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            datalayout.VisibilityCalculatorPad = true;
            expPlotter.MouseMove += new System.Windows.Forms.MouseEventHandler(ExpPlotter_OnMouseMove);
            expPlotter.MouseWheel += new System.Windows.Forms.MouseEventHandler(ExpPlotter_OnMouseWheel);
            Tuple<MahApps.Metro.AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(Thememanager.accentColor), ThemeManager.GetAppTheme(Thememanager.themeColor));
        }

        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e)
        {

            if (ExpressionField.Text != "")
            {
                if ((bool)RealNumber.IsChecked)
                    if (ExpressionField.Text.Contains("%i"))
                    {
                        System.Windows.MessageBox.Show("Expression contains a complex number despite the fact that you are in real numbers mode. Change the expression or mode and try again ", " Error ", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

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
                        System.Windows.MessageBox.Show("There was an error in the expression syntax - correct expression and try againe", "Syntax Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (Expresion.Contains("rat"))
                {
                    string[] rat = Expresion.Split('=');
                    Expresion = rat.Last() + "     [WIP]";
                }
                ResultList.Items.Add("Input:      " + phrase.AddToNumberDot(ExpressionField.Text) + "\nOutput:   " + Expresion);
                ResultList.SelectedIndex = ResultList.Items.Count - 1;
                ResultList.ScrollIntoView(ResultList.Items[ResultList.Items.Count - 1]);
                ExpressionField.Clear();
                TipBox.Text = "Type an expression and then click Enter.";
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
            TipBox.Text = "Type an expression and then click Enter.";
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
                ListFunctionLine = functiontoallpolot.AddFunctionToList(ChartListFunction, ListFunctionLine, "FunctionTextBox", keyboard, klawisz, true);
            }
        }

        private void PlotChart_Click(object sender, RoutedEventArgs e)              //Funckja generująca wykres podanej funkcji #M
        {

            var chartbutton = (Button)sender;
            switch (chartbutton.Name)
            {
                case "PlotChart":

                    GraphHelpGrid.Visibility = Visibility.Hidden;
                    expPlotterControl.Visibility = Visibility.Visible;
                    ListFunctionLine.Clear();
                    ListFunctionLine = functiontoallpolot.AddFunctionToList(ChartListFunction, ListFunctionLine, "FunctionTextBox", new Keyboard(), new Button(), false);
                    if ((LineTypeChart.Items[LineTypeChart.SelectedIndex] as ComboBoxItem).Content.ToString() == "Cartesian")
                    {
                        double ys = FindRoundMiddle(ListFunctionLine[0]);
                        ChartLineView.DrawChartLine(expPlotter, -5, 5, ys - 5, ys + 5, ListFunctionLine, datatolinechartview.ToogleGridLineView, false);
                        datatolinechartview.ToogleGridLineView = false;
                    }
                    else
                    {
                        ChartLineView.DrawChartLine(expPlotter, -5, 5, -5, 5, ListFunctionLine, false, true);
                    }

                    datatolinechartview.TypeChart = "line";
                    datatochart.WhichGraphZoom = "PlotChart";
                    break;
                case "PointPlotChart":
                    GraphHelpGrid.Visibility = Visibility.Hidden;
                    expPlotterControl.Visibility = Visibility.Hidden;
                    Plot.Visibility = Visibility.Visible;
                    ListFunctionPoint.Clear();
                    List<DataToPointChartView> DataToChartPoint;
                    ListFunctionPoint = functiontoallpolot.AddFunctionToList(PointChartListFunction, ListFunctionPoint, "PointFunctionTextBox", new Keyboard(), new Button(), false);
                    DataToChartPoint = pointchartfunction.DataListFunction(new List<DataToPointChartView>(), ListFunctionPoint);
                    ViewPlot = new ChartPointView(DataToChartPoint);
                    DataContext = ViewPlot;
                    datatolinechartview.TypeChart = "point";
                    datatochart.WhichGraphZoom = "PointPlotChart";
                    break;
            }
        }

        private void AddExpresionToPlot_Click(object sender, RoutedEventArgs e)
        {
            Button buttonclicked = ((Button)sender);
            switch (buttonclicked.Name)
            {
                case "PointAddExpresionToPlot":
                    datatopointchartview.CountFunction = datatopointchartview.CountFunction + 1;
                    PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction });
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
                case 7:
                    ComplexTab.Height = 91 + wartosc * 64;
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
            string level;
            switch (datatolinechartview.TypeChart)
            {
                case "line":
                    level = (ChartZoomLevel.Items[ChartZoomLevel.SelectedIndex] as ComboBoxItem).Content.ToString().Replace("x", "");
                    for (int i = 0; i < int.Parse(level); i++)
                    {
                        ChartLineView.ButtonZoomIn(expPlotter, ZoomAfterX, ZoomAfterY);
                    }
                    break;
                case "point":
                    level = (ChartZoomLevel.Items[ChartZoomLevel.SelectedIndex] as ComboBoxItem).Content.ToString().Replace("x", "");
                    for (int i = 0; i < int.Parse(level); i++)
                    {
                        ViewPlot.UpdateModelZoomIn(Plot, ZoomAfterX, ZoomAfterY);
                    }                        
                    break;
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            string level;
            switch (datatolinechartview.TypeChart)
            {
                case "line":
                    level = (ChartZoomLevel.Items[ChartZoomLevel.SelectedIndex] as ComboBoxItem).Content.ToString().Replace("x", "");
                    for (int i = 0; i < int.Parse(level); i++)
                    {
                        ChartLineView.ButtonZoomOut(expPlotter, ZoomAfterX, ZoomAfterY);
                    }
                    break;
                case "point":
                    level = (ChartZoomLevel.Items[ChartZoomLevel.SelectedIndex] as ComboBoxItem).Content.ToString().Replace("x", "");
                    for (int i = 0; i < int.Parse(level); i++)
                    {
                        ViewPlot.UpdateModelZoomOut(Plot, ZoomAfterX, ZoomAfterY);
                    }
                    break;
            }
        }

        private void ChartLineTopPosition_Click(object sender, RoutedEventArgs e)
        {
            switch (datatochart.WhichGraphZoom)
            {
                case "PlotChart":
                    ChartLineView.MoveUPChart(expPlotter);
                    break;
                case "PointPlotChart":
                    ViewPlot.MoveUPChart(Plot);
                    break;
            }
        }

        private void ChartLineDownPosition_Click(object sender, RoutedEventArgs e)
        {
            switch (datatochart.WhichGraphZoom)
            {
                case "PlotChart":
                    ChartLineView.MoveDownChart(expPlotter);
                    break;
                case "PointPlotChart":
                    ViewPlot.MoveDownChart(Plot);
                    break;
            }
        }

        private void ChartLineRightPosition_Click(object sender, RoutedEventArgs e)
        {
            switch (datatochart.WhichGraphZoom)
            {
                case "PlotChart":
                    ChartLineView.MoveRightChart(expPlotter);
                    break;
                case "PointPlotChart":
                    ViewPlot.MoveRightChart(Plot);
                    break;
            }
        }

        private void ChartLineLeftPosition_Click(object sender, RoutedEventArgs e)
        {
            switch (datatochart.WhichGraphZoom)
            {
                case "PlotChart":
                    ChartLineView.MoveLeftChart(expPlotter);
                    break;
                case "PointPlotChart":
                    ViewPlot.MoveLeftChart(Plot);
                    break;
            }
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
                ShowElementListViewCharts(this.EqualizationAndFunctionsChB, null);
                FormatujOverLap.Visibility = System.Windows.Visibility.Visible;
                Plot.Visibility = Visibility.Hidden;
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
            string[] split = new string[] { "\n" };
            partExpression = ResultList.SelectedItem.ToString().Split(split, StringSplitOptions.RemoveEmptyEntries);
            ExpressionField.Text = partExpression[0].Remove(0, 12);
            ExpressionField.Focus();
            ExpressionField.SelectionStart = ExpressionField.Text.Length;

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
            if (Complex.Content.ToString() == "-")
            {
                ComplexTab.Height = 27;
                Complex.Content = keyboard.Mark(Complex.Content.ToString());
            }
        }

        private void Variable_Click(object sender, RoutedEventArgs e)                   //Funkcja wywołania popout-u z zmeinnymi #Ł
        {
            VariablesPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            VariablesPopOut.HorizontalOffset = point.X;
            VariablesPopOut.VerticalOffset = point.Y - 20;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)                      //Funkcja wywołania popout-u z menu #Ł
        {
            MenuPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            MenuPopOut.HorizontalOffset = point.X - 10;
            MenuPopOut.VerticalOffset = point.Y - 20;
        }

        private void Button_Click(object sender, RoutedEventArgs e)                 //Funkcja wywołania popout-u z zestawem danych #Ł
        {
            DataSetsPopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            DataSetsPopOut.HorizontalOffset = point.X - 10;
            DataSetsPopOut.VerticalOffset = point.Y - 20;
            Button buttonclicked = ((Button)sender);
            datatopointlist.IndexTextBox = int.Parse(buttonclicked.Tag.ToString()) - 1;
            List<TextBox> ListTextBox = new List<TextBox>();
            ListTextBox = pointchartfunction.FindBox(PointChartListFunction, "PointFunctionTextBox", "", "", ListTextBox, "First");
            List<DataToPointChartView> DataToChartPoint;
            DataToChartPoint = pointchartfunction.DataListFunction(new List<DataToPointChartView>(), new List<string>() { ListTextBox[datatopointlist.IndexTextBox].Text });
            List<TextBox> ListTextboxInPomList = new List<TextBox>();
            ListTextboxInPomList = pointchartfunction.FindBox(ListChartPointW, "", "XValue", "YValue", ListTextboxInPomList, "Second");
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
            function = pointchartfunction.FindFunctionInBox(ListChartPointW, function);
            ListTextBox = pointchartfunction.FindBox(PointChartListFunction, "PointFunctionTextBox", "", "", ListTextBox, "First");
            ListTextBox[datatopointlist.IndexTextBox].Text = function;
            DataSetsPopOut.IsOpen = false;
        }

        private void ExpPlotter_OnMouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
                expPlotter.ZoomIn();
            else if (e.Delta < 0)
                expPlotter.ZoomOut();
            expPlotter.Refresh();
        }

        private void Chart_Click(object sender, EventArgs e)
        {
            if (datatolinechartview.ShowTooltip == true)
            {
                TooltipToLineChart.ReshowDelay = 0;
                TooltipToLineChart.InitialDelay = 0;
                TooltipToLineChart.OwnerDraw = true;
                TooltipToLineChart.Draw += new System.Windows.Forms.DrawToolTipEventHandler(toolTip1_Draw);
                TooltipToLineChart.Popup += new System.Windows.Forms.PopupEventHandler(toolTip1_Popup);
                datatolinechartview.TooltipData = "X = " + datatolinechartview.currentX + " \nY = " + datatolinechartview.currentY;
                TooltipToLineChart.Show(datatolinechartview.TooltipData, expPlotter);
                datatolinechartview.ShowTooltip = false;
            }
            else
            {
                TooltipToLineChart.Hide(expPlotter);
                datatolinechartview.ShowTooltip = true;
            }
        }

        private void ExpPlotter_OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            datatolinechartview.currentX = (e.X - expPlotter.Width / 2) * expPlotter.ScaleX / expPlotter.Width * 2.25 + expPlotter.ForwardX;
            datatolinechartview.currentY = (expPlotter.Height / 2 - e.Y) * expPlotter.ScaleY / expPlotter.Height * 2.25 + expPlotter.ForwardY;
            if (this.expPlotter.GraphMode == GraphMode.Polar)
            {
                double r = ChartLineView.GetR(datatolinechartview.currentX, datatolinechartview.currentY);
                double theta = ChartLineView.GetTheta(datatolinechartview.currentX, datatolinechartview.currentY);
                datatolinechartview.currentX = r;
                datatolinechartview.currentY = theta;
            }
            datatolinechartview.currentX = Math.Round(datatolinechartview.currentX, 3);
            datatolinechartview.currentY = Math.Round(datatolinechartview.currentY, 3);
        }

        private void toolTip1_Popup(object sender, System.Windows.Forms.PopupEventArgs e)
        {
            e.ToolTipSize = System.Windows.Forms.TextRenderer.MeasureText(datatolinechartview.TooltipData, new System.Drawing.Font("Arial", 13.0f));
        }

        private void toolTip1_Draw(object sender, System.Windows.Forms.DrawToolTipEventArgs e)
        {
            System.Drawing.Font f = new System.Drawing.Font("Arial", 13.0f);
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, f, System.Drawing.Brushes.Black, new System.Drawing.PointF(2, 2));
        }

        private void DataSetClear_Click(object sender, RoutedEventArgs e)
        {

        }
        public double FindRoundMiddle(string expression)                                        //Funkcja znajdująca środek wykresu #Ł
        {
            if (expression.Contains(":=") == false) expression = phrase.AddToNumberDot(expression);         //dopisanie do wyrażenia .0 #Ł
            if (Maxima.Eval("limit(" + expression + ",x,0)").Contains("infinity"))                              //sprawdzenie czy nie próbujemy dzielić przez 0 #Ł
                expression = expression.Replace("x", "(1.0)");                                              //jeżeli tak to zminiamy wartość na 1 #Ł
            else expression = expression.Replace("x", "(0.0)");                                             //jeżeli nie to zostajmey z wartością 0 #Ł
            if (expression.Contains(":=") == false) expression = phrase.AddToNumberDot(expression);          //dopisanie do wyrażenia .0 #Ł
            expression = Maxima.Eval(expression);                                                           //obliczenie wyrażenia dla podanej wartości #Ł
            double result = double.Parse(expression.Replace('.', ','));                                     //zamiana kropek na przecinki (maxima->double)#Ł
            result = Math.Round(result);                                                                    //zaokrąglenie wyniku #Ł
            return result;                                                                                  //zwrócenie wyniku #Ł
        }

        private void ComplexNumber_Checked(object sender, RoutedEventArgs e)                    //Funkcja uruchamiająca tryb Liczyb rzeczywiste #Ł
        {
            ComplexTab.Visibility = System.Windows.Visibility.Visible;                          //wyświetlenie pasku funkcji na kalkulatroze "Liczby zespolone #Ł
            System.Windows.MessageBox.Show("You are now in the complex numbers mode. The imaginary unit in our program is determined by use of the phrase \" % i \" Ex. 2 + 3 *% i ", " Complex Numbers Mode", MessageBoxButton.OK, MessageBoxImage.Question);
            
        }

        private void RealNumber_Checked(object sender, RoutedEventArgs e)                       //Funkcja uruchamiająca tryb liczby zespolone #Ł
        {

            ComplexTab.Visibility = System.Windows.Visibility.Collapsed;                    // ukrycie pasku funkcji na kalkulatorze "Liczby zespolone" #Ł
        }
        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            VariablesListView.Items.Clear();
            phrase.SymbolsAndValues.Clear();
            ResultList.Items.Clear();
            ChartListFunction.Items.Clear();
            PointChartListFunction.Items.Clear();
            ParametricChartListFunction.Items.Clear();
            InequalitiesChartListFunction.Items.Clear();
            expPlotter.RestoreDefaults();
            LineTypeChart.SelectedItem = LineTypeChart.Items[0];
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
            if (Favorite.Content.ToString() == "-")
            {
                FavoriteTab.Height = 27;
                Favorite.Content = keyboard.Mark(Favorite.Content.ToString());
            }
            StandardTab.Height = 213;
            Standard.Content = keyboard.Mark(Standard.Content.ToString());

            InitializeApplication();
            Plot.InvalidatePlot(true);
            worksheetTab.IsSelected = true;
        }

        private void AplicationExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private string filename = "";
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            List<string> ToSave = new List<string>();
            foreach (string item in ResultList.Items) ToSave.Add(item);
            ToSave.Add("ChartLine");
            foreach (var item in ListFunctionLine) ToSave.Add(item);
            ToSave.Add("ChartPoint");
            foreach (var item in ListFunctionPoint) ToSave.Add(item);
            ToSave.Add("Variables");
            foreach (var item in phrase.SymbolsAndValues) ToSave.Add(item.Key + " = " + item.Value);
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Expresions"; // Default file name
            dlg.DefaultExt = ".data"; // Default file extension
            dlg.Filter = "Expresions (.Data)|*.Data"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process save file dialog box results
            if (result == true)
            {
                filename = dlg.FileName;
                Serialization.ConcurrentSerializer<List<string>>.Serialize(filename, ToSave);
            }
        }
        private void OpenProject_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Expresions"; // Default file name
            dlg.DefaultExt = ".data"; // Default file extension
            dlg.Filter = "Expresions (.Data)|*.Data"; // Filter files by extension
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                List<string> ToSave = new List<string>();
                ToSave = Serialization.ConcurrentSerializer<List<string>>.Deserialize(dlg.FileName);
                ResultList.Items.Clear();
                PointChartListFunction.Items.Clear();
                chartlist.CountFunction = "1";
                datatopointchartview.CountFunction = 1;
                foreach (var item in ToSave)
                {
                    if (item != "ChartLine")
                    {
                        ResultList.Items.Add(item);
                    }
                    else
                    {
                        int index = ToSave.IndexOf(item);
                        ToSave.RemoveRange(0, index + 1);
                        break;
                    }
                }
                ChartListFunction.Items.Clear();
                foreach (var item in ToSave)
                {
                    if (item != "ChartPoint")
                    {
                        ChartListFunction.Items.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction, TextInChartPlot = item });
                    }
                    else
                    {
                        int index = ToSave.IndexOf(item);
                        ToSave.RemoveRange(0, index + 1);
                        break;
                    }
                    chartlist.CountFunction = (int.Parse(chartlist.CountFunction) + 1).ToString();
                }
                if (ChartListFunction.Items.Count == 0) ChartListFunction.Items.Add(new ChartListViewLine { LabelChartValue = chartlist.CountFunction });
                foreach (var item in ToSave)
                {
                    if(item != "Variables")
                    {
                        PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction, TextBoxText = item });
                    }
                    else
                    {
                        int index = ToSave.IndexOf(item);
                        ToSave.RemoveRange(0, index + 1);
                        break;
                    }                   
                    datatopointchartview.CountFunction = datatopointchartview.CountFunction + 1;
                }
                if (PointChartListFunction.Items.Count == 0) PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction });
                VariablesListView.Items.Clear();
                phrase.SymbolsAndValues.Clear();
                int ind = 0;
                foreach(var item in ToSave)
                {
                    VariablesListView.Items.Add(new VariablesListView { Variable = item, Index = ind });
                    ind++;
                    string key = item.Substring(0, item.IndexOf("=") - 1);
                    string value = item.Substring(item.IndexOf("=") + 1, item.Length-1- item.IndexOf("="));
                    phrase.SymbolsAndValues.Add(key, value);
                }
            }
        }
        private void Sava_Click(object sender, RoutedEventArgs e)
        {
            List<string> ToSave = new List<string>();
            if (filename == "")
            {
                foreach (string item in ResultList.Items) ToSave.Add(item);
                ToSave.Add("ChartLine");
                foreach (var item in ListFunctionLine) ToSave.Add(item);
                ToSave.Add("ChartPoint");
                foreach (var item in ListFunctionPoint) ToSave.Add(item);
                ToSave.Add("Variables");
                foreach (var item in phrase.SymbolsAndValues) ToSave.Add(item.Key + " = " + item.Value);
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();
                // Process save file dialog box results
                if (result == true)
                {
                    filename = dlg.FileName;
                    Serialization.ConcurrentSerializer<List<string>>.Serialize(filename, ToSave);
                }
            }
            else
            {
                foreach (string item in ResultList.Items) ToSave.Add(item);
                ToSave.Add("ChartLine");
                foreach (var item in ListFunctionLine) ToSave.Add(item);
                ToSave.Add("ChartPoint");
                foreach (var item in ListFunctionPoint) ToSave.Add(item);
                Serialization.ConcurrentSerializer<List<string>>.Serialize(filename, ToSave);
            }
        }

        private void ThemaMenu_Click(object sender, RoutedEventArgs e)                                  //Otwarcie PopOutu ThemeMenu #Ł
        {
            ThemePopOut.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            ThemePopOut.HorizontalOffset = point.X - 10;
            ThemePopOut.VerticalOffset = point.Y - 20;

        }

        private void ColorAccept_Click(object sender, RoutedEventArgs e)                             //Obsługa zmiany koloru programu w popout-cie #Ł       
        {
            Thememanager.accentColor = SelectedColor.SelectedItem.ToString().Remove(0, 37);         //Zczytanie wybranego akcentu #Ł
            Thememanager.themeColor = SelectedTheme.SelectedItem.ToString().Remove(0, 37);          //Zczytanie wybranego motywu #Ł
            ThemePopOut.IsOpen = false;                                                             //Zamknięcie popout-u #Ł
            ThemeChange();                                                                          //Wywołanie zmian koloru aplikacji #Ł
            Thememanager.SaveThem();                                                                //Zapisanie wybranych zmian do pliku #Ł

        }
        private void ThemeChange()                                                                  //Zmiana koloru aplikacji #Ł
        {
            Thememanager.Accent(Thememanager.accentColor);                                          //Wygenerowanie kodu koloru odpowidającego wybranemu kolorowi w klasie AppTheme #Ł
            ThemePopOutBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Thememanager.accentColorCode));                               //Zmiana koloru popoutów #Ł
            DataSetsPopOutBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Thememanager.accentColorCode));
            MenuPopOutBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Thememanager.accentColorCode));
            VariablesPopOutBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Thememanager.accentColorCode));
            VariablePopBorder.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Thememanager.accentColorCode));

            Tuple<MahApps.Metro.AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);                  //Zmiana motuwy aplikacji #Ł
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(Thememanager.accentColor), ThemeManager.GetAppTheme(Thememanager.themeColor));
        }

        private void AcceptXY_Click(object sender, RoutedEventArgs e)
        {
            switch (datatochart.WhichGraphZoom)
            {
                case "PlotChart":
                    ChartLineView.SetRange(expPlotter, double.Parse(MinX.Text), double.Parse(MaxX.Text), double.Parse(MinY.Text), double.Parse(MaxY.Text));
                    expPlotter.Refresh();
                    break;
                case "PointPlotChart":
                    ViewPlot.SetXAndY(Plot, double.Parse(MinX.Text), double.Parse(MaxX.Text), double.Parse(MinY.Text), double.Parse(MaxY.Text));
                    break;
            }
        }

        private void Hidde_CalculatorPad_Click(object sender, RoutedEventArgs e)
        {
            if(datalayout.VisibilityCalculatorPad == true)
            {
                Function_list.Visibility = Visibility.Hidden;
                Pad_Atribute.Visibility = Visibility.Hidden;
                Worksheetadn.Margin = new Thickness(10, 188, 0, 0);
                Worksheetadn.Width = 1191;
                datalayout.VisibilityCalculatorPad = false;
            }else
            {
                Function_list.Visibility = Visibility.Visible;
                Pad_Atribute.Visibility = Visibility.Visible;
                Worksheetadn.Margin = new Thickness(322, 188, 0, 0);
                Worksheetadn.Width = 879;
                datalayout.VisibilityCalculatorPad = true;
            }         
        }

        private void Stored_Variable_Click(object sender, RoutedEventArgs e)
        {
            VariablePop.IsOpen = true;
            var point = Mouse.GetPosition(Application.Current.MainWindow);
            VariablePop.HorizontalOffset = point.X - 10;
            VariablePop.VerticalOffset = point.Y - 20;
            VariablesListView.Items.Clear();
            int index = 0;
            foreach(var item in phrase.SymbolsAndValues)
            {              
                VariablesListView.Items.Add(new VariablesListView {Variable = item.Key + " = " + item.Value.Insert(item.Value.Length, " "), Index = index});
                index++;
            }
        }

        private void ClearVariable_Click(object sender, RoutedEventArgs e)
        {
            var klawisz = (Button)sender;
            var item = VariablesListView.Items[int.Parse(klawisz.Tag.ToString())];
            VariablesListView.Items.Remove(item);
            var variable = item as VariablesListView;
            int ind = variable.Variable.IndexOf("=");
            string key = variable.Variable.Substring(0, ind-1).ToString();
            phrase.SymbolsAndValues.Remove(key);
        }

        private void Clear_Variables_Click(object sender, RoutedEventArgs e)
        {
            VariablesListView.Items.Clear();
            phrase.SymbolsAndValues.Clear();
            VariablePop.IsOpen = false;
        }

        private void TriangleSolverButton_Click(object sender, RoutedEventArgs e)
        {
            TriangleSolver trianglesolverwindow = new TriangleSolver(Thememanager.accentColorCode);
            trianglesolverwindow.ShowDialog();
        }

        private void EquationSolverButton_Click(object sender, RoutedEventArgs e)
        {
            EquationSolver equationsolver = new EquationSolver();
            equationsolver.ShowDialog();
        }
    }
}
