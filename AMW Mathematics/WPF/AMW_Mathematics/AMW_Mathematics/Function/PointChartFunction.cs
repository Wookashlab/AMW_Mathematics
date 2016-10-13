using AMW_Mathematics.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.Function
{
    class PointChartFunction
    {
        public List<DataToPointChartView> DataListFunction(List<DataToPointChartView> DataToChartPoint, List<string> ListFunctionPoint)
        {
            int FunctionId = 0;
            foreach (var function in ListFunctionPoint)
            {
                var f = function.Replace("{", "");
                f = f.Replace("}", "");
                f = f.Insert(f.Length, ",");
                GetData(DataToChartPoint, f, FunctionId);
                FunctionId++;
            }

            return DataToChartPoint;
        }
        public List<DataToPointChartView> GetData(List<DataToPointChartView> DataToChartPoint, String function, int FunctionId)
        {
            var f = function;
            int index = f.IndexOf(",");
            double x = double.Parse(f.Substring(0, index));
            f = f.Remove(0, index + 1);
            index = f.IndexOf(",");
            double y = double.Parse(f.Substring(0, index).Replace(".", ","));
            f = f.Remove(0, index + 1);
            DataToChartPoint.Add(new DataToPointChartView { functionId = FunctionId, dataX = x, dataY = y });
            if (f.Length == 0) return DataToChartPoint;
            else return GetData(DataToChartPoint, f, FunctionId);
        }
    }
}
