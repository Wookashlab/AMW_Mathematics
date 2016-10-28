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
using System.Windows.Shapes;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AMW_Mathematics.T_ModelView
{
    class TriangleShowBoxView
    {
        public void ShowSolverBox_Window(int index, ListBox ShowSolverBox, TextBox AngleA, TextBox AngleB, TextBox AngleC, TextBox TValuea, TextBox TValueb, TextBox TValuec)
        {
            try
            {
                switch (index)
                {
                    case 0:
                        ShowSolverBox.Items.Clear();
                        ShowSolverBox.Items.Add("A: Law of Cosines: cos(α)=(c²+b²-a²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("B: Law of Cosines: cos(β)=(c²-b²+a²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("C: Law of Cosines: cos(γ)=(b²+a²-c²)÷(2·b·a)");
                        ShowSolverBox.Items.Add("D: Sum of triangle's angles measures A+B+C=1");
                        break;
                    case 1:
                        ShowSolverBox.Items.Clear();
                        if (AngleA.Text == AngleB.Text && AngleA.Text == AngleC.Text && AngleB.Text == AngleC.Text)
                            ShowSolverBox.Items.Add("Equailaterial triangle (all angiels congruent, all sides congruent)");
                        if (AngleA.Text == AngleB.Text || AngleB.Text == AngleC.Text || AngleA.Text == AngleC.Text)
                            ShowSolverBox.Items.Add("Isosceles triangle (two sides of equal lenght)");
                        if (AngleA.Text == "90" || AngleB.Text == "90" || AngleC.Text == "90")
                            ShowSolverBox.Items.Add("Right triangle (one 90 degree angle)");
                        if (AngleA.Text != AngleB.Text && AngleA.Text != AngleC.Text && AngleB.Text != AngleC.Text)
                            ShowSolverBox.Items.Add("Scalene triangle (all three sides of diffrent lenght)");
                        if (double.Parse(AngleA.Text) < 90 && double.Parse(AngleB.Text) < 90 && double.Parse(AngleC.Text) < 90)
                            ShowSolverBox.Items.Add("Acute Triangle (all three angles less than 90 degrees)");
                        if (double.Parse(AngleA.Text) > 90 || double.Parse(AngleB.Text) > 90 || double.Parse(AngleC.Text) > 90)
                            ShowSolverBox.Items.Add("Obtuse triangle (one angle greater than 90 degrees)");
                        break;
                    case 2:
                        ShowSolverBox.Items.Clear();
                        double p = 0.5 * (double.Parse(TValuea.Text) + double.Parse(TValueb.Text) + double.Parse(TValuec.Text));
                        double P = Math.Sqrt(p * (p - double.Parse(TValuea.Text)) * (p - double.Parse(TValueb.Text)) * (p - double.Parse(TValuec.Text)));
                        double w1 = (P / double.Parse(TValuea.Text)) * 2;
                        double w2 = (P / double.Parse(TValueb.Text)) * 2;
                        double w3 = (P / double.Parse(TValuec.Text)) * 2;
                        ShowSolverBox.Items.Add("Area: " + P.ToString());
                        ShowSolverBox.Items.Add("Altitude A: " + w1.ToString());
                        ShowSolverBox.Items.Add("Altitude B: " + w2.ToString());
                        ShowSolverBox.Items.Add("Altitude C: " + w3.ToString());
                        break;
                }
            }
            catch { };
        }
    }
}
