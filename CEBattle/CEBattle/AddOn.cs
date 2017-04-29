using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// A relevant help from outside
    /// </summary>
    class AddOn
    {
        public string Name { get; set; }
        public bool Mole { get; set; }
        public Config.Aids Aid { get; set; }
        public Config.AidsLevel AidsLevel { get; set; }

        // Internal stat
        public AddOnStat Stat;

        // Flag
        private bool _used = false;
        

        public AddOn(string name, bool mole, Config.Aids aid, Config.AidsLevel aidsLevel)
        {
            Name = name;
            Mole = mole;
            Aid = aid;
            AidsLevel = aidsLevel;
            Stat = new AddOnStat();
        }

        //Empty constructor need to be populate later
        public AddOn()
        {
            Mole = false;
            Aid = Config.Aids.Defense;
            AidsLevel = Config.AidsLevel.Minimal;
            Stat = new AddOnStat();
        }

        public void ComputeStat()
        {
            float percent = 0;
            switch (AidsLevel)
            {
                case Config.AidsLevel.Minimal:
                    percent += 0.1f;
                    break;
                case Config.AidsLevel.Small:
                    percent += 0.2f;
                    break;
                case Config.AidsLevel.Moderate:
                    percent += 0.3f;
                    break;
                case Config.AidsLevel.High:
                    percent += 0.4f;
                    break;
                case Config.AidsLevel.Essential:
                    percent += 0.5f;
                    break;
            }
            if (Mole)
            {
                percent *= -1;
            }

            switch(Aid)
            {
                case Config.Aids.Attack:
                    Stat.Attack = percent;
                    Stat.Time = Config.Time.WhenNeeded;
                    break;
                case Config.Aids.Defense:
                    Stat.Defense = percent;
                    Stat.Time = Config.Time.WhenNeeded;
                    break;
                case Config.Aids.Divine:
                    Stat.Attack = percent;
                    Stat.Defense = percent;
                    Stat.Moral = 1 - percent;
                    Stat.Time = Config.Time.AllBattle;
                    break;
                case Config.Aids.Escape:
                    Stat.Lost = -percent;
                    Stat.Time = Config.Time.End;
                    break;
                case Config.Aids.Healing:
                    Stat.Lost = -percent / 2;
                    Stat.MoralLimit = 1 - (percent / 2);
                    break;
                case Config.Aids.Moral:
                    Stat.Moral = 1 - percent;
                    Stat.Time = Config.Time.WhenNeeded;
                    break;
                case Config.Aids.Opening:
                    Stat.Attack = percent;
                    Stat.Time = Config.Time.Start;
                    break;
            }
        }

        public override string ToString()
        {
            string retValue = "";

            retValue += "Aide: " + Name + "\n";
            if (Mole)
            {
                retValue += "C'est une taupe!\n";
            }
            retValue += "Son type d'aide: " + Config.EnumToString(Aid) + "\n";
            retValue += "Son ampleur: \n" + Config.EnumToString(AidsLevel) + "\n";

            retValue += "Statistiques: \n" + Stat.ToString() + "\n";

            return retValue;
        }

        public Boolean Validate()
        {
            if (Name == null || Name == "")
            {
                return false;
            }

            return true;
        }
    }
}
