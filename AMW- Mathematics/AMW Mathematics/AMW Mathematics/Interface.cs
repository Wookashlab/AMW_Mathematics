using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMW_Mathematics
{
    public partial class Interface : Form
    {
        Liczwyraz l;
        string wejscie;
        Klawiatura klawa = new Klawiatura();
        public Interface()
        {
            InitializeComponent();
           
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            wejscie = textBox1.Text + "=";
            l =  new Liczwyraz();
            listBox1.Items.Add(wejscie + l.liczwyrazenie(wejscie));
            wejscie = "";
            textBox1.Clear();
        }

        private void klaw_MouseDown(object sender, MouseEventArgs e)     // Obsługa użycia przycisków (kliknięcie + przytrzymanie)              #L
        {
                
                var klawisz = (PictureBox)sender;                       //przypisanie wybranego PictureBox-a do zmiennej                        
                string wartosc = klawisz.Name.ToString();               //przepisanie nazwy PictureBox-a do stringa                             
                wartosc =  wartosc.Substring(1);                        //pominięcie litery "k" z nazwy (by wartość == symbolowi)              
                klawa.Click(klawisz,wartosc, textBox1);                 //wywołanie funkcji obsługjącej użycie przycisku                       
            
            
        }
     
        private void klaw_MouseEnter(object sender, EventArgs e)        //Obsługa najechania myszką na przyciski                                #L
        {
            var klawisz = (PictureBox)sender;                           
            string wartosc = klawisz.Name.ToString();
            wartosc = wartosc.Substring(1);
            klawa.MyszNa(klawisz, wartosc);                             
        }

        private void klaw_MouseLeave(object sender, EventArgs e)       //Obsługa zabrania myszki z przycisku                                    #L
        {
            var klawisz = (PictureBox)sender;
            string wartosc = klawisz.Name.ToString();
            wartosc = wartosc.Substring(1);
            klawa.MyszZa(klawisz, wartosc);
        }

        private void klawa_MouseUp(object sender, MouseEventArgs e)    //Obsługa puszczenia przycisku (powtóraka klaw_MouseEnter)               #L
        {
            var klawisz = (PictureBox)sender;
            string wartosc = klawisz.Name.ToString();
            wartosc = wartosc.Substring(1);
            klawa.MyszNa(klawisz, wartosc);
        }
        private void Interface_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                wejscie = textBox1.Text + "=";
                l = new Liczwyraz();
                listBox1.Items.Add(wejscie + l.liczwyrazenie(wejscie));
                wejscie = "";
                textBox1.Clear();
            }
        }

        private void bBksp_Click(object sender, MouseEventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                
            }
            kBksp.Image = new Bitmap("img/ps/klawa/Click/Bksp.png");
        }

        private void kEnter_MouseDown(object sender, MouseEventArgs e)
        {
            button13.PerformClick();
            kEnter.Image = new Bitmap("img/ps/klawa/Click/Enter.png");
        }
    }
}
