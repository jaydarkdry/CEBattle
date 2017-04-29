using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  SideStat : Stat
    {

        public SideStat() : base()
        {
            
        }

        public override string ToString()
        {
            string retValue = "Morale: " + Moral + "\n";
            retValue += "Limite morale: " + MoralLimit + "\n";
            retValue += "Défense: " + Defense + "\n";
            return retValue;
        }

    }
}
