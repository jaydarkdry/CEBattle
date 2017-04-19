using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEBattle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int a = 0;
            float b = 1.34325f;
            double bb; /* Must be taken from program A to program B*/
            string c;
            bool d;
            int nombre1;
            string[] equips = { "Casque", "Brigantine", "G-string" };

            a = 1;
            bb = 1.328764324;
            c = "klajdjhsadjaadhusa \n shdfds";
            d = true;
            nombre1 = 214;
            int nombre2 = 4;


            Console.WriteLine("Hello World!");
            Console.WriteLine("INT: " + a);
            Console.WriteLine("FLOAT: " + b);
            Console.WriteLine("DOUBLE: " + bb);
            Console.WriteLine("String: " + c);
            Console.WriteLine("Bool: " + d);
            Console.WriteLine(nombre1 + nombre2);

            //.....

            //jsahdhsabdbsa ndbsalkdsajdnmsanmj
            /*
            dasdsadsa
            sa
            */
            nombre1 = 118;
            nombre2 = 64;

            // Do while
            do
            {
                nombre1 += 20;  // nombre1 = nombre1 - 1;
            } while (nombre1 < 190);
            

            // While lopp
            while(nombre1 < 200)
            {
                Console.WriteLine("qwetui");
                nombre1++;
            }
            nombre1 = 118;

            // IF statement
            if (nombre1 >= 119)
            {
                Console.WriteLine("YOOO");
            }
            else if(nombre1 >= 40 && nombre1 < 80) // Else if statement
            {
                Console.WriteLine("PAUL");
            }
            else
            {
                Console.WriteLine("SINON PAS YOO");
            }

            Console.WriteLine(nombre2);

            // How to do switch
            switch(nombre2)
            {
                case 0:
                    Console.WriteLine("BOOM");
                    break;
                case 1:
                    Console.WriteLine("BAAM");
                    break;
                default:
                    Console.WriteLine("Fuck you");
                    break;
            }

            // For loop of doom
            for (int i = 0; i< 40; i++)
            {
                Console.WriteLine("Repete: " + i);
            }

            Console.WriteLine("Nombre1: " + nombre1);

            // Method call
            DisplayMsg(nombre1, b);

            // Complex object settings
            string[] equips2 = (string[]) equips.Clone();

            equips2[2] = "Soutien-gorge";

            // Instance
            Player p1 = new Player("George", nombre1, 0, equips);
            Player p2 = new Player("Ivette", nombre2, 0, equips2);

            // In class calls
            p1.ShowStatCmd();
            Console.WriteLine("VS");
            p2.ShowStatCmd();

            // EXTRA
            p1.Attack(p2, 4);

            Player[] players = new Player[2];
            players[0] = new Player("Gaetan", 3000, 21999, null);
            players[1] = p2;

            foreach (Player p in players)
            {
                p.ShowStatCmd();
            }

            Monstre m1 = new CEBattle.Monstre("Bouftou", 24);

            m1.ShowMonsterStats();
            p2.Attack(m1, 24);
        
            


        }

        public void DisplayMsg(int nb, float fl)
        {
            float sum = nb + fl;
            Console.WriteLine("Super slut: " + sum);
        }
    }
}
