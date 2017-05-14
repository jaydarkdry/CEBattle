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
    class Implication : IEquatable<Implication>, IComparable<Implication>
    {
        /// <summary>
        /// An Id to find the good General according to the performance
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// The number of extra unit
        /// </summary>
        public int Attack { get; }

        /// <summary>
        /// The percent of extra unit
        /// </summary>
        public float AttackPercent { get; }

        /// <summary>
        /// The efficient army
        /// </summary>
        public int Army { get; }

        /// <summary>
        /// The number of unit in the army
        /// </summary>
        public int ArmyTotal { get; }
        /// <summary>
        /// The efficient army with bonus
        /// </summary>
        public int ArmyBonus { get; }

        /// <summary>
        /// Compute the implication
        /// </summary>
        /// <param name="nbArmy"></param>
        /// <param name="attack"></param>
        public Implication(int nbArmy, float attack)
        {
            ArmyTotal = nbArmy;
            Army = WarMath.ResultPower(nbArmy);
            AttackPercent = attack;
            Attack = (int)(ArmyTotal * AttackPercent);
            ArmyBonus = Army + Attack;
        }

        public string ToDevString()
        {
            string retValue = "Implication: ";
            retValue += " ArmyTotal: " + ArmyTotal;
            retValue += " Army: " + Army;
            retValue += " ArmyBonus: " + ArmyBonus;
            retValue += " Attack: " + Attack;
            retValue += " AttackPercent: " + AttackPercent;

            return retValue;
        }

        public string ToTechnicalString()
        {
            string retValue = "Implication: ";
            retValue += " Armée total: " + ArmyTotal;
            retValue += " Armée efficace: " + Army;
            retValue += " Unité bonus: " + Attack;
            retValue += " Pourcentage bonus: " + AttackPercent;
            retValue += " --> Armée efficace avec bonus: " + ArmyBonus;
            

            return retValue;
        }
        
        public bool Equals(Implication other)
        {
            return ArmyBonus == other.ArmyBonus;
        }

        public int CompareTo(Implication other)
        {
            // A null value means that this object is greater.
            if (other == null)
                return -1;
            else
                return -1*this.ArmyBonus.CompareTo(other.ArmyBonus);
        }
    }
}
