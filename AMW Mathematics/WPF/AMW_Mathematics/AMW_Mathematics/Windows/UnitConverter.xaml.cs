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

namespace AMW_Mathematics.Windows
{
    /// <summary>
    /// Interaction logic for UnitConverter.xaml
    /// </summary>
    public partial class UnitConverter : MetroWindow
    {
        public UnitConverter()
        {
            InitializeComponent();
        }

        private void selectUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)     //Obsugiwanie wyboru kategori jednostki #Ł
        {
            Model.Unit selectedUnit = new Model.Unit(FromCombo, ToCombo, (selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString());  //Wywołanie klasy wpisującej odpowiednie jednostki w zależności od kategorii do Comoboboxów #Ł
            FromCombo.SelectedIndex = 0;            //Ustawienie wybranej pierwszej (z) jednostki na pierwszą z listy #Ł
            ToCombo.SelectedIndex = 0;              //Ustawienie wybranej drugiej (do) jednostki na pierwszą z listy #Ł
            InputBox.Focus();                       //Wybranie pola wprowadzania #Ł
        }
        private void Convert_Click(object sender, RoutedEventArgs e)           //Funkcja obługująca kliknięcie "Convert #Ł
        {
            ConvertButtonClick();                       //Wywołanie funkcji, która obsługuje obliczaniejednostki #Ł
        }
        void ConvertButtonClick()
        {
            double equal;                               //wartość wyjściowa
            try                                        //wyłapywanie błędów podczas konwersji
            {

                Function.UnitFactor factoreValue = new Function.UnitFactor((selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString(), (FromCombo.Items[FromCombo.SelectedIndex] as ComboBoxItem).Content.ToString(), (ToCombo.Items[ToCombo.SelectedIndex] as ComboBoxItem).Content.ToString()); //wywołanie metody zwracającej mnożnik potrzebny do konwersji #Ł
                equal = double.Parse(InputBox.Text) * factoreValue.unitF;     // wymnożenie podanej wartości przez mnożnik #Ł

                if ((selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString().Contains("Temperature"))  //osobna zasada dla kategorii temperatura (jednostki te nie wymagają mnożenia tylko dodawania)  #Ł
                {
                    equal = equal + factoreValue.temp;          //dodanie pobranej wartości potrzebnej do zmiany temperatury #Ł
                }

                OutputBox.Text = equal.ToString();              //wpisanie obliczonej wartości do pola #Ł
            }
            catch (Exception error)                         //jeżeli zostanie złapany błąd podczas konwersji nic nie rób   #Ł  
            {

            }
        }

        private void ToCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)     //Dynamiczne przeliczanie jednostek podczas zmiany jednostki na kórą mamy konwertować #Ł
        {
            if (InputBox.Text != "") ConvertButtonClick();                                    //Wywołanie obliczania jednostek jeżeli pole zawierające wartość potrzebną do przeliczania nie jest puste #Ł
            else InputBox.Focus();                                                            //Wybranie pola wprowadzania #Ł
        }
    }
}
