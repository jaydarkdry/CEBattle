using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    public class Monstre
    {
        // Constructor

        public string name;
        private int _hp;

        public Monstre(string name, int hp)
        {
            this.name = name;
            _hp = hp;

        }
        // Description des statistiques du monstre
        public void ShowMonsterStats()
        {
            Console.WriteLine("You just encountered a wild: " + name);
            Console.WriteLine("But it only has " + _hp + " health points");

        }

        public void Attack(Player p, int hit)
        {
            if (p.Perte(hit))
            {
                Console.WriteLine("You have been defeated by a wild " + name + " ! I am not proud of you >:(");
            }

            else
            {
                Console.WriteLine("You did not kill the " + name + "he is now mad and killed your family");
            }
        }

        public bool Perte(int hit)

        {
            _hp -= hit;
            if (_hp <= 0)
            {
                _hp = 0;
                return true;
            }
            return false;
        }
    }
}
