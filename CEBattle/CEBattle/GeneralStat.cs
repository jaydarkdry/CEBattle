using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  GeneralStat : Stat
    {
        public Config.EndBehaviour Behaviour { get; set; } // End behaviour
        public float NegoPower { get; set; } // Negociation power for decision
        public float ShowOff { get; set; } // % to give the highlight to the chief

        public GeneralStat() : base()
        {
            ShowOff = 0.5f;
            Behaviour = Config.EndBehaviour.None;
            NegoPower = 0;
            MoralLimit = 0.9f; // Limit can be achievable
        }

        public override string ToString()
        {
            string retvalue = base.ToString();
            retvalue += "Orgueil: " + ShowOff + "\n";
            retvalue += "Comportement: " + Config.EnumToString(Behaviour) + "\n";
            retvalue += "Pouvoir de négociation: " + NegoPower + "\n";

            return retvalue;
        }

    }
}
