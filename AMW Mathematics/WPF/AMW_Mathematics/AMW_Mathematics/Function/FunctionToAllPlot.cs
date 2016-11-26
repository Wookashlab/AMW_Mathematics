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
    class FunctionToAllPlot
    {
        public List<Control> AllChildren(DependencyObject parent)                   //Funkcja wyszukuje wszysktie kontrolki znajdujące się w danej Liście #M
        {
            var _List = new List<Control> { };
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var _Child = VisualTreeHelper.GetChild(parent, i);                  //wprowadzenie do zmiennej dziecka Elementu ListView #M
                if (_Child is Control)                                              //sprawdzenie czy jest dziecko jest kontrolką #M
                    _List.Add(_Child as Control);                                   //Jeśli tak dodananie go do listy #M
                _List.AddRange(AllChildren(_Child));                                //Rekurencyjne sprawdzenie czy dziecko ListView nie ma dzieci które też są kontrolkami #M
            }
            return _List;                                                           //zwrócenie listy Kontrolek ListView #M

        }
        public List<string> AddFunctionToList(ListView ListViewFunction, List<string> ListFunction, string name, Keyboard keyboard, Button klawisz, bool which) //metoda odpowiedzialna za dodanie funkcji znadujących w dynamicznej liście generowanej przez widok do listy typu string  #M
        {
            var _ListBox = ListViewFunction as ListBox;
            foreach (var _ListBoxItem in _ListBox.Items)
            {
                var _Container = _ListBox.ItemContainerGenerator.ContainerFromItem(_ListBoxItem);                                                               //wprowadzenie do zmiennej _Container elementu ListView #M
                var _Children = AllChildren(_Container);                                                                                                        //wprowadzenie do zmiennej wszyskich dziecki zmiennej _Container, która jest elementem ListView #M
                var _Name = name;
                var _Control = (TextBox)_Children.First(c => c.Name == _Name);                                                                                  //wprowadzenie do zmiennej _Control pierwszego znalezionego obiektu TextBox o nazwie zadeklarowanej powyżej #M
                ListFunction.Add(_Control.Text);                                                                                                                //dodanie do listy funkcji występującej w TextBox #M
                if (which == true)                                                                                                                              //zmienna odpowiadająca za wprowadzenie do TextBoxa przeznaczonego na funkcjie do generowania wykresów funkcji klikniętego przycisku z klawiatury #M
                {
                    _Control.Text = _Control.Text + keyboard.Click(klawisz.Name.ToString(), klawisz.Content.ToString());                                        //Wprowadzenie do TextBoxa funkcji z klawiatury #M
                }
            }
            return ListFunction;
        }
    }
}
