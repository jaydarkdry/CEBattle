using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    ///  The implication during the battle
    /// </summary>
    class ImplicationSaboteur
    {

        /// <summary>
        /// The whole army
        /// </summary>
        public int Army { get; }
        
        /// <summary>
        /// The bonus of the army
        /// </summary>
        public int Bonus { get; }

        /// <summary>
        /// The bonus of the army + the army
        /// </summary>
        public int ArmyBonus { get; }

        /// <summary>
        /// The negative bonus to get busted
        /// </summary>
        public float ShowOff { get; }

        /// <summary>
        /// The minimal lost
        /// </summary>
        public int MinimalLost { get; }

        /// <summary>
        /// The threshold when we start to lost unit
        /// </summary>
        public int ReusedThreshold { get; }
        
        public ImplicationSaboteur(int nbArmy, float attack, float showOff)
        {
            Army = nbArmy;
            Bonus = (int)(Army*attack);
            ArmyBonus = Army + Bonus;
            ShowOff = showOff;
            if (Bonus < 0)
            {
                MinimalLost = -Bonus;
                ReusedThreshold = 0;
            }
            else if (Bonus > 0)
            {
                MinimalLost = 0;
                ReusedThreshold = Bonus;
            }
            else
            {
                MinimalLost = 0;
                ReusedThreshold = 0;
            }
        }

        public int ReduceArmy(int deltaWin)
        {
            int realDelta = deltaWin;
            if (ReusedThreshold > 0)
            {
                if (deltaWin < ReusedThreshold)
                {
                    return 0;
                }
                realDelta -= ReusedThreshold;
            }
            int armyDead = realDelta + MinimalLost;
            return Math.Min(armyDead, Army);
        }

        /// <summary>
        /// Found if the General is busted.
        /// </summary>
        /// <returns></returns>
        public bool IsBusted()
        {
            // ShowOff*100  launch random 1-100, is showoff is 0.9  (90) and it's 70, return true, false otherwise.
            return true; // TEMP
            //return false;
        }

        public string ToDevString()
        {
            string retValue = "Implication Saboteur: ";
            retValue += " Army: " + Army;
            retValue += " Bonus: " + Bonus;
            retValue += " ArmyBonus: " + ArmyBonus;
            retValue += " ShowOff: " + ShowOff;
            retValue += " MinimalLost: " + MinimalLost;
            retValue += " ReusedThreshold: " + ReusedThreshold;

            return retValue;
        }

        public string ToTechnicalString()
        {
            string retValue = "Implication du Saboteur: ";

            retValue += " Armée: " + Army;
            retValue += " Unité bonus: " + Bonus;
            retValue += " Orgueil: " + ShowOff;
            retValue += " Perte minimal: " + MinimalLost;
            retValue += " Seuil de perte: " + ReusedThreshold;
            retValue += " --> Armée et bonus: " + ArmyBonus;


            return retValue;
        }

    }
}
