using AMW_Mathematics.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AMW_Mathematics.Function
{
    class PointChartFunction
    {
        private FunctionToAllPlot functiontoplot = new FunctionToAllPlot();
        public List<DataToPointChartView> DataListFunction(List<DataToPointChartView> DataToChartPoint, List<string> ListFunctionPoint) //Metoda odpowadająca za przerobienie wprowadzonych przez użytkownika punktów i wprowadzania ich do listy #M
        {
            int FunctionId = 0;
            foreach (var function in ListFunctionPoint)                                                                                 //foreach usuwający w wszystkich funkcjach niepotrzebne znaki takie jak { } #M
            {
                var f = function.Replace("{", "");
                f = f.Replace("}", "");
                f = f.Insert(f.Length, ",");
                if(f.Length > 1) GetData(DataToChartPoint, f, FunctionId);                                                              //metoda zwracająca listę punktów danej funkcji #M
                FunctionId++;
            }

            return DataToChartPoint;                                                                                                    //zwrócenie listy punktów wszystkich funkcji #M 
        }
        private List<DataToPointChartView> GetData(List<DataToPointChartView> DataToChartPoint, String function, int FunctionId)        //metoda rekurencyjna zwracająca listę punktów w danym wyrażeniu #M
        {
            var f = function;
            int index = f.IndexOf(",");
            double x = 0;
            try
            {
               x  = double.Parse(f.Substring(0, index));                                                                            //zabezpieczenie na wypadek gdy x nie będzie liczbą użytkownik źle wprowadzi dane #M
            }                                                                                                                       //pobranie pierwszej liczby jako x #M
            catch
            {
                return new List<DataToPointChartView>();
            }         
            f = f.Remove(0, index + 1);                                                                                             //usunięcie jej z ciągu znaków #M
            index = f.IndexOf(",");
            double y = 0;
            try
            {                                                                                                                       //obsługa zdarzenia na wypadek gdyby y nie był liczbą #M
                y = double.Parse(f.Substring(0, index).Replace(".", ","));                                                          //pobranie pierwszej liczby w ciagu jako y #M
            }
            catch
            {
                return new List<DataToPointChartView>();
            }
            f = f.Remove(0, index + 1);
            DataToChartPoint.Add(new DataToPointChartView { functionId = FunctionId, dataX = x, dataY = y });                       //dodanie do listy x i y oraz id funkcji z której pochodzi
            if (f.Length == 0) return DataToChartPoint;     
            else return GetData(DataToChartPoint, f, FunctionId);                                                                   //rekurencyjne wywołanie funkcji aż do momentu gdy ciąg znaków f będzie pusty #M
        }
        public List<TextBox> FindBox(ListView PointChartListFunction, string name, string namex, string namey, List<TextBox> ListTextBox, string function)
        {
            switch (function)                                                                                                                       //decyduje który rodzaj szukania TextBoxów ma wykonać #<
            {
                case "First":
                    var _ListBox = PointChartListFunction as ListBox;
                    foreach (var _ListBoxItem in _ListBox.Items)
                    {
                        var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = functiontoplot.AllChildren(_Container);                                                                     //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = name;
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                        ListTextBox.Add(_Control);                                                                                                  //dodanie do listy TextBoxów występujących w ListView #M
                    }
                    return ListTextBox;
                case "Second":
                    var _ListBoxs = PointChartListFunction as ListBox;
                    foreach (var _ListBoxItems in _ListBoxs.Items)
                    {
                        var _Containers = _ListBoxs.ItemContainerGenerator.ContainerFromItem(_ListBoxItems);                                        //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = functiontoplot.AllChildren(_Containers);                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = namex;
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                              //znalezienie pierwszego TextBoxa który ma x w nazwie #M
                        _Control.Text = "";                                                                                                         //oczyszczenie TextBoxa #M
                        ListTextBox.Add(_Control);                                                                                                  //dodanie znalezionego TextBoxa do listy #M
                        _Name = namey;
                        _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                                  //znalezienie drugiego Textboxa który ma y w nazwie #M
                        _Control.Text = "";                                                                                                         //Oczyszczenie TextBoxa #M
                        ListTextBox.Add(_Control);                                                                                                  //dodanie znalezionego TextBoxa do listy #M
                    }
                    return ListTextBox;
            }
            return new List<TextBox>();
        }

        public string FindFunctionInBox(ListView ListChartPointW, string function)                                                              //metoda odpowiadająca za odczyt zawartości TexTboxów znadujących się w liście a następnie przedstawienie ich zawartości w formie stringa #M
        {
            try                                                                                                                                 //obsłga błedu na wypadek złego przeczytania zawartości TextBoxa #M
            {
                var _ListBox = ListChartPointW as ListBox;
                foreach (var _ListBoxItem in _ListBox.Items)
                {
                    var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                    var _Children = functiontoplot.AllChildren(_Container);                                                                     //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                    var _Name = "XValue";
                    var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                    if (_Control.Text != "") function += "{" + _Control.Text + ",";                                                             //wprowadzenie do zmiennej function zawartości TextBoxa znajdującego się w liście jako parametru x #M
                    _Name = "YValue";
                    _Control = (TextBox)_Children.First(c => c.Name == _Name);
                    if (_Control.Text != "") function += _Control.Text + "}" + ",";                                                             //wprowadzenie do zmiennej function zawartości TextBoxa znajdującego się w liście jako parametru y #M
                }
                int index = function.LastIndexOf(",");                                                                                          //znalezienie a następnie usunięcie kropi znadującej sięna końcu stringa #M
                function = function.Remove(index, 1);
                function = function + "}";                                                                                                      //dodanie do stringa nawiasu #M
                return function;                                                                                                                //zwrócenie stringa #M
            }
            catch
            {
                return function;                                                                                                                //zwrócenie stringa #M
            }
        }
    }
}
