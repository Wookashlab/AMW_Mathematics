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
        List<System.Drawing.Color> color = new List<System.Drawing.Color>
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
        public void SetRange(ExpressionPlotter e, double startX, double endX, double startY, double endY)
        {
            e.SetRangeX(startX, endX);
            e.SetRangeY(startY, endY);
        }
        public void SetDivisions(ExpressionPlotter e, int divX, int divY)
        {
            e.DivisionsX = divX;
            e.DivisionsY = divY;
        }
        public void SetMode(ExpressionPlotter e, GraphMode mode, int sensitivity)
        {
            e.GraphMode = mode;
            if (mode == GraphMode.Polar)
            {
                e.PolarSensitivity = sensitivity;
            }
        }
        public void DrawChartLine(ExpressionPlotter e, double startx, double endx, double starty, double endy, List<string> FunctionList, bool ToogleGridS, bool Type)
        {
            SetRange(e, startx, endx, starty, endy);
            SetDivisions(e, (int)5, (int)5);
            SetPenWidth(e, (int)2);
            if (Type == true)
            {
                e.GraphMode = GraphMode.Polar;
            }
            else
            {
                SetMode(e, GraphMode.Rectangular, 50);
            }
            e.DisplayText = false;
            if (ToogleGridS == true) e.ToggleGrids();
            e.RemoveAllExpressions();
            for (int i = 0; i < FunctionList.Count; i++)
            {
                e.AddExpression((IEvaluatable)new ExpressionPlotterControl.Expression(FunctionList[i]), color[i], true);
            }
            e.Refresh();
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
        public double GetR(double X, double Y)
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public double GetTheta(double X, double Y)
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

            //actual range of theta is from 0 to 2PI
            if (X < 0)
                dTheta = dTheta + Math.PI;
            else if (Y < 0)
                dTheta = dTheta + 2 * Math.PI;
            return dTheta;
        }

    }
}
