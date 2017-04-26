using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CEBattle
{
    /// <summary>
    /// A hero in the battle, the champion of the battle field.
    /// </summary>
    class General
    {
        public string Name { get; set; }
        public int NbArmy { get; set; }
        public bool Saboteur { get; set; }
        public Config.Attitude Att { get; set; }
        public float[] Armies { get; set; }

        // Empty constructor, need to be populate later
        public General(string name, int nbarmy, bool saboteur, Config.Attitude att, float[] armies )
        {
            Name = name;
            NbArmy = nbarmy;
            Saboteur = saboteur;
            Att = att;
            Armies = armies;

            Armies = new float[Config.UnitType];
            
            for(int i=0; i<Armies.Length; i++)
            {
                Armies[i] = armies[i];
            }
        }

        public General()
        {
            Saboteur = false;
            Att = Config.Attitude.Neutral;
        }


        public Boolean Validate()
        {
            if (Name == null || Name == "")
            {
                return false;

            }
            if (NbArmy == 0)
            {
                return false;

            }
            if (Armies.Length == 0)
            {
                return false;

            }
            
            return true;
        }
    }
}
