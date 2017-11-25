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

        private General _gen1;
        private int _gen1Id;
        private General _gen2;
        private int _gen2Id;

        private AddOn _aid1;
        private int _aid1Id;
        private AddOn _aid2;
        private int _aid2Id;

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

            _war.For1 = (Config.Fortification)_fortificationLevelTB1.Value;
            _war.For2 = (Config.Fortification)_fortificationLevelTB2.Value;
            _war.Fat1 = (Config.Fatigue)_exhaustionLevelTB1.Value;
            _war.Fat2 = (Config.Fatigue)_exhaustionLevelTB2.Value;
            UpdateInegality();

            _war.ComputeStat();
            

            //UniTests
            /*int a = WarMath.ResultBalance(500,123);
            Console.WriteLine("Mon chiffre est " + a);*/
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
                return;
            }
            if (_armyName1.Text == "")
            {
                _errorMsg.Text = "Veuillez inscrire le nom de la première armée";
                return;
            }
            if (_armyName2.Text == "")
            {
                _errorMsg.Text = "Veuillez inscrire le nom de la deuxième armée";
                return;
            }
            // Step 1, check if all the basis are fill (_battleName, _armyName1, _armyName2) ""

            // Step 2, deeper analysis, at least one: _generalCB1 and _generalCB2
            if (!_war.Validate())
            {
                _errorMsg.Text = "Veuillez inscrire des généraux au minimum";
                return;
            }
            // Must validate they contains (call method validate)

            // Multiple skirmish normally
            _war.StartBattle();
            
            _mainTab.SelectedIndex = 1;
            _reportTab.SelectedIndex = 2;
            _technicalTxt.Text = "";
            _detailedTxt.Text = "";

            Report r;
            do
            {
                r = _war.Skirmish();
                string[] text = r.ToTechnical().Split('\n');
                string[] textD = r.ToDetailed().Split('\n');
                for (int i = 0; i < text.Length; i++)
                {
                    _technicalTxt.AppendText(text[i]);
                    _technicalTxt.AppendText(Environment.NewLine);
                    
                }
                for (int i = 0; i < textD.Length; i++)
                {
                    _detailedTxt.AppendText(textD[i]);
                    _detailedTxt.AppendText(Environment.NewLine);
                }

                _war.Round++;
            } while (!r.Final);



        }

        private void _aidCB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load an aid by index
            if (_war.GetAddOns(false) == null)
                return;
            // Load a general by index
            _aid1Id = _aidCB1.SelectedIndex;
            if (_aid1Id >= _war.GetAddOns(false).Count)
                return;
            _aid1 = _war.GetAddOns(false)[_aid1Id];
            _aidName1.Text = _aid1.Name;
            _aidMole1.Checked = _aid1.Mole;
            _aidTypeCB1.SelectedIndex = (int)_aid1.Aid;
            _aidLevelTB1.Value = (int)_aid1.AidsLevel;
        }

        private void _aidCB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load an aid by index
            // Load an aid by index
            if (_war.GetAddOns(true) == null)
                return;
            // Load a general by index
            _aid2Id = _aidCB2.SelectedIndex;
            if (_aid2Id >= _war.GetAddOns(true).Count)
                return;
            _aid2 = _war.GetAddOns(true)[_aid2Id];
            _aidName2.Text = _aid2.Name;
            _aidMole2.Checked = _aid2.Mole;
            _aidTypeCB2.SelectedIndex = (int)_aid2.Aid;
            _aidLevelTB2.Value = (int)_aid2.AidsLevel;
        }

        private void _generalCB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_war.GetGenerals(false) == null)
                return;
            // Load a general by index
            _gen1Id = _generalCB1.SelectedIndex;
            if (_gen1Id >= _war.GetGenerals(false).Count)
                return;
            _gen1 = _war.GetGenerals(false)[_gen1Id];
            _nameGeneral1.Text = _gen1.Name;
            _nbArmy1.Text = "" + _gen1.NbArmy;
            _saboteur1.Checked = _gen1.Saboteur;
            _leaderStatTB1.Value = (int)_gen1.Att;
        }

        private void _generalCB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_war.GetGenerals(true) == null)
                return;
            // Load a general by index
            _gen2Id = _generalCB2.SelectedIndex;
            if (_gen2Id >= _war.GetGenerals(true).Count)
                return;
            _gen2 = _war.GetGenerals(true)[_gen2Id];
            _nameGeneral2.Text = _gen2.Name;
            _nbArmy2.Text = "" + _gen2.NbArmy;
            _saboteur2.Checked = _gen2.Saboteur;
            _leaderStatTB2.Value = (int)_gen2.Att;
        }

        private void _showStat_Click(object sender, EventArgs e)
        {
            Console.WriteLine(_war.ToString());// Show stat
        }

        private void _battleName_TextChanged(object sender, EventArgs e)
        {
            _war.BattleName = _battleName.Text;
        }

        private void _armyName1_TextChanged(object sender, EventArgs e)
        {
            _war.Side1Name = _armyName1.Text;
        }

        private void _armyName2_TextChanged(object sender, EventArgs e)
        {
            _war.Side2Name = _armyName2.Text;
        }

        private void _fortificationLevelTB1_Scroll(object sender, EventArgs e)
        {
            _war.For1 = (Config.Fortification)_fortificationLevelTB1.Value;
            _war.ComputeStat();
        }

        private void _fortificationLevelTB2_Scroll(object sender, EventArgs e)
        {
            _war.For2 = (Config.Fortification)_fortificationLevelTB2.Value;
            _war.ComputeStat();
        }

        private void _exhaustionLevelTB1_Scroll(object sender, EventArgs e)
        {
            _war.Fat1 = (Config.Fatigue)_exhaustionLevelTB1.Value;
            _war.ComputeStat();
        }

        private void _exhaustionLevelTB2_Scroll(object sender, EventArgs e)
        {
            _war.Fat2 = (Config.Fatigue)_exhaustionLevelTB2.Value;
            _war.ComputeStat();
        }

        private void _inegalityLevelTB_Scroll(object sender, EventArgs e)
        {
            UpdateInegality();
            _war.ComputeStat();
        }

        private void UpdateInegality()
        {
            switch (_inegalityLevelTB.Value)
            {
                case 1:
                    _war.InegalityRatio = -0.5f;
                    break;
                case 2:
                    _war.InegalityRatio = -0.25f;
                    break;
                case 3:
                    _war.InegalityRatio = -0.1f;
                    break;
                case 4:
                    _war.InegalityRatio = 0f;
                    break;
                case 5:
                    _war.InegalityRatio = 0.1f;
                    break;
                case 6:
                    _war.InegalityRatio = 0.25f;
                    break;
                case 7:
                    _war.InegalityRatio = 0.50f;
                    break;
            }
            _war.ComputeStat();
        }

        private void _addBtn1_Click(object sender, EventArgs e)
        {
            // Add a general and load some basic information.
            // Also, set the ID on him and modified all in real time
            _generalCB1.Items.Add(Config.NewName);
            _gen1Id = _generalCB1.Items.Count - 1;
            _generalCB1.SelectedIndex = _gen1Id;

            _gen1 = _war.AddGeneral(false, Config.NewName, 0, false, Config.Attitude.Neutral, new float[4]);

            /*_nameGeneral1.Text = Config.NewName;
            _nbArmy1.Text = "0";
            _saboteur1.Checked = false;
            _leaderStatTB1.Value = 0;
            _unitMagician1.Text = "0";
            _unitDistance1.Text = "0";
            _unitClose1.Text = "0";
            _unitTactic1.Text = "0";*/

            UpdateComboBoxes();
            _war.ComputeStat();
        }

        private void _addBtn2_Click(object sender, EventArgs e)
        {
            // Add a general and load some basic information.
            // Also, set the ID on him and modified all in real time
            
            _generalCB2.Items.Add(Config.NewName);
            _gen2Id = _generalCB2.Items.Count - 1;
            _generalCB2.SelectedIndex = _gen2Id;

            _gen2 = _war.AddGeneral(true, Config.NewName, 0, false, Config.Attitude.Neutral, new float[4]);

            _nameGeneral2.Text = Config.NewName;
            _nbArmy2.Text = "0";
            _saboteur2.Checked = false;
            _leaderStatTB2.Value = 0;
            _unitMagician2.Text = "0";
            _unitDistance2.Text = "0";
            _unitClose2.Text = "0";
            _unitTactic2.Text = "0";

            UpdateComboBoxes();
            _war.ComputeStat();
        }

        private void _nameGeneral1_TextChanged(object sender, EventArgs e)
        {
            if (_gen1 == null)
                return;
            _gen1.Name = _nameGeneral1.Text;
            _generalCB1.Items[_gen1Id] = _nameGeneral1.Text;
            _war.ComputeStat();
        }

        private void _nbArmy1_TextChanged(object sender, EventArgs e)
        {
            if (_gen1 == null)
                return;
            int army = 0;
            Int32.TryParse(_nbArmy1.Text, out army);
            _gen1.NbArmy = army;
            _war.ComputeStat();
        }

        private void _saboteur1_CheckedChanged(object sender, EventArgs e)
        {
            if (_gen1 == null)
                return;
            _gen1.Saboteur = _saboteur1.Checked;
            _war.ComputeStat();
        }

        private void _leaderStatTB1_Scroll(object sender, EventArgs e)
        {
            if (_gen1 == null)
                return;
            _gen1.Att = (Config.Attitude)_leaderStatTB1.Value;
            _war.ComputeStat();
        }
        // TODO army type


        private void UpdateComboBoxes()
        {
            List<General> gens = _war.GetGenerals(false);
            if (gens != null)
            {
                for (int i = 0; i < gens.Count; i++)
                {
                    _generalCB1.Items[i] = gens[i].Name;
                }
            }

            gens = _war.GetGenerals(true);
            if (gens != null)
            {
                for (int i = 0; i < gens.Count; i++)
                {
                    _generalCB2.Items[i] = gens[i].Name;
                }
            }

            List<AddOn> adds = _war.GetAddOns(false);
            if (adds != null)
            {
                for (int i = 0; i < adds.Count; i++)
                {
                    _aidCB1.Items[i] = adds[i].Name;
                }
            }

            adds = _war.GetAddOns(true);
            if (adds != null)
            {
                for (int i = 0; i < adds.Count; i++)
                {
                    _aidCB2.Items[i] = adds[i].Name;
                }
            }
        }

        private void _nameGeneral2_TextChanged(object sender, EventArgs e)
        {
            if (_gen2 == null)
                return;
            _gen2.Name = _nameGeneral2.Text;
            _generalCB2.Items[_gen2Id] = _nameGeneral2.Text;
            _war.ComputeStat();
        }

        private void _nbArmy2_TextChanged(object sender, EventArgs e)
        {
            if (_gen2 == null)
                return;
            int army = 0;
            Int32.TryParse(_nbArmy2.Text, out army);
            _gen2.NbArmy = army;
            _war.ComputeStat();
        }

        private void _saboteur2_CheckedChanged(object sender, EventArgs e)
        {
            if (_gen2 == null)
                return;
            _gen2.Saboteur = _saboteur2.Checked;
            _war.ComputeStat();
        }

        private void _leaderStatTB2_Scroll(object sender, EventArgs e)
        {
            if (_gen2 == null)
                return;
            _gen2.Att = (Config.Attitude)_leaderStatTB2.Value;
            _war.ComputeStat();
        }

        private void _delBtn1_Click(object sender, EventArgs e)
        {
            _war.RemoveGeneral(false, _gen1Id);
            _generalCB1.Items.RemoveAt(_gen1Id);
            _gen1 = null;
            _nameGeneral1.Text = "";
            _nbArmy1.Text = "";
            _saboteur1.Checked = false;
            _leaderStatTB1.Value = 0;
            _unitMagician1.Text = "";
            _unitDistance1.Text = "";
            _unitClose1.Text = "";
            _unitTactic1.Text = "";
            _war.ComputeStat();
        }

        private void _delBtn2_Click(object sender, EventArgs e)
        {
            _war.RemoveGeneral(true, _gen2Id);
            _generalCB2.Items.RemoveAt(_gen2Id);
            _gen2 = null;
            _nameGeneral2.Text = "";
            _nbArmy2.Text = "";
            _saboteur2.Checked = false;
            _leaderStatTB2.Value = 0;
            _unitMagician2.Text = "";
            _unitDistance2.Text = "";
            _unitClose2.Text = "";
            _unitTactic2.Text = "";
            _war.ComputeStat();
        }

        private void _aidAddBtn1_Click(object sender, EventArgs e)
        {
            // Add a AddOn and load some basic information.
            // Also, set the ID on him and modified all in real time
            _aidCB1.Items.Add(Config.NewName);
            _aid1Id = _aidCB1.Items.Count - 1;
            _aidCB1.SelectedIndex = _aid1Id;

            _aid1 = _war.AddAddOn(false, Config.NewName, false, Config.Aids.Moral, Config.AidsLevel.Minimal);
            

            UpdateComboBoxes();
            _war.ComputeStat();
        }

        private void _aidTypeCB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_aid1 == null)
                return;
            _aid1.Aid = (Config.Aids)_aidTypeCB1.SelectedIndex;
            _war.ComputeStat();
        }

        private void _aidAddBtn2_Click(object sender, EventArgs e)
        {
            // Add a AddOn and load some basic information.
            // Also, set the ID on him and modified all in real time
            _aidCB2.Items.Add(Config.NewName);
            _aid2Id = _aidCB2.Items.Count - 1;
            _aidCB2.SelectedIndex = _aid2Id;

            _aid2 = _war.AddAddOn(true, Config.NewName, false, Config.Aids.Moral, Config.AidsLevel.Minimal);


            UpdateComboBoxes();
            _war.ComputeStat();
        }

        private void _aidName1_TextChanged(object sender, EventArgs e)
        {
            if (_aid1 == null)
                return;
            _aid1.Name = _aidName1.Text;
            _aidCB1.Items[_aid1Id] = _aidName1.Text;
            _war.ComputeStat();
        }

        private void _aidMole1_CheckedChanged(object sender, EventArgs e)
        {
            if (_aid1 == null)
                return;
            _aid1.Mole = _aidMole1.Checked;
            _war.ComputeStat();
        }

        private void _aidLevelTB1_Scroll(object sender, EventArgs e)
        {
            if (_aid1 == null)
                return;
            _aid1.AidsLevel = (Config.AidsLevel)_aidLevelTB1.Value;
            _war.ComputeStat();
        }

        private void _aidName2_TextChanged(object sender, EventArgs e)
        {
            if (_aid2 == null)
                return;
            _aid2.Name = _aidName2.Text;
            _aidCB2.Items[_aid2Id] = _aidName2.Text;
            _war.ComputeStat();
        }

        private void _aidMole2_CheckedChanged(object sender, EventArgs e)
        {
            if (_aid2 == null)
                return;
            _aid2.Mole = _aidMole2.Checked;
            _war.ComputeStat();
        }

        private void _aidTypeCB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_aid2 == null)
                return;
            _aid2.Aid = (Config.Aids)_aidTypeCB2.SelectedIndex;
            _war.ComputeStat();
        }

        private void _aidLevelTB2_Scroll(object sender, EventArgs e)
        {
            if (_aid2 == null)
                return;
            _aid2.AidsLevel = (Config.AidsLevel)_aidLevelTB2.Value;
            _war.ComputeStat();
        }

        private void _aidDelBtn1_Click(object sender, EventArgs e)
        {
            _war.RemoveAddOn(false, _aid1Id);
            _aidCB1.Items.RemoveAt(_aid1Id);
            _aid1 = null;
            _aidName1.Text = "";
            _aidMole1.Checked = false;
            _aidTypeCB1.SelectedIndex = 0;
            _aidLevelTB1.Value = 0;
            _war.ComputeStat();
        }

        private void _aidDelBtn2_Click(object sender, EventArgs e)
        {
            _war.RemoveAddOn(true, _aid2Id);
            _aidCB2.Items.RemoveAt(_aid2Id);
            _aid2 = null;
            _aidName2.Text = "";
            _aidMole2.Checked = false;
            _aidTypeCB2.SelectedIndex = 0;
            _aidLevelTB2.Value = 0;
            _war.ComputeStat();
        }

        private void _exportBtn_Click(object sender, EventArgs e)
        {

            SaveFile(".\\technical" + _battleName.Text + "_"
                + _armyName1.Text + "_VS_" + _armyName2.Text + "_" + DateTime.Now.Hour + "-"
                + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".txt", _technicalTxt.Text);

            SaveFile(".\\detailed" + _battleName.Text + "_"
                + _armyName1.Text + "_VS_" + _armyName2.Text + "_" + DateTime.Now.Hour + "-"
                + DateTime.Now.Minute + "-" + DateTime.Now.Second + ".txt", _detailedTxt.Text);

            // TODO all text
        }

        private void SaveFile(string filename, string text)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
            file.WriteLine(text);

            file.Close();
        }
        //TODO army type
    }
}
