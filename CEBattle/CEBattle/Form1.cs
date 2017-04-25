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
    public partial class unitTactic1 : Form
    {
        private War _war;

        public unitTactic1()
        {
            InitializeComponent();

            // Setup UI
            _aidTypeCB1.Items.Clear();
            _aidTypeCB2.Items.Clear();
            for (int i=0; i<Config.AidsLbl.Length; i++)
            {
                _aidTypeCB1.Items.Add(Config.AidsLbl[i]);
                _aidTypeCB2.Items.Add(Config.AidsLbl[i]);
            }

            _war = new War();

            //UniTests


            // Camille help
            // Type
            int integer = 0;
            int integer2 = 20;
            long longer = 0;
            float floating = 0.2f;
            double doubler = 0.2;
            string text = "jhsaiudhsdaama  \n jhadsa";
            bool condition = true;

            // Comment
            // dfghjklskjhfds
            /* bla ajdasnd s
            kjsadsa
            nbsakdsa */

            Console.WriteLine("Je suis là!");

            Console.WriteLine("Je suis un string" + text);

            float add = integer + floating;
            Console.WriteLine("Add: " + add);

            // Condition statement
            if (integer2 != integer)
            {
                Console.WriteLine("Yess!");
            }
            else if(integer2 <= integer)
            {
                Console.WriteLine("Satisfied");
            }
            else
            {
                Console.WriteLine("NO!!");
            }

            switch (integer)
            {
                case 0:
                    Console.WriteLine("00!!!");
                    break;
                case 1:
                    Console.WriteLine("111!!");
                    break;
                default:
                    Console.WriteLine("J'em colisse");
                    break;
            }

            // integer - 4  est plus petit que integer2

            if (integer -4 < integer2 && integer2 > 10)
            {

            }
            else
            {

            }


            // loop
            do
            {
                Console.WriteLine("Youppi" + integer2);
                integer2 += 2;
            } while (integer2 < 40);

            // integer2 = 40
            while (integer2 < 40)
            {
                Console.WriteLine("Youppiwhile" + integer2);
            } 

            for(int i=0; i<20; i++)
            {
                Console.WriteLine("Chat" + i);
            }

            //Arrays
            int[] integers = new int[30];

            integers[29] = 0;


            ShowMesg(text);
            General gen1 = new General(10, 20, "Gerard");
            General gen2 = new General(20, 10, "Gertrude");

            ShowMesg(gen1.GetStat());
            ShowMesg(gen2.GetStat());

            gen1.Attack(gen2, 9000);

            ShowMesg(gen2.GetStat());
        }

        public void ShowMesg(string s)
        {
            Console.WriteLine("My message:" + s);
        }

        public int SjsjdSjhss(int a, int b)
        {
            return (a * b)+(b * a);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // When the fight button is clicked.
        private void _fightBtn_Click(object sender, EventArgs e)
        {
            // return and display message in _errorMsg.text is anything is wrong

            // Step 1, check if all the basis are fill (_battleName, _armyName1, _armyName2)

            // Step 2, deeper analysis, at least one: _generalCB1 and _generalCB2
            // Must validate they contains (call method validate)

        }

        private void _aidCB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load an aid by index
        }

        private void _aidCB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load an aid by index
        }

        private void _generalCB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load a general by index
        }

        private void _generalCB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load a general by index
        }
    }
}
