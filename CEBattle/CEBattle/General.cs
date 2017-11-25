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
        public bool GeneralDead = false;
        private bool _moralDefeated = false;
        public bool Defeated = false;
        
        // Keep value
        private int _initial;
        
        // Empty constructor, need to be populate later
        public General(string name, int nbarmy, bool saboteur, Config.Attitude att, float[] armies )
        {
            Name = name;
            NbArmy = nbarmy;
            Saboteur = saboteur;
            Att = att;
            Armies = armies;
            _initial = NbArmy;

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

        public void StartBattle()
        {
            _initial = NbArmy;
        }

        public Implication GetImplication()
        {
            if (Saboteur || !IsValid())
            {
                return null;
            }
            else
            {
                return new Implication(this.NbArmy, this.Stat.Attack);
            }
        }

        public ImplicationSaboteur GetImplicationSaboteur()
        {
            return new ImplicationSaboteur(NbArmy, this.Stat.Attack, this.Stat.ShowOff);
        }

        /// <summary>
        /// Reduce the army number WITHOUT any logic.
        /// * Uses for Saboteur
        /// </summary>
        /// <param name="dead">The dead units</param>
        public void ReduceArmy(int dead)
        {
            this.NbArmy -= dead;
            if (NbArmy <= 0)
            {
                NbArmy = 0;
                Defeated = true;
            }
        }

        /// <summary>
        /// Reduce the army number WITH any logic.
        /// * Uses for normal general
        /// </summary>
        /// <param name="lost">The initial lost</param>
        /// <param name="withMoral">Affecting moral?</param>
        /// <returns>The left over unit to die if too much.</returns>
        public int LosingArmy(int lost, bool withMoral=true)
        {
            int leftOver = 0;
            // Step 1, get the real number of lost
            int realLost = (int)(lost * (Stat.Lost+1));
            Console.WriteLine("vrai perte: " + realLost);

            // Step 2, Decrease unit
            int initialActual = NbArmy;
            NbArmy -= realLost;
            Console.WriteLine("Restant: " + NbArmy);

            // Step 3, If units are more
            if (NbArmy <= 0)
            {
                Console.WriteLine("Nombre defait");
                Defeated = true;
                leftOver = -NbArmy;
                NbArmy = 0;

                // Chief dead?
                Console.WriteLine("Show off " + Stat.ShowOff);
                GeneralDead = WarMath.ResultChance(Stat.ShowOff);
                Console.WriteLine("General mort: " + GeneralDead);

                return leftOver;
            }

            // Step 4, decrease moral
            if (withMoral)
            {
                float ratio = (float)(_initial - NbArmy) / _initial;
                float ratioInstant = (float)(initialActual - NbArmy) / initialActual;
                Console.WriteLine("Moral perte: " + ratio);
                Stat.Moral += ratio;
                if (ratioInstant > 0.5f) // Too much for limit
                {
                    Stat.MoralLimit -= 0.2f;
                }
                Console.WriteLine("Moral: " + Stat.Moral);
                Console.WriteLine("Moral limite: " + Stat.MoralLimit);
            }

            // Step 5, is defeated because of moral?
            if (Stat.MoralLimit < Stat.Moral)
            {
                Console.WriteLine("Moral defeated: ");
                Defeated = true;
                _moralDefeated = true;
                // Chief dead?
                GeneralDead = WarMath.ResultChance(Stat.ShowOff);
                Console.WriteLine("General mort: " + GeneralDead);
            }

            return leftOver;
        }
        

        public string ToDetailedHit()
        {
            string txt = "";
            if (Defeated)
            {
                txt += "Le groupe du général " + Name + " est défait.\n";
            }
            if (_moralDefeated)
            {
                txt += "Le défaite est dû au moral.\n";
            }
            if (GeneralDead)
            {
                txt += "Le général " + Name + " a péri pendant la bataille.\n";
            }

            return txt;
        }

        public bool IsRecoverable()
        {
            if (NbArmy > 0 && !GeneralDead && _moralDefeated)
            {
                return true;
            }
            return false;
        }

        public bool CanHostage()
        {
            if (NbArmy > 0 && _moralDefeated)
            {
                return true;
            }
            return false;
        }

        public bool IsRevive(float moralBoost)
        {
            Stat.Moral -= moralBoost;
            if (Stat.Moral < Stat.MoralLimit)
            {
                Defeated = false;
                _moralDefeated = false;
                return true;
            }
            return false;
        }

        public bool IsValid()
        {
            return !Defeated;
        }
        

        /// <summary>
        ///  Compute the base stats
        /// </summary>
        public void ComputeStat()
        {
            // Start value
            Stat.Moral = 0.0f;
            Stat.Defense = 0f;
            Stat.ShowOff = 0.5f;
            Stat.MoralLimit = 0.9f;
            Stat.Lost = 0;
            Stat.Attack = 0;
            Stat.Behaviour = Config.EndBehaviour.None;
            Stat.NegoPower = 0;

            switch (Att)
            {
                case Config.Attitude.Scared:
                    // -20% morale
                    // 20%  defense
                    // -40% to show off general
                    // -10% moraleLimit
                    // -10% attack
                    // Behaviour none
                    // -10% Nego
                    Stat.Moral += 0.2f;
                    Stat.Defense += 0.2f;
                    Stat.ShowOff -= 0.4f;
                    Stat.MoralLimit -= 0.1f;
                    Stat.Attack -= 0.1f;
                    Stat.Behaviour = Config.EndBehaviour.None;
                    Stat.NegoPower -= 0.1f;
                    break;
                case Config.Attitude.Passif:
                    // -10% morale
                    // 10%  defense
                    // -20% to show off general
                    // -5% moraleLimit
                    // -5% attack
                    // Behaviour Mercy
                    // 5% Nego power
                    Stat.Moral += 0.1f;
                    Stat.Defense += 0.1f;
                    Stat.ShowOff -= 0.2f;
                    Stat.MoralLimit -= 0.05f;
                    Stat.Attack -= 0.05f;
                    Stat.Behaviour = Config.EndBehaviour.Mercy;
                    Stat.NegoPower += 0.05f;
                    break;
                case Config.Attitude.Neutral:
                    // Nothing
                    Stat.Behaviour = Config.EndBehaviour.Mercy;
                    break;
                case Config.Attitude.Agressive:
                    // 10% morale
                    // -10%  defense
                    // -20% to show off general
                    // +5% moraleLimit
                    // +10% lost
                    // 5% attack
                    // Behaviour: taking hostage
                    // 5 % Nego power
                    Stat.Moral -= 0.1f;
                    Stat.Defense -= 0.1f;
                    Stat.ShowOff += 0.2f;
                    Stat.MoralLimit += 0.05f;
                    Stat.Lost += 0.1f;
                    Stat.Attack += 0.05f;
                    Stat.Behaviour = Config.EndBehaviour.Hostage;
                    Stat.NegoPower += 0.05f;
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
                    Stat.Moral -= 0.2f;
                    Stat.Defense -= 0.2f;
                    Stat.ShowOff += 0.4f;
                    Stat.MoralLimit += 0.10f;
                    Stat.Lost += 0.2f;
                    Stat.Attack += 0.1f;
                    Stat.Behaviour = Config.EndBehaviour.Carnage;
                    Stat.NegoPower -= 0.1f;
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
