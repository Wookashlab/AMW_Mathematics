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
        public List<DataToPointChartView> DataListFunction(List<DataToPointChartView> DataToChartPoint, List<string> ListFunctionPoint)
        {
            int FunctionId = 0;
            foreach (var function in ListFunctionPoint)
            {
                var f = function.Replace("{", "");
                f = f.Replace("}", "");
                f = f.Insert(f.Length, ",");
                if(f.Length > 1) GetData(DataToChartPoint, f, FunctionId);
                FunctionId++;
            }

            return DataToChartPoint;
        }
        private List<DataToPointChartView> GetData(List<DataToPointChartView> DataToChartPoint, String function, int FunctionId)
        {
            var f = function;
            int index = f.IndexOf(",");
            double x = double.Parse(f.Substring(0, index));
            f = f.Remove(0, index + 1);
            index = f.IndexOf(",");
            double y = 0;
            try
            {
                y = double.Parse(f.Substring(0, index).Replace(".", ","));
            }
            catch
            {

                return new List<DataToPointChartView>();
            }
            f = f.Remove(0, index + 1);
            DataToChartPoint.Add(new DataToPointChartView { functionId = FunctionId, dataX = x, dataY = y });
            if (f.Length == 0) return DataToChartPoint;
            else return GetData(DataToChartPoint, f, FunctionId);
        }
        public List<TextBox> FindBox(ListView PointChartListFunction, string name, string namex, string namey, List<TextBox> ListTextBox, string function)
        {
            switch (function)
            {
                case "First":
                    var _ListBox = PointChartListFunction as ListBox;
                    foreach (var _ListBoxItem in _ListBox.Items)
                    {
                        var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = functiontoplot.AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = name;
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                        ListTextBox.Add(_Control);                                                                                         //dodanie do listy funkcji występującej w TextBox #M
                    }
                    return ListTextBox;
                case "Second":
                    var _ListBoxs = PointChartListFunction as ListBox;
                    foreach (var _ListBoxItems in _ListBoxs.Items)
                    {
                        var _Containers = _ListBoxs.ItemContainerGenerator.ContainerFromItem(_ListBoxItems);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                        var _Children = functiontoplot.AllChildren(_Containers);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                        var _Name = namex;
                        var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                        _Control.Text = "";
                        ListTextBox.Add(_Control);
                        _Name = namey;
                        _Control = (TextBox)_Children.First(c => c.Name == _Name);
                        _Control.Text = "";//dodanie do listy funkcji występującej w TextBox #M
                        ListTextBox.Add(_Control);
                    }
                    return ListTextBox;
            }
            return new List<TextBox>();
        }

        public string FindFunctionInBox(ListView ListChartPointW, string function)
        {
            var _ListBox = ListChartPointW as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                           //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = functiontoplot.AllChildren(_Container);                                                                                    //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = "XValue";
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);
                if (_Control.Text != "") function += "{" + _Control.Text + ",";
                _Name = "YValue";
                _Control = (TextBox)_Children.First(c => c.Name == _Name);
                if (_Control.Text != "") function += _Control.Text + "}" + ",";                                //dodanie do listy funkcji występującej w TextBox #M
            }
            int index = function.LastIndexOf(",");
            function = function.Remove(index, 1);
            function = function + "}";
            return function;
        }
    }
}
