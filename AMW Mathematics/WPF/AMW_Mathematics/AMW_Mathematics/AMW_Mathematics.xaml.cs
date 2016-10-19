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
using AMW_Mathematics.Model;

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

        System.Windows.Forms.ToolTip TooltipToLineChart = new System.Windows.Forms.ToolTip();

        public MainWindow()
        {
            InitializeApplication();
            InitializeComponent();
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
            RealNumber.IsChecked = true;                                       //Włączenie trybu "Liczby rzeczywiste" #Ł
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComplexNumber comp = new ComplexNumber();
            expPlotter.MouseMove += new System.Windows.Forms.MouseEventHandler(ExpPlotter_OnMouseMove);
            expPlotter.MouseWheel += new System.Windows.Forms.MouseEventHandler(ExpPlotter_OnMouseWheel);
        }

        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e)
        {

            if (ExpressionField.Text != "")
            {
                if ((bool)RealNumber.IsChecked)
                    if (ExpressionField.Text.Contains("%i"))
                    {
                        System.Windows.MessageBox.Show("Podane wyrażenie zawiera liczby złożone mimo tego, że jesteś w trybie liczb rzeczywistych. Zmień wyrażenie lub tryb i spróbuj ponownie", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        System.Windows.MessageBox.Show("Wystąpił błąd w składni wyrażenia popraw działanie i spróbuj ponownie", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (Expresion.Contains("rat"))
                {
                    string[] rat = Expresion.Split('=');
                    Expresion = rat.Last() + "     [WIP]";
                }
                ResultList.Items.Add("Wejście: " + phrase.AddToNumberDot(ExpressionField.Text) + "\nWyjście: " + Expresion);
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
                    if ((LineTypeChart.Items[LineTypeChart.SelectedIndex] as ComboBoxItem).Content.ToString() == "Kartezjański")
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
                    ComplexTab.Height = 66 + wartosc * 39;
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
            switch (datatolinechartview.TypeChart)
            {
                case "line":
                    ChartLineView.ButtonZoomIn(expPlotter, ZoomInSeries);
                    break;
                case "point":
                    ViewPlot.UpdateModelZoomIn(Plot, ZoomInSeries);                                                                                                                                                                                                                             //przekazanie do modelu wartości funkcji X #M                             
                    break;
            }
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            switch (datatolinechartview.TypeChart)
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
            ExpressionField.Text = partExpression[0].Remove(0, 9);
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

        private void ComplexNumber_Checked(object sender, RoutedEventArgs e)
        {
            ComplexTab.Visibility = System.Windows.Visibility.Visible;
        }

        private void RealNumber_Checked(object sender, RoutedEventArgs e)
        {

            ComplexTab.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
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
            ZoomOutSeries.Text = "x,y";
            ZoomInSeries.Text = "x,y";
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
                    PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction, TextBoxText = item });
                    datatopointchartview.CountFunction = datatopointchartview.CountFunction + 1;
                }
                if (PointChartListFunction.Items.Count == 0) PointChartListFunction.Items.Add(new ChartListViewPoint { LabelChartPointValue = datatopointchartview.CountFunction.ToString(), Index = datatopointchartview.CountFunction });
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
    }
}
