using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CEBattle
{
    /// <summary>
    /// A hero in the battle, the champion of the battle field.
    /// </summary>
    class General
    {
        public string Name { get; set; }
        public int NbArmy { get; set; }
        public bool Saboteur { get; set; }
        public Config.Attitude Att { get; set; }
        public float[] Armies { get; set; } //TODO

        // Stat
        public GeneralStat Stat;

        // Flag
        private bool _defeated = false;
        private bool _hostage = false;
        
        // Empty constructor, need to be populate later
        public General(string name, int nbarmy, bool saboteur, Config.Attitude att, float[] armies )
        {
            Name = name;
            NbArmy = nbarmy;
            Saboteur = saboteur;
            Att = att;
            Armies = armies;

            Armies = new float[Config.UnitType];
            
            for(int i=0; i<Armies.Length; i++)
            {
                Armies[i] = armies[i];
            }

            Stat = new GeneralStat();
        }

        public General()
        {
            Saboteur = false;
            Att = Config.Attitude.Neutral;
            Stat = new GeneralStat();
        }

        /// <summary>
        ///  Compute the base stats
        /// </summary>
        public void ComputeStat()
        {
            switch(Att)
            {
                case Config.Attitude.Scared:
                    // -20% morale
                    // 20%  defense
                    // -40% to show off general
                    // -10% moraleLimit
                    // -20% lost
                    // -10% attack
                    // Behaviour none
                    // -10% Nego
                    Stat.Moral = 1-0.2f;
                    Stat.Defense = 0.2f;
                    Stat.ShowOff = -0.4f;
                    Stat.MoralLimit = 0.9f-0.1f;
                    Stat.Lost = -0.2f;
                    Stat.Attack = -0.1f;
                    Stat.Behaviour = Config.EndBehaviour.None;
                    Stat.NegoPower = -0.1f;
                    break;
                case Config.Attitude.Passif:
                    // -10% morale
                    // 10%  defense
                    // -20% to show off general
                    // -5% moraleLimit
                    // -10% lost
                    // -5% attack
                    // Behaviour Mercy
                    // 5% Nego power
                    Stat.Moral = 1-0.1f;
                    Stat.Defense = 0.1f;
                    Stat.ShowOff = -0.2f;
                    Stat.MoralLimit = 0.9f-0.05f;
                    Stat.Lost = -0.1f;
                    Stat.Attack = -0.05f;
                    Stat.Behaviour = Config.EndBehaviour.Mercy;
                    Stat.NegoPower = 0.05f;
                    break;
                case Config.Attitude.Neutral:
                    // Nothing
                    Stat.Behaviour = Config.EndBehaviour.Mercy;
                    break;
                case Config.Attitude.Agressive:
                    // 10% morale
                    // -10%  defense
                    // -20% to show off general
                    // -5% moraleLimit
                    // -10% lost
                    // 5% attack
                    // Behaviour: taking hostage
                    // 5 % Nego power
                    Stat.Moral = 1+0.1f;
                    Stat.Defense = -0.1f;
                    Stat.ShowOff = 0.2f;
                    Stat.MoralLimit = 0.9f+0.05f;
                    Stat.Lost = 0.1f;
                    Stat.Attack = 0.05f;
                    Stat.Behaviour = Config.EndBehaviour.Hostage;
                    Stat.NegoPower = 0.05f;
                    break;
                case Config.Attitude.Suicide:
                    // 20% morale
                    // -20%  defense
                    // -40% to show off general
                    // -10% moraleLimit
                    // -20% lost
                    // 10% attack
                    // Behaviour: carnage
                    // -10% Nego power
                    Stat.Moral = 1+0.2f;
                    Stat.Defense = -0.2f;
                    Stat.ShowOff = 0.4f;
                    Stat.MoralLimit = 0.9f + 0.10f;
                    Stat.Lost = 0.2f;
                    Stat.Attack = 0.1f;
                    Stat.Behaviour = Config.EndBehaviour.Carnage;
                    Stat.NegoPower = -0.1f;
                    break;
            }
        }
        
        public Boolean Validate()
        {
            if (Name == null || Name == "")
            {
                return false;

            }
            if (NbArmy == 0)
            {
                return false;

            }
            if (Armies.Length == 0)
            {
                return false;

            }
            
            return true;
        }

        public override string ToString()
        {
            string retValue = "";

            retValue += "General " + Name + "\n";
            retValue += NbArmy + " sont sous ses ordres\n";
            if (Saboteur)
            {
                retValue += "C'est un Saboteur!\n";
            }
            retValue += "Son tempérament: " + Config.EnumToString(Att) + "\n";
            retValue += "Statistiques: \n" + Stat.ToString() + "\n";

            return retValue;
        }

        // Static methods
        /// <summary>
        /// According to the _negoPower, we must choose a general that will take the decision according 
        /// to the fate of the losing team. 
        /// </summary>
        /// <param name="gen"></param>
        /// <returns></returns>
        static General DecisionMaking(List<General> gen)
        {
            return null;
        }
    }
}
