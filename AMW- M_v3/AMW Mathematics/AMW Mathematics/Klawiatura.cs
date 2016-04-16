using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace AMW_Mathematics
{       
    class Klawiatura                                                                    // Klasa SŁUŻĄCA DO OBSŁUGI KLAWIATURY GRAFICZNEJ                 #L
    {
        public void MyszNa (System.Windows.Forms.PictureBox klawisz, string wartosc)    //Zmiana grafiki klawiszy na podświetlone na zielono              #L
        {       
            klawisz.Image= new Bitmap("img/ps/klawa/MyszNa/" + wartosc +".png");  
        }

        public void MyszZa(System.Windows.Forms.PictureBox klawisz, string wartosc)     //Ustawienie grafiki klawiszy na standardowe                      #L
        {
            klawisz.Image = new Bitmap("img/ps/klawa/" + wartosc + ".png");
        }

        public void Click(System.Windows.Forms.PictureBox klawisz, string wartosc, System.Windows.Forms.TextBox tekst)  //Obsługa kliknięcia klawisza     #L
        {
            klawisz.Image = new Bitmap("img/ps/klawa/Click/" + wartosc + ".png");       //zmiana grafiki klawisza na "kliknięty"
            if (wartosc == "Dzielenie") wartosc = "/";  
            if (wartosc == "Mnozenie") wartosc = "*";                                   //obsługa wyjątku WARTOŚĆ != SYMBOLOWI
            if (wartosc == "Dodawanie") wartosc = "+";  
            if (wartosc == "Odejmowanie") wartosc = "-";
            if (wartosc == "Kropka") wartosc = ".";
            if (wartosc == "Przecinek") wartosc = ",";
            tekst.Text += wartosc;                                                      //dodadanie wartosci klaiwsza do lini wporwadzania
            

        }
       

    }
}
