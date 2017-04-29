using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  WarStat : Stat
    {
        public float Chance { get; set; } // Chance of winning, - = side 1, += side 2

        public WarStat() : base()
        {
            Chance = 0;
        }

        public override string ToString()
        {
            string retValue = base.ToString();
            retValue += "Balance du combat: " + Chance + "\n";
            return retValue;
        }

    }
}
