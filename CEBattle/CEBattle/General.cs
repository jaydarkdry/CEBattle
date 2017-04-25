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
        private int _hp;
        private int _mp;
        private string _name;
        private bool _status;
        // Empty constructor, need to be populate later
        public General(int hp, int mp, string name)
        {
            _status = true;
            _hp = hp;
            _mp = mp;
            _name = name;
        }

        public string GetStat()
        {
            string text = "";
            text += "HP: " + _hp + "\n";
            text += "MP: " + _mp + "\n";
            text += "Name: " + _name + "\n";
            text += "Status: " + _status + "\n";
            return text;

        }

        public void Insult(General ennemi, int words)
        {
            
        }

        public void Attack(General ennemi, int damage)
        {
            ennemi.Wound(damage);
        }

        public void Wound(int damage)
        {
            _hp -= damage;
            if (_hp < 0)
            {
                _status = false;
            }
        }

        public Boolean Validate()
        {
            //TODO
            return true;
        }
    }
}
