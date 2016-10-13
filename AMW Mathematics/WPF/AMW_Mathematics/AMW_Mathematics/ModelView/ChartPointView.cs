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
        public PlotModel PlotModel                                          //użycie akceleratorów get; set; w celu uzyskania dostępu do prywatnego pola plotMode. Wlłaściwość PlotModel jest publiczna dzieki temu możeby bindować w XAML prawatną zmienną plotMode                             // 
        {                                                                   //czyli możemy wyświetlić wykres z jego wszystkimi parametrami ustawianymi w następnych krokach #M
            get { return plotModel; }
            set { plotModel = value; OnPropertyChanged("PlotModel"); }
        }
        public ChartPointView(List<DataToPointChartView> ReturFunctionValueToChart)        //konstruktor klasy ViewPlot wykona się podczas stworzenia nowego obiektu kalsy ViewPlot #M
        {
            PlotModel = new PlotModel();                                    //Stworznie nowego obiektu kalsy PlotModel #M
            SetUpModel();                                                   //metoda odpowiada za ustawienie podstawoych parametrów wykresu #M
            LoadData(ReturFunctionValueToChart);
        }
        private readonly List<OxyColor> colors = new List<OxyColor>         //Lista kolorów będąca wykorzystystana do wyświetlenia przebiegu różnych funkcji na jednym wykresie #M
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
        private void SetUpModel()                                           //ustawienie parametrów wykresu takich jak: (legenda ramka, tło, oś x, oś y ), dla obiektu klasy PlotModel #M
        {
            var dateAxis = new LinearAxis() { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, IntervalLength = 80};
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis() { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot };
            PlotModel.Axes.Add(valueAxis);
        }
        private void LoadData(List<DataToPointChartView> DataToChart)                                    //Metoda odpowiedzialna za: załadowanie danych do wykresu, ustawienie koloru wykresu jego markerów i legendy #M
        {
            var dataPerSeries = DataToChart.GroupBy(m => m.functionId).ToList();                  //grupowanie listy DataToChart po Seri #M
            foreach (var data in dataPerSeries)                                                 //pętla dodająca dane do seri. Podzielenie danych na dwie serie w zależności od osi X dzięki temu unikniemy wystąpienia lini gdy funkcja nie ma miejsca zerowego.   #M               
            {
                var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };           // (Funkcja powinna dodatkow mieć możliwość rozgraniczenia po Y) #M
                data.ToList().ForEach(d => scatterSeries.Points.Add(new ScatterPoint(d.dataX,d.dataY, 5, 200)));
                PlotModel.Series.Add(scatterSeries);                                                 //Dodanie do modelu wykresu nowej serii #M
            }
        }
        public void UpdateModelZoomIn(OxyPlot.Wpf.PlotView e, TextBox ZoomInSeries)
        { 
            if (ZoomInSeries.Text == "x")
            {
                if (PlotModel.Axes[0].ActualMinimum <= -1 && PlotModel.Axes[0].ActualMaximum >= 1)
                {
                    PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum + 1;
                    PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum - 1;
                }
            }
            if (ZoomInSeries.Text.ToString() == "y")
            {
                if (PlotModel.Axes[1].ActualMinimum <= -1 && PlotModel.Axes[1].ActualMaximum >= 1)
                {
                    PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum + 1;
                    PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum - 1;
                }
            }
            if (ZoomInSeries.Text.ToString() == "x,y" || ZoomInSeries.Text.ToString() == "y,x")
            {
                if (PlotModel.Axes[1].ActualMinimum <= -1 && PlotModel.Axes[1].ActualMaximum >= 1)
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
            e.InvalidatePlot(true);
        }
        public void UpdateModelZoomOut(OxyPlot.Wpf.PlotView e, TextBox ZoomOutSeries)              //Metoda odpowiedzialna za aktualizacje danych na wykresie #M
        {
            if (ZoomOutSeries.Text == "x")
            {
                PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;
                PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
            }
            if (ZoomOutSeries.Text.ToString() == "y")
            {
                PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;
                PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
            }
            if (ZoomOutSeries.Text.ToString() == "x,y" || ZoomOutSeries.Text.ToString() == "y,x")
            {
                PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;
                PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
                PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;
                PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
            }
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveUPChart(OxyPlot.Wpf.PlotView e)
        {
            PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum + 1;
            PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum + 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveDownChart(OxyPlot.Wpf.PlotView e)
        {
            PlotModel.Axes[1].Minimum = PlotModel.Axes[1].ActualMinimum - 1;
            PlotModel.Axes[1].Maximum = PlotModel.Axes[1].ActualMaximum - 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveLeftChart(OxyPlot.Wpf.PlotView e)
        {
            PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum - 1;
            PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum - 1;
            e.ResetAllAxes();
            e.InvalidatePlot(true);
        }
        public void MoveRightChart(OxyPlot.Wpf.PlotView e)
        {
            PlotModel.Axes[0].Minimum = PlotModel.Axes[0].ActualMinimum + 1;
            PlotModel.Axes[0].Maximum = PlotModel.Axes[0].ActualMaximum + 1;
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