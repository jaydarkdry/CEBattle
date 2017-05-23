using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  AddOnStat : Stat
    {
        /// <summary>
        /// When the stat is used
        /// </summary>
        public Config.Time Time;
        /// <summary>
        /// The escape bonus
        /// </summary>
        public float Escape; 

        public AddOnStat() : base()
        {
            Time = Config.Time.Never;
        }

        public override string ToString()
        {
            string retValue = base.ToString();
            retValue += "Temps d'action: " + Config.EnumToString(Time) + "\n";
            retValue += "Chance d'échappatoire: " + this.Escape + "\n";
            return retValue;
        }

    }
}
