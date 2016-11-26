using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Windows.Controls;

namespace AMW_Mathematics.ModelView
{
    public class ChartPointView : INotifyPropertyChanged
    {
        private PlotModel plotModel;
        public PlotModel PlotModel                                                          //użycie akceleratorów get; set; w celu uzyskania dostępu do prywatnego pola plotMode. Wlłaściwość PlotModel jest publiczna dzieki temu możeby bindować w XAML prawatną zmienną plotMode                             // 
        {                                                                                   //czyli możemy wyświetlić wykres z jego wszystkimi parametrami ustawianymi w następnych krokach #M
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ChartPointView(List<DataToPointChartView> ReturFunctionValueToChart)        //konstruktor klasy ViewPlot wykona się podczas stworzenia nowego obiektu kalsy ViewPlot #M
        {
            PlotModel = new PlotModel();                                                    //Stworznie nowego obiektu kalsy PlotModel #M
            SetUpModel();                                                                   //metoda odpowiada za ustawienie podstawoych parametrów wykresu #M
            LoadData(ReturFunctionValueToChart);
        }
        private readonly List<OxyColor> colors = new List<OxyColor>                         //Lista kolorów będąca wykorzystystana do wyświetlenia przebiegu różnych funkcji na jednym wykresie #M
                                            {
                                                OxyColors.Green,
                                                OxyColors.IndianRed,
                                                OxyColors.Coral,
                                                OxyColors.Chartreuse,
                                                OxyColors.Azure,
                                                OxyColors.Magenta,
                                                OxyColors.Violet,
                                                OxyColors.Yellow
                                            };
        private void SetUpModel()                                                                                                                                               //ustawienie parametrów wykresu takich jak: (legenda ramka, tło, oś x, oś y ), dla obiektu klasy PlotModel #M
        {
            var dateAxis = new LinearAxis() { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80};
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            PlotModel.Axes.Add(valueAxis);
        }
        private void LoadData(List<DataToPointChartView> DataToChart)                                                                                                           //Metoda odpowiedzialna za: załadowanie danych do wykresu, ustawienie koloru wykresu jego markerów i legendy #M
        {
            var dataPerSeries = DataToChart.GroupBy(m => m.functionId).ToList();                                                                                                //grupowanie listy DataToChart po Id wyrażenia #M
            foreach (var data in dataPerSeries)                                                                                                                                 //pętla dodająca dane do seri #M               
            {
                var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };                                                                                       //tworzenie serii #M
                data.ToList().ForEach(d => scatterSeries.Points.Add(new ScatterPoint(d.dataX,d.dataY, 5, 200)));                                                                //dodawanie punktów do serii #Ms
                PlotModel.Series.Add(scatterSeries);                                                                                                                            //Dodanie do modelu wykresu nowej serii #M
            }
        }
        public void UpdateModelZoomIn(OxyPlot.Wpf.PlotView e, CheckBox ZoomAfterX, CheckBox ZoomAfterY)     //metoda odpowiedzialna za zwiększanie wykresu #M
        {
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == false)                              //sprawdzenie po jakiej zmiennej ma być zwiększany wykres #M
            {
                if (PlotModel.Axes[0].ActualMinimum <= -1 && PlotModel.Axes[0].ActualMaximum >= 1)
                {
                    PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum + 1;                        //zwiększanie po x #M
                    PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum - 1;
                }
            }
            if (ZoomAfterX.IsChecked == false && ZoomAfterY.IsChecked == true)
            {
                if (PlotModel.Axes[1].ActualMinimum <= -1 && PlotModel.Axes[1].ActualMaximum >= 1)
                {
                    PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum + 1;                        //zwiększanie po y #M
                    PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum - 1;
                }
            }
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == true)
            {
                if (PlotModel.Axes[1].ActualMinimum <= -1 && PlotModel.Axes[1].ActualMaximum >= 1)          //zwiększanie po x i y #M
                {
                    PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum + 1;
                    PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum - 1;
                }
                if (PlotModel.Axes[0].ActualMinimum <= -1 && PlotModel.Axes[1].ActualMaximum >= 1)
                {
                    PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum + 1;
                    PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum - 1;
                }
            }
            e.ResetAllAxes();
            e.InvalidatePlot(true);                                                                         //odświeżenie wykresu #M
        }
        public void UpdateModelZoomOut(OxyPlot.Wpf.PlotView e, CheckBox ZoomAfterX, CheckBox ZoomAfterY)             //metoda odpowiedzialna za zmniejszanie wykresu #M
        {
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == false)                                       //sprawdzenie po jakiej zmiennej ma być zmniejszany wykres #M
            {
                PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;                                    //zwiększanie po x #M
                PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
            }
            if (ZoomAfterX.IsChecked == false && ZoomAfterY.IsChecked == true)
            {
                PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;                                    //zwiększanie po y #M
                PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
            }
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == true)
            {
                PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;                                    //zwiększanie po x y #M
                PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
                PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;
                PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
            }
            e.ResetAllAxes();
            e.InvalidatePlot(true);                                                                                  //odświeżenie wykresu #M
        }
        public void MoveUPChart(OxyPlot.Wpf.PlotView e) //przesunięcie wykresu w góre #M
        {
            PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum + 1;
            PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveDownChart(OxyPlot.Wpf.PlotView e)    //przesunięcie wykresu w dół #M
        {
            PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;
            PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum - 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveLeftChart(OxyPlot.Wpf.PlotView e)    //przesunięcie wykresu w lewo #M
        {
            PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;
            PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum - 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveRightChart(OxyPlot.Wpf.PlotView e)   //przesunięcie wykresu w prawko #M
        {
            PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum + 1;
            PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void SetXAndY(OxyPlot.Wpf.PlotView e ,double startx, double endx,double starty,double endy)  //ustawienie skali wykresu #M
        {
            PlotModel.Axes[1].Minimum = startx;
            PlotModel.Axes[1].Maximum = endx;
            PlotModel.Axes[0].Minimum = starty;
            PlotModel.Axes[0].Maximum = endy;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName) //metoda odpowiedzialna jest za wykrycie zdarzenia aktualizacji wykresu #M
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}