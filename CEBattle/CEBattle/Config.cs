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

        public enum Attitude
        {
            Scared,
            Passif,
            Neutral,
            Agressive,
            Suicide
        }

        static public string[] AttitudeLbl =
        {
            "Peureux",
            "Passif",
            "Neutre",
            "Agressif",
            "Suicidaire"
        };

        static public int UnitType = 4;


        // Enum to String section
        static public string EnumToString(Aids a)
        {
            return AidsLbl[(int)a];
        }

        static public string EnumToString(Attitude a)
        {
            return AttitudeLbl[(int)a];
        }

    }
}
