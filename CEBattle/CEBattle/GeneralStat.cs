using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    partial class  GeneralStat : Stat
    {
        /// <summary>
        /// End behaviour
        /// </summary>
        public Config.EndBehaviour Behaviour { get; set; } 
        /// <summary>
        /// Negociation power for decision
        /// </summary>
        public float NegoPower { get; set; }
        /// <summary>
        /// % to give the highlight to the chief
        /// </summary>
        public float ShowOff { get; set; }

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
            retvalue += "Comportement envers les otages: " + Config.EnumToString(Behaviour) + "\n";
            retvalue += "Pouvoir de négociation: " + NegoPower + "\n";

            return retvalue;
        }

    }
}
