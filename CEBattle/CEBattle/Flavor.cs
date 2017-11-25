using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    class Flavor
    {
        /// <summary>
        /// Get random fighting scene
        /// </summary>
        /// <returns></returns>
        public static string Fighting()
        {
            int text = WarMath.ResultPower(5);
            switch(text)
            {
                case 0:
                    return "Le fracas des épées se fait entendre.\n";
                case 1:
                    return "La terre frémit sous le pied de attaquants.\n";
                case 2:
                    return "Les généraux chargent sans merci.\n";
                case 3:
                    return "Une volée de flèche siffle dans l'air.\n";
                case 4:
                    return "La première ligne défensive est percée.\n";
                default:
                    return "L'ardeur des combattants résonnent dans l'air.\n";
            }
        }
    }
}
