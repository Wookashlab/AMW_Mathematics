using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMW_Mathematics.ModelView
{
    public class GraphingHelp
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string MainHelp { get; set; }

        public GraphingHelp(int a)
        {
            if (a == 1) this.RownaniaFunkcje2d();
            else this.ZestawDanych();
        }
        public void RownaniaFunkcje2d()
        {
            Title = "Pomoc do zakładki \"Wyrkesy\"";
            SubTitle = "Wzory i równania 2D - układ kartezjański";
            MainHelp = "Wprowadź równanie lub funkcj, którą chcesz narysować.\n\nPrzykłady\ny = x + 3\ny = sqrt(x^2 + 2)\ny = x ^ 2 + 2x - 1 - 2";
        }
        public void ZestawDanych()
        {
            Title = "Pomoc do zakładki \"Wyrkesy\"";
            SubTitle = "Zestaw danych 2D - układ kartezjański";
            MainHelp = "Wprowadź zestaw danych, którą chcesz narysować.\n\nWskazówki:\nKliknij przycisk \"Wstaw zestaw danych\", aby wprowadzić dane w łatwy sposób.\nJeżeli przechowujesz dane w zmiennej możesz wprowadzić jej nazwę do \nokna wprowadzania\n\nPrzykłady\n{{1,1}, {- 1,1}, {- 3,9}, {2,4}}";
        }
    }
}