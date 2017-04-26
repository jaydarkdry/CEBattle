using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// The great combat, contains instance of generals, add-on and more on both side!
    /// </summary>
    class War
    {
        private List<General> _general1 = null;
        private List<General> _general2 = null;

        private List<AddOn> _addOn1 = null;
        private List<AddOn> _addOn2 = null;

        // Empty constructor
        public War()
        {

        }

        public bool Validate()
        {
            if (_general1 == null)
            {
                return false;
            }
            foreach (General g in _general1)
            {
                if (!g.Validate())
                {
                    return false;
                }
            }

            if (_general2 == null)
            {
                return false;
            }
            foreach (General g in _general2)
            {
                if (!g.Validate())
                {
                    return false;
                }
            }

           

            return true;
        }

    }
}
