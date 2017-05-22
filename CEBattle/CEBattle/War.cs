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

        // Step
        private int _round = 0;
        private Config.Time _time = Config.Time.Start;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="side">false = side1 and side2 otherwise.</param>
        /// <param name="name"></param>
        /// <param name="nbarmy"></param>
        /// <param name="saboteur"></param>
        /// <param name="att"></param>
        /// <param name="armies"></param>
        /// <returns></returns>
        public General AddGeneral(bool side, string name, int nbarmy, bool saboteur, 
            Config.Attitude att, float[] armies)
        {
            General g = new General(name, nbarmy, saboteur, att, armies);
            if (!side)
            {
                if (_general1 == null)
                {
                    _general1 = new List<General>();
                }
                _general1.Add(g);
            }
            else
            {
                if (_general2 == null)
                {
                    _general2 = new List<General>();
                }
                _general2.Add(g);
            }
            return g;
        }

        public AddOn AddAddOn(bool side, string name, bool mole, Config.Aids aid, 
            Config.AidsLevel aidsLevel)
        {
            AddOn add = new AddOn(name, mole, aid, aidsLevel);
            if (!side)
            {
                if (_addOn1 == null)
                {
                    _addOn1 = new List<AddOn>();
                }
                _addOn1.Add(add);
            }
            else
            {
                if (_addOn2 == null)
                {
                    _addOn2 = new List<AddOn>();
                }
                _addOn2.Add(add);
            }
            return add;
        }

        public General GetGeneral(bool side, int i)
        {
            try {
                if (!side)
                {
                    return _general1[i];
                }
                else
                {
                    return _general2[i];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AddOn getAddOn(bool side, int i)
        {
            try
            {
                if (!side)
                {
                    return _addOn1[i];
                }
                else
                {
                    return _addOn2[i];
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<General> GetGenerals(bool side)
        {
            if (!side)
            {
                return _general1;
            }
            else
            {
                return _general2;
            }
        }

        public List<AddOn> GetAddOns(bool side)
        {
            if (!side)
            {
                return _addOn1;
            }
            else
            {
                return _addOn2;
            }
        }

        public void RemoveGeneral(bool side, int i)
        {
            if (!side)
            {
                _general1.RemoveAt(i);
            }
            else
            {
                _general2.RemoveAt(i);
            }
        }

        public void RemoveAddOn(bool side, int i)
        {
            if (!side)
            {
                _addOn1.RemoveAt(i);
            }
            else
            {
                _addOn2.RemoveAt(i);
            }
        }

        /// <summary>
        /// The signal to enter in war, general can be ready.
        /// </summary>
        public void StartBattle()
        {
            foreach(General g in _general1)
            {
                g.StartBattle();
            }
            foreach (General g in _general2)
            {
                g.StartBattle();
            }
        }

        /// <summary>
        /// A skirmish
        /// </summary>
        /// <returns></returns>
        public Report Skirmish()
        {
            Report r = new Report();

            r.Setup(_general1, _general2, _addOn1, _addOn2, Side1Name, Side2Name, BattleName, _round, _time,
                For1, For2, Fat1, Fat2, InegalityRatio);

            // Base strenght
            r.Phase1();
            // Add on
            r.Phase2();
            // Saboteur
            r.Phase3();
            // Sorting
            r.Phase4();
            // Losing
            r.Phase5();
            // Hostage managing
            r.Phase6();
            /*
            
            */
            // Phase 3
            // Saboteur phase

            // Phase 4
            // Get winning team, calculated ratio of implication and determine loss

            // Phase 5
            // Defense and lost apply

            // Phase 6
            // Morale issue
            return r;
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
