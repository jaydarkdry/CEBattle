using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    class Stat
    {

        public float Moral { get; set; } // The base moral of the troops
        public float MoralLimit { get; set; } // The limits when the troops go away
        public float Lost { get; set; } // Modifier of lost (positive means more)
        public float Attack { get; set; } // Modifier when attacking
        public float Defense { get; set; } // Modifier when defending


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
