using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// Static class to handle all enum, toString definition and UI defaut text
    /// </summary>
    class Config
    {
        // Aids: The external group that can help during a battle
        public enum Aids
        {
            Opening,
            Defense,
            Healing,
            Moral,
            Escape,
            Attack,
            Divine
        }

        static public string[] AidsLbl =
        {
            "Ouverture",
            "Défense",
            "Soin",
            "Morale",
            "Échappatoire",
            "Attaque",
            "Divin"
        };


        // Enum to String section
        static public string EnumToString(Aids a)
        {
            return AidsLbl[(int)a];
        }

    }
}
