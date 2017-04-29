using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  AddOnStat : Stat
    {
        public Config.Time Time; // When the stat is used

        public AddOnStat() : base()
        {
            Time = Config.Time.Never;
        }

        public override string ToString()
        {
            string retValue = base.ToString();
            retValue += "Temps d'action: " + Config.EnumToString(Time) + "\n";
            return retValue;
        }

    }
}
