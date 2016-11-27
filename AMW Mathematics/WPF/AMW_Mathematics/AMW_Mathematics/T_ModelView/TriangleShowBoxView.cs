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
        public void ShowSolverBox_Window(int index, ListBox ShowSolverBox,ComboBox TypeSolving, TextBox AngleA, TextBox AngleB, TextBox AngleC, TextBox TValuea, TextBox TValueb, TextBox TValuec)  //metoda odpowiedzialna za wyświetlenie zakładki która zawiera informacjie o trójącie #M
        {
            try
            {
                switch (index)                                                                                                                                                                      //pobranie indeksu klikniętej zakładki #M
                {
                    case 0:
                        ShowSolverBox.Items.Clear();                                                                                                                                                //oczyszczenie kontenera w którym będą znajdować się zakładki #M
                        if((TypeSolving.Items[TypeSolving.SelectedIndex] as ComboBoxItem).Content.ToString() == "Sides")                                                                            //sprawdzenie na podstawie czego został wygenerowany trójkąt #M
                        {                                                                                                       
                            ShowSolverBox.Items.Add("A: Law of Cosines: cos(α)=(c²+b²-a²)÷(2·b·a)");                                                                                                //wyświetlenie poszczególnych parametrów trójkąta w zakładce #M
                            ShowSolverBox.Items.Add("B: Law of Cosines: cos(β)=(c²-b²+a²)÷(2·b·a)");
                            ShowSolverBox.Items.Add("C: Law of Cosines: cos(γ)=(b²+a²-c²)÷(2·b·a)");
                            ShowSolverBox.Items.Add("D: Sum of triangle's angles measures α+β+γ=1");
                        }
                        if ((TypeSolving.Items[TypeSolving.SelectedIndex] as ComboBoxItem).Content.ToString() == "Angles")                                                                          //sprawdzenie na podstawie czego został wygenerowany trójkąt #M
                        {
                            ShowSolverBox.Items.Add("A: Sum of triangle's angles measure: α+β+γ=1");                                                                                                //wyświetlenie poszczególnych parametrów trójkąta w zakładce #M
                        }                      
                        break;
                    case 1:
                        ShowSolverBox.Items.Clear();                                                                                                                                                //oczyszczenie kontenera w którym będą znajdować się zakładki #M
                        if (AngleA.Text == AngleB.Text && AngleA.Text == AngleC.Text && AngleB.Text == AngleC.Text)                 
                            ShowSolverBox.Items.Add("Equailaterial triangle (all angiels congruent, all sides congruent)");                                                                         //dodanie informacji o danym trójącie #M
                        if (AngleA.Text == AngleB.Text || AngleB.Text == AngleC.Text || AngleA.Text == AngleC.Text)
                            ShowSolverBox.Items.Add("Isosceles triangle (two sides of equal lenght)");                                                                                              //dodanie informacji o danym trójącie #M
                        if (AngleA.Text == "90" || AngleB.Text == "90" || AngleC.Text == "90")
                            ShowSolverBox.Items.Add("Right triangle (one 90 degree angle)");                                                                                                        //dodanie informacji o danym trójącie #M
                        if (AngleA.Text != AngleB.Text && AngleA.Text != AngleC.Text && AngleB.Text != AngleC.Text)
                            ShowSolverBox.Items.Add("Scalene triangle (all three sides of diffrent lenght)");                                                                                       //dodanie informacji o danym trójącie #M
                        if (double.Parse(AngleA.Text) < 90 && double.Parse(AngleB.Text) < 90 && double.Parse(AngleC.Text) < 90)
                            ShowSolverBox.Items.Add("Acute Triangle (all three angles less than 90 degrees)");                                                                                      //dodanie informacji o danym trójącie #M
                        if (double.Parse(AngleA.Text) > 90 || double.Parse(AngleB.Text) > 90 || double.Parse(AngleC.Text) > 90)
                            ShowSolverBox.Items.Add("Obtuse triangle (one angle greater than 90 degrees)");                                                                                         //dodanie informacji o danym trójącie #M
                        break;
                    case 2:
                        ShowSolverBox.Items.Clear();                                                        
                        double p = 0.5 * (double.Parse(TValuea.Text) + double.Parse(TValueb.Text) + double.Parse(TValuec.Text));
                        double P = Math.Sqrt(p * (p - double.Parse(TValuea.Text)) * (p - double.Parse(TValueb.Text)) * (p - double.Parse(TValuec.Text)));                                           //obliczenie pola trójkąta #M
                        double w1 = (P / double.Parse(TValuea.Text)) * 2;                                                                                                                           //obliczenie wysokości #M
                        double w2 = (P / double.Parse(TValueb.Text)) * 2;
                        double w3 = (P / double.Parse(TValuec.Text)) * 2;
                        ShowSolverBox.Items.Add("Area: " + P.ToString());                                                                                                                           //dodanie do zakładki pola trójkąta #M
                        ShowSolverBox.Items.Add("Altitude A: " + w1.ToString());                                                                                                                    //dodanie do zkaładki wysokości #M
                        ShowSolverBox.Items.Add("Altitude B: " + w2.ToString());                                                                                                                    //dodanie do zkaładki wysokości #M
                        ShowSolverBox.Items.Add("Altitude C: " + w3.ToString());                                                                                                                    //dodanie do zkaładki wysokości #M
                        break;
                }
            }
            catch { };
        }
    }
}
