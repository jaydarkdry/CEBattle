using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEBattle
{
    /// <summary>
    /// The report of a Skirmish.
    /// </summary>
    class Report
    {
        // Phase 1
        private string _battleName;
        private string[] _sides;
        private List<General>[] _general;
        private List<AddOn>[] _addon;
        private List<Implication>[] _imp;
        private int[] _force;
        private int[] _totalForce;
        private int[] _totalForceBonus;
        private int[] _add;
        private int[] _counterAdd;
        private int _round;
        private Config.Time _time;



        // Text
        string _essential = "";
        string _detailed = "";
        string _technical = "";

        static int SIDE = 2;


        public Report()
        {

        }

        /// <summary>
        /// Setup all the variables for the skimish phases
        /// </summary>
        /// <param name="gen1"></param>
        /// <param name="gen2"></param>
        /// <param name="add1"></param>
        /// <param name="add2"></param>
        public void Setup(List<General> gen1, List<General> gen2, List<AddOn> add1, List<AddOn> add2,
            string side1, string side2, string battleName, int turn, Config.Time time)
        {
            _general = new List<General>[2];
            _general[0] = gen1;
            _general[1] = gen2;
            _addon = new List<AddOn>[2];
            _addon[0] = add1;
            _addon[1] = add2;
            _sides = new string[2];
            _sides[0] = side1;
            _sides[1] = side2;
            _battleName = battleName;
            _round = turn;
            _time = time;

            _imp = new List<Implication>[2];
            _force = new int[2];
            _force[0] = 0;
            _force[1] = 0;
            _totalForce = new int[2];
            _totalForce[0] = 0;
            _totalForce[1] = 0;
            _totalForceBonus = new int[2];
            _totalForceBonus[0] = 0;
            _totalForceBonus[1] = 0;
            _add = new int[2];
            _add[0] = 0;
            _add[1] = 0;
            _counterAdd = new int[2];
            _counterAdd[0] = 0;
            _counterAdd[1] = 0;
        }

        public void Phase1()
        {
            // Phase 1
            // base strenght with base bonus
            // Inputs:
            // Generals
            // Generals.Attack
            if (_general == null && _addon == null)
            {
                return;
            }
            
            _technical += "Escarmouche " + _round + " (" + Config.EnumToString(_time) + ") dans la bataille de " + _battleName + ": \n";
            _technical += "Implication des généraux dans la bataille: \n";

            for (int i =0; i < SIDE; i++)
            {
                _technical += "Côté " + _sides[i] + "\n";
                _imp[i] = new List<Implication>();
                foreach (General g in _general[i])
                {
                    Implication imp = g.GetImplication();
                    _imp[i].Add(imp);
                    _technical += g.Name + ": ";
                    if (imp == null)
                    {
                        _technical += "Saboteur\n";
                    }
                    else
                    {
                        _technical += imp.ToTechnicalString() + "\n";
                    }
                    
                }

                for (int j = 0; j < _imp[i].Count; j++)
                {
                    if (_imp[i][j] != null)
                    {
                        _force[i] += _imp[i][j].ArmyBonus;
                        _totalForce[i] += _imp[i][j].ArmyTotal;
                        _totalForceBonus[i] += _imp[i][j].ArmyTotal + _imp[i][j].Attack;
                    }
                }
                _technical += "donne un total de: " + _force[i] + " sur un maximum de " + _totalForce[i] + " (" + _totalForceBonus[i] + " avec bonus)\n\n";
            }
            
        }

        public void Phase2()
        {
            // Phase 2
            // Add-on essentials
            // Normal
            _technical += "Implication des aide extérieures OBLIGATOIRE dans la bataille: \n";
            for (int i=0; i<SIDE; i++)
            {
                _technical += "Côté " + _sides[i] + "\n";
                if (_addon[i] != null)
                    foreach (AddOn a in _addon[i])
                    {
                        Console.WriteLine(a.Name + " on side: " + _sides[i]);
                        if (a.IsOrder() && a.IsOrderWhen(_time))
                        {
                            int imp = a.GetCombatImplication(_totalForce[i]);
                            _add[i] += imp;
                            _technical += a.Name + ": " + imp + "\n";
                        }
                    }
            }

            _technical += "Résultat de l'escarmouche sans ajout: \n";

            for (int i = 0; i < SIDE; i++)
            {
                _totalForceBonus[i] = _force[i] + _add[i];
                _technical += "Côté " + _sides[i] + ": " + _totalForceBonus[i] + "\n";
            }

            // Phase 2 continues, add another round of reéquilibration
            int[] order = { 0, 1 };
            // Normally, if 1 is greater than 0, 1 can have another shot.
            // If 0 is greater than 1, than 1 has the rights.
            if (_totalForceBonus[0] > _totalForceBonus[1])
            {
                order[0] = 1;
                order[1] = 0;
            }
            else if(_totalForceBonus[0] == _totalForceBonus[1])
            {
                // For tie, get 50% to get advantage
                float rand = WarMath.ResultPower(10);
                if (rand < 5)
                {
                    order[0] = 0;
                    order[1] = 1;
                }
                else
                {
                    order[0] = 1;
                    order[1] = 0;
                }
            }

            // Ne entre pas ici TODO
            // The weakest attack first as a reply if he can!!
            if (_addon[order[0]] != null)
            {
                foreach (AddOn a in _addon[order[0]])
                {
                    if (a.CanUseAttack())
                    {
                        int imp = a.GetCombatImplication(_totalForce[order[0]]);
                        _counterAdd[order[0]] = imp;
                        _technical += "Côté " + _sides[order[0]] + " contre attaque avec " + a.Name + ": " + imp + "\n";
                    }
                }
            }
            // If the help is useful, we can counter attack the counter attack
            if (_counterAdd[order[0]] > 0)
            {
                // Fight back
                if (_addon[order[1]] != null)
                {
                    foreach (AddOn a in _addon[order[1]])
                    {
                        if (a.CanUseAttack())
                        {
                            int imp = a.GetCombatImplication(_totalForce[order[1]]);
                            _counterAdd[order[1]] = imp;
                            _technical += "Côté " + _sides[order[1]] + " contre attaque la contre attaque avec " + a.Name + ": " + imp + "\n";
                        }
                    }
                }
            }

            _technical += "Résultat de l'escarmouche AVEC ajout: \n";

            for (int i = 0; i < SIDE; i++)
            {
                _totalForceBonus[i] = _force[i] + _add[i] + _counterAdd[i];
                _technical += "Côté " + _sides[i] + ": " + _totalForceBonus[i] + "\n";
            }

            /*

            // Check if needed
            // Logic is if losing use one needed, can reply to it one. (If it's a mole, too bad)
            force1 += add1;
            force2 += add2;
            int finalAdd1 = 0;
            int finalAdd2 = 0;
            if (force1 > force2)
            {
                // Force 2 can reply
                if (_addOn2 != null)
                    foreach (AddOn a in _addOn2)
                    {
                        if (a.CanUseAttack())
                        {
                            finalAdd2 = a.GetCombatImplication(totalForce2);
                        }
                    }
                if (finalAdd2 != 0)
                {
                    // Fight back
                    if (_addOn1 != null)
                        foreach (AddOn a in _addOn1)
                        {
                            if (a.CanUseAttack())
                            {
                                finalAdd1 += a.GetCombatImplication(totalForce1);
                            }
                        }
                }
            }
            else
            {
                // Force 1 can reply
                if (_addOn1 != null)
                    foreach (AddOn a in _addOn1)
                    {
                        if (a.CanUseAttack())
                        {
                            finalAdd1 = a.GetCombatImplication(totalForce1);
                        }
                    }
                if (finalAdd1 != 0)
                {
                    // Fight back
                    if (_addOn2 != null)
                        foreach (AddOn a in _addOn2)
                        {
                            if (a.CanUseAttack())
                            {
                                finalAdd2 += a.GetCombatImplication(totalForce2);
                            }
                        }
                }
            }*/
        }

        /// <summary>
        /// Return the Essential log of the battle
        /// </summary>
        /// <returns></returns>
        public String ToEssential()
        {
            return _essential;
        }

        /// <summary>
        /// Return the Detailed log of the battle
        /// </summary>
        /// <returns></returns>
        public String ToDetailed()
        {
            return _detailed;
        }
        
        /// <summary>
        /// Return the Technical log of the battle
        /// </summary>
        /// <returns></returns>
        public String ToTechnical()
        {
            return _technical;
        }
    }
}
