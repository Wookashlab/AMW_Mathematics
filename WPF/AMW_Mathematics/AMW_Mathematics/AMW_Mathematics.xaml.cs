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
namespace AMW_Mathematics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Dictionary<string, string> SymbolsAndValues;
        public MainWindow()
        {
            InitializeComponent();
            SymbolsAndValues = new Dictionary<string, string>(); 
        }
        private void ConfirmExpresion_Click(object sender, RoutedEventArgs e)
        {
            string Expresion = ExpressionField.Text;
            Expresion = SaveValuesOfVariables(Expresion);
            Expresion = CheckVariablesinExpresion(Expresion);
            if(Expresion.Contains(":=")==false) Expresion = AddToNumberDot(Expresion);
            Expresion = Maxima.Eval(Expresion);
            Expresion = Expresion.Replace(":=", " = ");
            ResultList.Items.Add(Expresion);
            ExpressionField.Clear();
        }
        private string SaveValuesOfVariables(string Expresion)
        {
            if (Expresion.Contains(":=") == true) //sprawdzenie wyrażenie ma stworzyć zmienna
            {
                Expresion = AddToNumberDot(ExpressionField.Text.Substring(Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":")) + 2));
                Expresion = Maxima.Eval(Expresion);
                SymbolsAndValues.Add(ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))), Expresion);
                return ExpressionField.Text.Substring(0, Math.Min(ExpressionField.Text.Length, ExpressionField.Text.IndexOf(":"))) + ":=" + Expresion;
            }
            else return Expresion;
        }
        private string AddToNumberDot(string Expresion)
        {
           string pom = "";
           bool pointexist = false;
           for(int i = 0; i < Expresion.Length; i++)
           {
               if(Char.IsNumber(Expresion[i]) == true)
               {
                   for(int j = i; j < Expresion.Length; j++)
                   {
                        if (Char.IsNumber(Expresion[j]) != true && Expresion[j] != '.')
                        {
                            if (pom.Length == 1)
                            {
                                Expresion = Expresion.Insert(j, ".0");
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".")!= true)
                                {
                                    Expresion = Expresion.Insert(j, ".0");
                                    pom = "";
                                    i = j+2;
                                    break;
                                }
                                else
                                {
                                    pom = "";
                                    i = j;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            pom = pom + Expresion[j];
                        }           
                        if (i + 1 == Expresion.Length)
                        {
                            if (pom.Length == 1)
                            {
                                Expresion = Expresion.Insert(j+1, ".0");
                                i = i + 2;
                                pom = "";
                                break;
                            }
                            else
                            {
                                if (pom.Contains(".") != true)
                                {
                                    Expresion = Expresion.Insert(j+1, ".0");
                                    pom = "";
                                    i = j + 2;
                                    break;
                                }
                                else
                                {
                                    pom = "";
                                    i = j;
                                    break;
                                }
                            }
                        }
                   }
               }
           }
           return Expresion;
        }
        private string CheckVariablesinExpresion(string Expresion)
        {
            if (Expresion.Contains(":=") != true)
            {
                foreach (var symbol in SymbolsAndValues)
                {
                    Expresion = Expresion.Replace(symbol.Key, symbol.Value);
                }
            }
            return Expresion;
        }



        private void Keyboard_Click(object sender, RoutedEventArgs e)
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            ExpressionField.Text = ExpressionField.Text + wartosc;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ExpressionField.Text = "";
        }

        private void kBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ExpressionField.Text.Length > 0)
            {
                ExpressionField.Text = ExpressionField.Text.Substring(0, ExpressionField.Text.Length - 1);

            }
        }

        private void Function_Click(object sender, RoutedEventArgs e)
        {
            var klawisz = (Button)sender;
            string wartosc = klawisz.Content.ToString();
            ExpressionField.Text = ExpressionField.Text + wartosc+"(";
        }
    }
}
