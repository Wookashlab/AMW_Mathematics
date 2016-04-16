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
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }

        int licznik;

        private void Logo_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)            //Licznik odpowiadający za wyświetlenie programu 
        {
            licznik++;
            if (licznik==10)
            {
                Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
