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
            int a = WarMath.ResultBalance(500,123);
            Console.WriteLine("Mon chiffre est " + a);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // When the fight button is clicked.
        private void _fightBtn_Click(object sender, EventArgs e)
        {
            // return and display message in _errorMsg.text is anything is wrong
            if (_battleName.Text == "")
            {
                _errorMsg.Text = "Veuillez inscire le nom de la bataille";
            }
            if (_armyName1.Text == "")
            {
                _errorMsg.Text = "Veuillez inscrire le nom de la première armée";
            }
            if (_armyName2.Text == "")
            {
                _errorMsg.Text = "Veuillez inscrire le nom de la deuxième armée";
            }
            // Step 1, check if all the basis are fill (_battleName, _armyName1, _armyName2) ""

            // Step 2, deeper analysis, at least one: _generalCB1 and _generalCB2
            if (!_war.Validate())
            {
                _errorMsg.Text = "Veuillez inscrire des généraux au minimum";
            }
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
