using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// A relevant help from outside
    /// </summary>
    class AddOn
    {
        public string Name { get; set; }
        public bool Mole { get; set; }
        public Config.Aids Aid { get; set; }
        public Config.AidsLevel AidsLevel { get; set; }
            
        public AddOn(string name, bool mole, Config.Aids aid, Config.AidsLevel aidsLevel)
        {
            Name = name;
            Mole = mole;
            Aid = aid;
            AidsLevel = aidsLevel;
        }

        //Empty constructor need to be populate later
        public AddOn()
        {
            Mole = false;
            Aid = Config.Aids.Defense;
            AidsLevel = Config.AidsLevel.Minimal;
        }

        public Boolean Validate()
        {
            if (Name == null || Name == "")
            {
                return false;
            }

            return true;
        }
    }
}
