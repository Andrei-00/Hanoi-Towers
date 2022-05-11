using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int N = Program.N; // Acesta va fi numarul de discuri - acesta va trebui citit de undeva, nu sa ii atribuim noi valoare
        private int cnt, nr = 0;
        private int Height = 15; // inaltimea unui disc
        private int[] XTija=new int[5]; // va retine abscisa din stanga a fiecarei tije (x-ul unde incepe tija) 
        private int[] len = new int[5]; // va retine cate discuri avem pe fiecare tija
        private int YTijaJOS = 300; // retine ordonata de jos a tijelor (practic unde e solul)
        // urmatorii 4 vectori vor retine pentru fiecare disc cate 4 proprietati
        // si anume coordonatele coltului stanga-sus, respectiv inaltimea si latimea (H si W)
        private int[] Xdisc = new int[15];
        private int[] Ydisc = new int[15];
        private int[] Hdisc = new int[15];
        private int[] Wdisc = new int[15];
        
        public void panel1_Paint(object sender, PaintEventArgs e)
        {
            // ne pregatim culorile cu care vom lucra
            Graphics gObject = panel1.CreateGraphics();
            Brush cblue = new SolidBrush(Color.CadetBlue);
            Brush orange=new SolidBrush(Color.Orange);  
            Brush black = new SolidBrush(Color.Black);
            
            // tijele vor fi echidistante
            XTija[0] = 206;
            XTija[1] = 506;
            XTija[2] = 806;
            len[0] = N; // initial toate discurile vor fi pe prima tija
            len[1] = len[2] = 0;
            
            // se construiesc cele 3 tije 
            // functia AfisareTija apelata de 3 ori va desena cu albastru cele 3 tije 
            AfisareTija(0);
            AfisareTija(1);
            AfisareTija(2);
            
           
           
            for (cnt = N; cnt >= 1; cnt--)
            {
                // la fiecare 0.2 secunde vom pune pe prima tija discurile, incepand de la cel mai mare
                System.Threading.Thread.Sleep(200); // functie care regleaza intervalul de 0.2 s
                // se calculeaza coordonatele tuturor discurilor pentru plasarea lor pe prima tija
                Xdisc[cnt] = XTija[0] - cnt * 20; // cu cat constanta inmultita cu cnt este mai mare
                // cu atat este mai vizibila diferenta de lungime intre discuri
                // deci putem in loc de 20 sa punem un numar mai mic, in cazul in care dorim sa rezolvam problema cu mai multe discuri
                Ydisc[cnt] = YTijaJOS - (nr + 1) * (Height+1);
                Wdisc[cnt] = cnt * 40 + 5; // constanta inmultita cu cnt va fi mereu dublul constantei precedente !!!!!
                // asta se intampla ca aranjarea sa fie simetrica, iar discurile plasate mereu pe mijloc
                Hdisc[cnt] = Height;
                // vom desena cu portocaliu cele N discuri
                gObject.FillRectangle(orange, Xdisc[cnt], Ydisc[cnt], Wdisc[cnt], Hdisc[cnt]); 
                nr++;

                // dupa fiecare aparitie a unui disc, dorim ca tija sa nu se vada prin acesta
                // motiv pentru care vom sterge liniile albastre au ramas patrunse in interiorul discului
                //gObject.DrawLine(blackPen, XTija[0], Ydisc[cnt] + 1, XTija[0], Ydisc[cnt] + Hdisc[cnt]-1);
                //gObject.DrawLine(blackPen, XTija[0]+5, Ydisc[cnt] + 1, XTija[0]+5, Ydisc[cnt] + Hdisc[cnt] - 1);
            }
            
            System.Threading.Thread.Sleep(1000); // inainte de inceperea jocului se va astepta o secunda

           

            // se apeleaza functia recursiva care va efectua mutarile
            Hanoi(N, 0, 2, 1);
         
            


        }

        public void AfisareTija(int ind)
        {
            Graphics gObject = panel1.CreateGraphics();
            Brush cblue = new SolidBrush(Color.CadetBlue);
            
            Brush orange = new SolidBrush(Color.Orange);
            
            Brush black = new SolidBrush(Color.Black);
            
            gObject.FillRectangle(cblue, XTija[ind], 6, 5, 295);
        }

        public void Mutare(int cnt, int ind1, int ind2)
        {
            // definim din nou culorile
            Graphics gObject = panel1.CreateGraphics();
            Brush cblue = new SolidBrush(Color.CadetBlue);
            Brush orange = new SolidBrush(Color.Orange);           
            Brush black = new SolidBrush(Color.Black);

            // se va astepta 0.5s intre fiecare 2 discuri mutate
            System.Threading.Thread.Sleep(500);
            // intai se sterge discul cnt din varful tijei ind1
            // stergerea se face prin umplerea unui dreptunghi de culoarea backgroundului peste dreptunghiul discului nostru
            gObject.FillRectangle(black, Xdisc[cnt], Ydisc[cnt], Wdisc[cnt], Hdisc[cnt]);
            // odata cu stergerea, trebuie sa umplem golurile lasate care apartin tija 
            // caci acum se vede inca o portiune, din moment ce am scos discul
            // asadar umplem un dreptunghi albastru
            gObject.FillRectangle(cblue, XTija[ind1], Ydisc[cnt], 5, Hdisc[cnt]);
            // numarul de discuri de pe tija ind1 scade cu 1
            len[ind1]--;

            System.Threading.Thread.Sleep(250);
            gObject.FillRectangle(orange, Xdisc[cnt], 8, Wdisc[cnt], Hdisc[cnt]);
            
            // alte delayuri pt configuratia mutarii de pe un disc pe altu

            System.Threading.Thread.Sleep(750);
            gObject.FillRectangle(black, Xdisc[cnt], 8, Wdisc[cnt], Hdisc[cnt]);
            gObject.FillRectangle(cblue, XTija[ind1], 8, 5, Hdisc[cnt]);
            Xdisc[cnt] = XTija[ind2] - cnt * 20;
            gObject.FillRectangle(orange, Xdisc[cnt], 8, Wdisc[cnt], Hdisc[cnt]);




            // dupa ce discul cnt va fi sters din tija ind1, se va astepta 0.25 secunde
            // apoi il punem pe tija ind2
            System.Threading.Thread.Sleep(250);
            gObject.FillRectangle(black, Xdisc[cnt], 8, Wdisc[cnt], Hdisc[cnt]);
            gObject.FillRectangle(cblue, XTija[ind2], 8, 5, Hdisc[cnt]);
            // se calculeaza noi coordonate ale discului cnt pe care le va ocupa pe tija ind2
            Xdisc[cnt] = XTija[ind2] - cnt * 20;
            Ydisc[cnt] = YTijaJOS - (len[ind2] + 1) * (Height + 1);
            Wdisc[cnt] = cnt * 40 + 5;
            Hdisc[cnt] = Height;
            // umplem discul cu culoarea portocaliu
            gObject.FillRectangle(orange, Xdisc[cnt], Ydisc[cnt], Wdisc[cnt], Hdisc[cnt]);
            // numarul de discuri de pe tija ind2 creste cu 1
            len[ind2]++;
        }

        public void Hanoi(int N, int source, int destination, int aux)
        {
            // se realizeaza algoritmul clasic de rezolvare a problemei turnurilor din Hanoi
            if (N == 1)
            {
                Mutare(1, source, destination);
                return; 
            }
            else
            {
                Hanoi(N - 1, source, aux, destination);
                Mutare(N, source, destination);
                Hanoi(N - 1, aux, destination, source);
                return;
            }
        }

        

    }
}
