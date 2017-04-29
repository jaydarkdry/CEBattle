using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// The great combat, contains instance of generals, add-on and more on both side!
    /// </summary>
    class War
    {
        public string BattleName { get; set; } = null;
        public string Side1Name { get; set; } = null;
        public string Side2Name { get; set; } = null;

        private List<General> _general1 = null;
        private List<General> _general2 = null;

        private List<AddOn> _addOn1 = null;
        private List<AddOn> _addOn2 = null;

        public Config.Fortification For1;
        public Config.Fortification For2;

        public Config.Fatigue Fat1;
        public Config.Fatigue Fat2;

        public float InegalityRatio = 0;

        // Stat
        private SideStat _stat1;
        private SideStat _stat2;
        private WarStat _stat;

        // Empty constructor
        public War()
        {
            For1 = Config.Fortification.Nothing;
            For2 = Config.Fortification.Nothing;
            Fat1 = Config.Fatigue.None;
            Fat2 = Config.Fatigue.None;

            _stat1 = new SideStat();
            _stat2 = new SideStat();
            _stat = new WarStat();
        }

        public void ComputeStat()
        {
            if (_general1 != null)
            {
                foreach (General g in _general1)
                {
                    g.ComputeStat();
                }
            }
            if (_general2 != null)
            {
                foreach (General g in _general2)
                {
                    g.ComputeStat();
                }
            }
            if (_addOn1 != null)
            {
                foreach(AddOn a in _addOn1)
                {
                    a.ComputeStat();
                }
            }

            if (_addOn2 != null)
            {
                foreach (AddOn a in _addOn2)
                {
                    a.ComputeStat();
                }
            }

            // Fortification
            ComputeFortification(For1, _stat1);
            ComputeFortification(For2, _stat2);
            ComputeFatigue(Fat1, _stat1);
            ComputeFatigue(Fat2, _stat2);

            ComputeWarStat();
        }

        private void ComputeFortification(Config.Fortification f, SideStat ss)
        {
            switch(f)
            {
                case Config.Fortification.Nothing:
                    // Nothing
                    ss.Defense = 0;
                    break;
                case Config.Fortification.Barack:
                    ss.Defense = 0.05f;
                    break;
                case Config.Fortification.Rambard:
                    ss.Defense = 0.1f;
                    break;
                case Config.Fortification.Fort:
                    ss.Defense = 0.2f;
                    break;
                case Config.Fortification.Castle:
                    ss.Defense = 0.25f;
                    break;
            }
        }

        private void ComputeFatigue(Config.Fatigue f, SideStat ss)
        {
            switch (f)
            {
                case Config.Fatigue.None:
                    // Nothing
                    ss.Moral = 1;
                    ss.MoralLimit = 1;
                    break;
                case Config.Fatigue.Little:
                    ss.Moral = 0.95f;
                    ss.MoralLimit = 1;
                    break;
                case Config.Fatigue.Moderate:
                    ss.Moral = 0.90f;
                    ss.MoralLimit = 1;
                    break;
                case Config.Fatigue.Lot:
                    ss.Moral = 0.80f;
                    ss.MoralLimit = 1;
                    break;
                case Config.Fatigue.Limit:
                    ss.Moral = 0.80f;
                    ss.MoralLimit = 0.90f;
                    break;
            }
        }

        private void ComputeWarStat()
        {
            _stat.Chance = InegalityRatio;
        }

        public override string ToString()
        {
            string retValue = "";
            retValue += "Bataille de " + BattleName + "\n\n\n";
            retValue += "Côté " + Side1Name + "\n\n";
            if (_general1 != null)
            {
                foreach (General g in _general1)
                {
                    retValue += g.ToString() + "\n";
                }
            }
            if (_addOn1 != null)
            {
                foreach (AddOn a in _addOn1)
                {
                    retValue += a.ToString() + "\n";
                }
            }
            retValue += "Niveau de fortification: " + Config.EnumToString(For1) + "\n";
            retValue += "Niveau de fatigue: " + Config.EnumToString(Fat1) + "\n";
            retValue += "Statistique: " + _stat1.ToString();

            retValue += "\n\n";

            retValue += "Côté " + Side2Name + "\n\n";
            if (_general2 != null) {
                foreach (General g in _general2)
                {
                    retValue += g.ToString() + "\n";
                }
            }
            if (_addOn2 != null)
            {
                foreach (AddOn a in _addOn2)
                {
                    retValue += a.ToString() + "\n";
                }
            }
            retValue += "Niveau de fortification: " + Config.EnumToString(For2) + "\n";
            retValue += "Niveau de fatigue: " + Config.EnumToString(Fat2) + "\n";
            retValue += "Statistique: " + _stat2.ToString();

            retValue += "\n\n";

            retValue += "Statistique générale de guerre: " + _stat.ToString();
            return retValue;
        }

        public bool Validate()
        {
            if (BattleName == null || BattleName == "")
            {
                return false;
            }
            if (Side1Name == null || Side1Name == "")
            {
                return false;
            }
            if (Side2Name == null || Side2Name == "")
            {
                return false;
            }
            if (BattleName == null || BattleName == "")
            {
                return false;
            }
            if (_general1 == null)
            {
                return false;
            }
            foreach (General g in _general1)
            {
                if (!g.Validate())
                {
                    return false;
                }
            }

            if (_general2 == null)
            {
                return false;
            }
            foreach (General g in _general2)
            {
                if (!g.Validate())
                {
                    return false;
                }
            }

           

            return true;
        }

    }
}
