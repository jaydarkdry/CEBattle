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
        // Add-on

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

        // Aids Level: The level of exterior help
        public enum AidsLevel
        {
            Minimal,
            Small,
            Moderate,
            High,
            Essential
        }

        static public string[] AidsLevelLbl =
        {
            "Minime",
            "Bas",
            "Modéré",
            "Haut",
            "Essentiel"
        };

        // General attitude
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

        //1
        public enum Fortification
        {
            Nothing,
            Barack,
            Rambard,
            Fort,
            Castle

        }

        static public string[] FortificationLbl =
        {
            "Rien",
            "Baraque",
            "Rambard",
            "Fort",
            "Chateau"
        
        };

        //2
        public enum Fatigue
        {
            None,
            Little,
            Moderate,
            Lot,
            Limit

        }

        static public string[] FatigueLbl =
        {
            "Aucun",
            "Peu",
            "Modéré",
            "Beacoup",
            "Limite"

        };

        
        public enum EndBehaviour
        {
            None,
            Mercy,
            Hostage,
            Carnage

        }

        static public string[] EndBehaviourLbl =
        {
            "Aucun",
            "Grâce",
            "Otage",
            "Massacre"

        };

        public enum Time
        {
            Never,
            Start,
            AllBattle,
            WhenNeeded,
            End,
            DecisionMaking
        }

        static public string[] TimeLbl =
        {
            "Jamais",
            "Commencement",
            "En tout temps",
            "Au besoin",
            "Fin",
            "Décision"
        };


        static public int UnitType = 4;
        static public string NewName = "Nom";


        // Enum to String section
        static public string EnumToString(Aids a)
        {
            return AidsLbl[(int)a];
        }

        static public string EnumToString(Attitude a)
        {
            return AttitudeLbl[(int)a];
        }

        static public string EnumToString(AidsLevel a)
        {
            return AidsLevelLbl[(int)a];
        }

        static public string EnumToString(Fortification a)
        {
            return FortificationLbl[(int)a];
        }

        static public string EnumToString(Fatigue a)
        {
            return FatigueLbl[(int)a];
        }

        static public string EnumToString(EndBehaviour a)
        {
            return EndBehaviourLbl[(int)a];
        }

        static public string EnumToString(Time a)
        {
            return TimeLbl[(int)a];
        }

    }
}
