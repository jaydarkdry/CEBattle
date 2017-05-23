using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    class Stat
    {

        /// <summary>
        /// The base moral of the troops
        /// </summary>
        public float Moral { get; set; }
        /// <summary>
        /// The limits when the troops go away
        /// </summary>
        public float MoralLimit { get; set; }
        /// <summary>
        /// Modifier of lost (positive means more)
        /// </summary>
        public float Lost { get; set; }
        /// <summary>
        /// Modifier when attacking
        /// </summary>
        public float Attack { get; set; }
        /// <summary>
        /// Modifier when defending
        /// </summary>
        public float Defense { get; set; }


        public Stat()
        {
            Moral = 1;
            MoralLimit = 1f;
            Lost = 0;
            Attack = 0;
            Defense = 0;
        }

        public override string ToString()
        {
            string retValue = "Morale: " + Moral + "\n";
            retValue += "Limite morale: " + MoralLimit + "\n";
            retValue += "Perte en plus: " + Lost + "\n";
            retValue += "Attaque: " + Attack + "\n";
            retValue += "Défense: " + Defense + "\n";

            return retValue;
        }

    }
}
