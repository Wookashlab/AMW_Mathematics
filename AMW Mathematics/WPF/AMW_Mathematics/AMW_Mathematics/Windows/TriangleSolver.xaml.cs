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
using AMW_Mathematics.T_ModelView;

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for TriangleSolver.xaml
    /// </summary>

    public partial class TriangleSolver : MetroWindow
    {
        private TriangleView triangleview = new TriangleView();

        private TriangleShowBoxView trangleshowboxview = new TriangleShowBoxView();

        private Polygon Triangle;

        private List<Label> ListLabel;

        private List<Label> ListLabel2;

        private bool drawlabel = false;
        private SolidColorBrush color;
        public TriangleSolver(string colorCode)
        {
            color = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorCode));
            InitializeComponent();
            ColorLabelA.Background = color;
            ColorLabelAlfa.Background = color;
            ColorLabelB.Background = color;
            ColorLabelBeta.Background = color;
            ColorLabelC.Background = color;
            ColorLabelGama.Background = color;

        }
        public Dictionary<string,double> SolveAfterAngles(Dictionary<string, double> LenghtSides, double?  AngleA, double ? AngleB, double ? AngleC, double ? ValueC, double ?  ValueB, double ? ValueA )   //metoda zwraca listę boków wraz z kątaki (boki trójkątów liczone są na podstawie kontów) #M
        {
            bool solve = false;
            if (AngleA != null && AngleB != null && AngleC != null) solve = true;                                                                                                                           //zmienna pilnująca że do obliczia boków zostaną wprowadzone 3 kąty #M
            if (AngleA == null && AngleB != null && AngleC != null)                                                                                                                                         //warunek sprawdzający czy 3 kąt trójkąta został podany #M
            {
                AngleA = Math.Abs(((double)AngleB + (double)AngleC)-180);                                                                                                                                   //obliczenie 3 kąta trójkąta #M
                solve = true;
            }
            if (AngleA != null && AngleB == null && AngleC != null)                                                                                                                                         //warunek sprawdzający czy 3 kąt trójkąta został podany #M
            {
                AngleB = Math.Abs(((double)AngleB + (double)AngleC)-180);                                                                                                                                   //obliczenie 3 kąta trójkąta #M
                solve = true;
            }      
            if (AngleA != null && AngleB != null && AngleC == null)                                                                                                                                         //warunek sprawdzający czy 3 kąt trójkąta został podany #M
            {
                AngleC = Math.Abs(((double)AngleB + (double)AngleA)-180);                                                                                                                                   //obliczenie 3 kąta trójkąta #M
                solve = true;
            }
                
            double Alfa = 0;
            double Beta = 0;
            double Gamma = 0;
            if (solve == true)
            {
                Alfa = ((double)AngleA * Math.PI) / 180;                                                                                                                                                    //zamiana kątów w radianach na stopnie #M
                Beta = ((double)AngleB * Math.PI) / 180;
                Gamma = ((double)AngleC * Math.PI) / 180;
                LenghtSides.Add("AngleA", (double)AngleA);                                                                                                                                                  //dodanie do listy obliczonych kontów #M
                LenghtSides.Add("AngleB", (double)AngleB);                                                                                                                                                  //dodanie do listy obliczonych kontów #M
                LenghtSides.Add("AngleC", (double)AngleC);                                                                                                                                                  //dodanie do listy obliczonych kontów #M
            }
            if(ValueC == null)
            {
                ValueC = 10;                                                                                                                                                                                //domyślne ustawienie długości jednego boku #M
                ValueA = Math.Round((((double)ValueC * Math.Sin(Beta)) / (Math.Sin(Alfa))), 2);                                                                                                             //obliczenie długości boku na podstawie domyślnej długości jednego boku #M
                ValueB = Math.Round((((double)ValueC * Math.Sin(Gamma)) / (Math.Sin(Alfa))), 2);                                                                                                            //obliczenie długości boku na podstawie domyślnej długości jendego boku #M
                LenghtSides.Add("TValuec", (double)ValueB);                                                                                                                                                 //dodanie do listy obliczonych boków #M
                LenghtSides.Add("TValuea", (double)ValueC);
                LenghtSides.Add("TValueb", (double)ValueA);
            }
            
            return LenghtSides;
        }
        private void SolveTriangel_Click(object sender, RoutedEventArgs e)                                                                                              //metoda generuje rysunek wykresu i oblicza kąty lub też boki trójkąta #M 
        {
            try
            {
                Dictionary<string, double> SolvingDictionary = new Dictionary<string, double>();
                double diffrent = 0;
                double val;
                SolvingDictionary = SolveAfterAngles(SolvingDictionary,                                                                                                 //przypisanie do zmiennej SolvingDictionary listy obliczonych boków na podstawie kątów #M
                    double.TryParse(AngleA.Text, out val) ? (double?)val : null,
                    double.TryParse(AngleB.Text, out val) ? (double?)val : null,
                    double.TryParse(AngleC.Text, out val) ? (double?)val : null,
                    double.TryParse(TValuec.Text, out val) ? (double?)val : null,
                    double.TryParse(TValueb.Text, out val) ? (double?)val : null,
                    double.TryParse(TValuea.Text, out val) ? (double?)val : null
                    );
                bool checkclear = false;
                bool Checkanglessum = true;                                                                                                                             //zmienna sprawdzająca czy summa kąty w trójkącie podane są prawidłwo #M
                if (TValuec.Text == "" && TValueb.Text == "" && TValuea.Text == "" )
                {
                    checkclear = true;
                }
                if (SolvingDictionary.Count != 0)                                                                                                                       //w przypadku gdy lista jest różna od zera oznacza to że obliczenia zostały wykonane na podstawie kątów #M
                {
                    var sol = ShowSolverBox;                                                
                    foreach (var item in SolvingDictionary)                                                                                                             //pętla odpowadająca za wprowadzenie do textboxów listy kątów i boków #M           
                    {
                        TextBox searched = sol.FindName(item.Key) as TextBox;                                                                                           //znalezienie TextBoxa w ShowSolverBox #M
                        searched.Text = item.Value.ToString();                                                                                                          //wprowadzenie do TextBoxa wartości listy #M
                    }
                    if (double.Parse(AngleA.Text) + double.Parse(AngleB.Text) + double.Parse(AngleC.Text) != 180) Checkanglessum = false;                               //sprawdzenie czy suma kontów jest równa 180 jesli nie ustawienie zmiennej na false co uniemożliwi obliczenia #M
                }
                double ValueC = double.Parse(TValuec.Text);
                double ValueA = double.Parse(TValuea.Text);
                double ValueB = double.Parse(TValueb.Text);
                if (checkclear == true)
                {
                    TValuec.Text = "";
                    TValuea.Text = "";
                    TValueb.Text = "";
                }

                if (ValueA + ValueB > ValueC && ValueA + ValueC > ValueB && ValueC + ValueB > ValueA && Checkanglessum == true)                                             //sprawdzenie czy z podanych wartości można stworzyć trójkąt #M
                {
                    Triangle = triangleview.DrawTriangel(TriangeImg, Triangle, TValuea, TValueb, TValuec, AngleA, AngleB, AngleC, diffrent, ValueC, ValueA, ValueB, color); //przypisanie do zmiennej Triangle wyniku funkcji draw triangle efektem czego jest narysowanie trójkąta #M
                    SolveTriangelProperties.IsEnabled = true;                                                                                                               //włączenie okna w którym można wybrać opis danego trójkąta #M
                    SolveTriangelProperties.SelectedIndex = 0;                                                                                                              //ustawienie właściwości na 0 #M
                    drawlabel = true;                                                                                                                                       //zmienna sterująca rysowaniem opisów
                }
                else
                {
                    TValuea.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));                                                                            //w przypadku gdy dane do wygenerowania trójkąta zostały źle podane podświetlenie TextBoxa na czerwono #M
                    TValueb.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    TValuec.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleA.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleB.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    AngleC.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 182, 16, 16));
                    try
                    {
                        TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);                                                                                      //usunięcie poprzedniego trójkąta jeśli jest narysowane #M
                    }
                    catch { }
                    drawlabel = false;                                                                                                                                      //złe dane opisy się nie pokażą #M
                    
                }
            }
            catch { } 
        }

        private void SolveTriangelClear_Click(object sender, RoutedEventArgs e)
        {
            SolveTriangelProperties.SelectedIndex = -1;
            ShowSolverBox.Items.Clear();
            SolveTriangelProperties.IsEnabled = false;
            TypeSolvingGrid.IsEnabled = true;
            TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
            TValuea.Clear();
            TValueb.Clear();
            TValuec.Clear();
            AngleA.Clear();
            AngleB.Clear();
            AngleC.Clear();
            
        }

        private void TriangelSolve_MouseLeave(object sender, MouseEventArgs e)                  //metoda odpowaida za dodanie labelek w trójkąta #M
        {
            if (drawlabel == true)                                                              //w przypadku gdy trójkąt został narysowany wygenerowanie labelek #M
            {
                triangleview.DrawLabel(TriangeImg, Triangle,ref ListLabel,ref ListLabel2);      //dodanie do trójąta labelek #M
            }
            drawlabel = false;
        }

        private void SolverLabel_MouseEnter(object sender, MouseEventArgs e)                    //metoda odpowiedzialna za podświetlenie labelki trójkąta w przypadku najechania na TexTboX w którym znadują się boki trójkąta  #M
        {
            triangleview.EnterToBoxSide(sender, ref ListLabel2);                                //podświetlenie labelki trójkąta #M
        }
 
        private void SolverLabel_MouseLeave(object sender, MouseEventArgs e)                    //metoda odpowiedzialna za podświetlenie labelki trójkąta w przypadku opuszczenia TexTbox'a w którym znadują się boki trójkąta  #M
        {
            triangleview.LeaveBoxSide(sender, ref ListLabel2);                                  //przywrócenie domyślnego stylu labelki trójkąta #M        
        }

        private void SolverLabelAngle_MouseEnter(object sender, MouseEventArgs e)               //metoda odpowiedzialna za podświetlenie labelki trójkąta w przypadku najechania na TexTboX w którym znadują się kąty trójkąta  #M
        {
            triangleview.EnterToBoxAngle(sender, ref ListLabel);                                //podświetlenie labelki trójkąta #M
        }

        private void SolverLabelAngle_MouseLeave(object sender, MouseEventArgs e)               //metoda odpowiedzialna za podświetlenie labelki trójkąta w przypadku opuszczenia TexTbox'a w którym znadują się kąty trójkąta  #M
        {
            triangleview.LeaveBoxAngle(sender, ref ListLabel);                                  //podświetlenie labelki trójkąta #M
        }

        private void SolveTriangelProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)                                                                   //metoda odpowiedzialna za wyświetlenie okna informacji o trójkącie #M
        {
            trangleshowboxview.ShowSolverBox_Window(SolveTriangelProperties.SelectedIndex, ShowSolverBox,TypeSolving, AngleA, AngleB, AngleC, TValuea, TValueb, TValuec);   //wyświetlenie okna informacji o trójkącie w momencie wygenerowania trójkąta #M
        }

        private void TypeSolving_SelectionChanged(object sender, SelectionChangedEventArgs e)     //metoda odpowiedzialna za zablokowania TextBoxów wprowadzania paremetrów trójkąta w zależności od wybranego itemu w polu ComboBox #M                                                                      
        {
            SolveTriangelProperties.SelectedIndex = -1;
            ShowSolverBox.Items.Clear();                                                          //czyszczenie pola w którym znajdująsie informacje o trókącie #M
            SolveTriangelProperties.IsEnabled = false;                                            //zmienna sterująca wyświetlaniem pola informacji o trójkącie #M
            TypeSolvingGrid.IsEnabled = true;
            TriangeImg.Children.RemoveRange(0, TriangeImg.Children.Count);
            int index = TypeSolving.SelectedIndex;
            switch(index)                                                                        //włączenie Textboxów w zależności od wybranego itemu w ComboBox #M
            {
                case 0:
                    AngleA.Clear();                                                             //oczyszczenie TextBoxa odpowiedzialnego za wprowadzanie kąta trójkąta #M
                    AngleB.Clear();
                    AngleC.Clear();
                    TValuea.IsEnabled = true;                                                   //włączenie TextBoxa w który wprowadza się boki trójkąta #M
                    TValueb.IsEnabled = true;
                    TValuec.IsEnabled = true;
                    AngleA.IsEnabled = false;                                                   //wyłączenie Textboxa w który wprowadza się kąty trójkąta #M
                    AngleB.IsEnabled = false;
                    AngleC.IsEnabled = false;
                    break;
                case 1:
                    TValuea.Clear();                                                            //oczyszczenie TextBoxa odpowiedzialnego za wprowadzanie boku trójkąta #M
                    TValueb.Clear();
                    TValuec.Clear();
                    AngleA.IsEnabled = true;                                                    //włączenie Textboxa w który wprowadza się kąty trójkąta #M
                    AngleB.IsEnabled = true;
                    AngleC.IsEnabled = true;
                    TValuea.IsEnabled = false;                                                  //wyłączenie TextBoxa w który wprowadza się boki trójkąta #M
                    TValueb.IsEnabled = false;
                    TValuec.IsEnabled = false;
                    break;
            }
        }
    }
}
