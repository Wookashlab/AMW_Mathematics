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

        private void selectUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Unit selectedUnit = new Model.Unit(FromCombo, ToCombo, (selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString());
            FromCombo.SelectedIndex = 0;
            ToCombo.SelectedIndex = 0;
            InputBox.Focus();
        }
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            ConvertButtonClick();
        }
        void ConvertButtonClick()
        {
            double equal;
            try
            {

                Function.UnitFactor factoreValue = new Function.UnitFactor((selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString(), (FromCombo.Items[FromCombo.SelectedIndex] as ComboBoxItem).Content.ToString(), (ToCombo.Items[ToCombo.SelectedIndex] as ComboBoxItem).Content.ToString());
                equal = double.Parse(InputBox.Text) * factoreValue.unitF;

                if ((selectUnit.Items[selectUnit.SelectedIndex] as ComboBoxItem).Content.ToString().Contains("Temperature"))
                {
                    equal = equal + factoreValue.temp;
                }

                OutputBox.Text = equal.ToString();
            }
            catch (Exception error)
            {

            }
        }

        private void ToCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InputBox.Text != "") ConvertButtonClick();
            else InputBox.Focus();
        }
    }
}
