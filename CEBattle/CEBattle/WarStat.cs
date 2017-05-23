using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  WarStat : Stat
    {
        /// <summary>
        /// Chance of winning, - = side 1, += side 2
        /// </summary>
        public float Chance { get; set; }

        public WarStat() : base()
        {
            Chance = 0;
        }

        public override string ToString()
        {
            string retValue = "Balance du combat: " + Chance + "\n";
            return retValue;
        }

    }
}
