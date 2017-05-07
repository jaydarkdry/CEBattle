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
        public bool Used = false;
        

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

        public int GetCombatImplication(int force)
        {
            if (Used)
                return 0;
            float value = (float)force * Stat.Attack;
            if (Stat.Time != Config.Time.AllBattle)
                Used = true;
            return (int)value;
        }

        public bool IsOrder()
        {
            switch(Stat.Time)
            {
                case Config.Time.AllBattle:
                case Config.Time.Start:
                case Config.Time.End:
                case Config.Time.DecisionMaking:
                    return true;
                default:
                    return false;
            }
        }

        public bool IsOrderWhen(Config.Time t)
        {
            if (t == Stat.Time  || Stat.Time == Config.Time.AllBattle)
            {
                return true;
            }
            return false;
        }

        public bool CanUseAttack()
        {
            if (Stat.Time == Config.Time.WhenNeeded && !Used && Stat.Attack != 0)
            {
                return true;
            }
            return false;
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

            Stat.Defense = 0;
            Stat.Moral = 0;
            Stat.MoralLimit = 1;
            Stat.Lost = 0;
            Stat.Attack = 0;
            switch (Aid)
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
                    Stat.Time = Config.Time.WhenNeeded;
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
