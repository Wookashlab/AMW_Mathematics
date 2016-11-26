using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionPlotterControl;
using System.Windows.Controls;

namespace AMW_Mathematics.ModelView
{
    class ChartLineView
    {
        List<System.Drawing.Color> color = new List<System.Drawing.Color>                                   //Lista kolorów dla posczególnych serii #M
        {
            System.Drawing.Color.Green,
            System.Drawing.Color.Blue,
            System.Drawing.Color.Red,
            System.Drawing.Color.Yellow,
            System.Drawing.Color.Violet,
            System.Drawing.Color.Gray,
        };
        public void SetPenWidth(ExpressionPlotter e, int width)
        {
            e.PenWidth = width;
        }
        public void SetRange(ExpressionPlotter e, double startX, double endX, double startY, double endY)   //Metoda odpowiadająca za ustawienie zakresu osi x i y #M
        {
            e.SetRangeX(startX, endX);
            e.SetRangeY(startY, endY);
        }
        public void SetDivisions(ExpressionPlotter e, int divX, int divY)                                   //metoda opowiadająca za ustawienie przedziału #M
        {
            e.DivisionsX = divX;
            e.DivisionsY = divY;
        }
        public void SetMode(ExpressionPlotter e, GraphMode mode, int sensitivity)                           //Metoda odpowadająca za ustawienie parametrów wykresu polarnego #M
        {
            e.GraphMode = mode;
            if (mode == GraphMode.Polar)
            {
                e.PolarSensitivity = sensitivity;
            }
        }
        public void DrawChartLine(ExpressionPlotter e, double startx, double endx, double starty, double endy, List<string> FunctionList, bool ToogleGridS, bool Type) //metoda odpowiedzielna za generowanie wykresu #M
        {
            SetRange(e, startx, endx, starty, endy);                                                                                                                    //ustawienie zakresu generowania wykresu #M
            SetDivisions(e, (int)5, (int)5);                                                                                                                            //ustalenie podziału #M
            SetPenWidth(e, (int)2);                                                                                                                                     //ustalenie grubości lini #M
            if (Type == true)                                                                                                                                           //ustalenie typu wykresu #M
            {
                e.GraphMode = GraphMode.Polar;
            }
            else
            {
                SetMode(e, GraphMode.Rectangular, 50);
            }
            e.DisplayText = false;                                                                                                                                      //ustawienie wyświetlania legendy #M
            if (ToogleGridS == true) e.ToggleGrids();
            e.RemoveAllExpressions();                                                                                                                                   //zresetowanie wykresu poprzez usunięcie poprzednich wyrażeń #M
            for (int i = 0; i < FunctionList.Count; i++)                                                                                                                //pętla odpowiadająca za rysowanie wszystkich wyrażeń znajdujących się w liście #M
            {
                e.AddExpression((IEvaluatable)new ExpressionPlotterControl.Expression(FunctionList[i]), color[i], true);                                                //rysowanie wyrażenia #M
            }
            e.Refresh();                                                                                                                                                //odświerzenie wykresu #M
        }
        public void ButtonZoomIn(ExpressionPlotter expPlotter, CheckBox ZoomAfterX, CheckBox ZoomAfterY)
        {
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == false)
            {
                expPlotter.ZoomInX();
            }
            if (ZoomAfterX.IsChecked == false && ZoomAfterY.IsChecked == true)
            {
                expPlotter.ZoomInY();
            }
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == true)
            {
                expPlotter.ZoomIn();
            }
            expPlotter.Refresh();
        }
        public void ButtonZoomOut(ExpressionPlotter expPlotter, CheckBox ZoomAfterX, CheckBox ZoomAfterY)
        {
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == false)
            {
                expPlotter.ZoomOutX();
            }
            if (ZoomAfterX.IsChecked == false && ZoomAfterY.IsChecked == true)
            {
                expPlotter.ZoomOutY();
            }
            if (ZoomAfterX.IsChecked == true && ZoomAfterY.IsChecked == true)
            {
                expPlotter.ZoomOut();
            }
            expPlotter.Refresh();
        }
        public void MoveUPChart(ExpressionPlotter expPlotter)
        {
            expPlotter.MoveUp(1);
            expPlotter.Refresh();
        }
        public void MoveDownChart(ExpressionPlotter expPlotter)
        {
            expPlotter.MoveDown(1);
            expPlotter.Refresh();
        }
        public void MoveLeftChart(ExpressionPlotter expPlotter)
        {
            expPlotter.MoveLeft(1);
            expPlotter.Refresh();
        }
        public void MoveRightChart(ExpressionPlotter expPlotter)
        {
            expPlotter.MoveRight(1);
            expPlotter.Refresh();
        }
        public double GetR(double X, double Y)      //wydaje mi się że niepotrzebne sprawdzić #M
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public double GetTheta(double X, double Y)  //Wydaje mi się że nie potrzebne sprawdzieć #M
        {
            double dTheta;
            if (X == 0)
            {
                if (Y > 0)
                    dTheta = Math.PI / 2;
                else
                    dTheta = -Math.PI / 2;
            }
            else
                dTheta = Math.Atan(Y / X);
            if (X < 0)
                dTheta = dTheta + Math.PI;
            else if (Y < 0)
                dTheta = dTheta + 2 * Math.PI;
            return dTheta;
        }

    }
}
