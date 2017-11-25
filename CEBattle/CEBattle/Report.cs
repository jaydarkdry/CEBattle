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
        /// <summary>
        /// The final battle
        /// </summary>
        public bool Final = false;

        // Phases
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
        private float[] _inegatilies;
        private Config.Fortification[] _for;
        private Config.Fatigue[] _fat;
        private int _round;
        private Config.Time _time;
        private ImplicationSaboteur[] _saboteurImpact;

        private List<General>[] _generalBest;


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
            string side1, string side2, string battleName, int turn, Config.Time time, 
            Config.Fortification for1, Config.Fortification for2, Config.Fatigue fat1, Config.Fatigue fat2, float inegality)
        {
            _general = new List<General>[2];
            _general[0] = gen1;
            _general[1] = gen2;
            _generalBest = new List<General>[2];
            _generalBest[0] = new List<General>();
            _generalBest[1] = new List<General>();
            _addon = new List<AddOn>[2];
            _addon[0] = add1;
            _addon[1] = add2;
            _sides = new string[2];
            _sides[0] = side1;
            _sides[1] = side2;
            _battleName = battleName;
            _round = turn;
            _time = time;
            _inegatilies = new float[2];
            if (inegality < 0)
            {
                _inegatilies[0] = -inegality;
                _inegatilies[1] = 0;
            }
            else
            {
                _inegatilies[0] = 0;
                _inegatilies[1] = inegality;
            }
            _for = new Config.Fortification[2];
            _for[0] = for1;
            _for[1] = for2;
            _fat = new Config.Fatigue[2];
            _fat[0] = fat1;
            _fat[1] = fat2;
            

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
            _saboteurImpact = new ImplicationSaboteur[2];

            if (_round == 1)
            {
                _detailed += "La bataille est sur point de commencer, les forces sont placées et les dés sont jetés,\n";
                _detailed += "Chaque groupe est prêt à une stratégie, l'issue se résout ce soir.\n";

                _detailed += "\nLa Bataille de " + _battleName + " commence!!";

                for (int i = 0; i < 2; i++)
                {
                    _detailed += "D'un côté, nous avons le groupe " + _sides[i] + "\n";
                    _detailed += "Les généraux sont nul autre que ";
                    for (int j = 0; j < _general[i].Count; j++)
                    {
                        _detailed += _general[i][j].Name + "; ";
                    }
                    _detailed += "\n\n";
                }

                _detailed += "Que le combat commence!\n\n\n";
            }

        }

        /// <summary>
        /// Phase 1 of the skimish.
        /// Calculate the implication of each generals and the fields.
        /// </summary>
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
            
            _technical += "Escarmouche " + _round + " dans la bataille de " + _battleName + ": \n";
            _technical += "Implication des généraux dans la bataille: \n";
            _detailed += "-------------------\n" + "Les généraux chargent!!\n";

            for (int i =0; i < SIDE; i++)
            {
                _technical += "Côté " + _sides[i] + "\n";
                _imp[i] = new List<Implication>();
                int id = 0;
                foreach (General g in _general[i])
                {
                    Implication imp = g.GetImplication();
                    _technical += g.Name + ": ";
                    if (imp == null)
                    {
                        _technical += "Saboteur\n";
                    }
                    else
                    {
                        imp.Id = id;
                        _imp[i].Add(imp);
                        _technical += imp.ToTechnicalString() + "\n";
                    }
                    id++;

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
                _technical += "donne un total de: " + _force[i] + " sur un maximum de " + _totalForce[i] + " (" + _totalForceBonus[i] + " avec bonus)\n";
                _technical += "inégalités: " + _inegatilies[i] + " et fatigue: " + Config.EnumToString(_fat[i]) + "\n";
                _force[i] = (int)(_force[i] * (1 + _inegatilies[i] - Config.GetFatiguePower(_fat[i])));
                _technical += "donne finalement: " + _force[i] + " avec la balance d'inégalité et la fatigue\n\n";
            }
            
        }

        /// <summary>
        /// Phase 2 of the skirmish
        /// Calculate the AddOn implication.
        /// First step if the base one.
        /// Second step is a counter and a counter's counter if possible.
        /// Please note that add-on can be negative, you cannot control them.
        /// </summary>
        public void Phase2()
        {
            // Phase 2
            // Add-on essentials
            // Normal
            _technical += "Implication des aide extérieures OBLIGATOIRE dans la bataille: \n";
            
            for (int i=0; i<SIDE; i++)
            {
                bool hasOne = false;
                _technical += "Côté " + _sides[i] + "\n";
                
                if (_addon[i] != null)
                    foreach (AddOn a in _addon[i])
                    {
                        if (a.IsOrder() && a.IsOrderWhen(_time))
                        {
                            int imp = a.GetCombatImplication(_totalForce[i]);
                            _add[i] += imp;
                            _technical += a.Name + ": " + imp + "\n";
                            if (imp > 0)
                            {
                                if (!hasOne)
                                {
                                    _detailed += "Les tactiques " + _sides[i] + " commencent!\n";
                                    _detailed += "Du côté " + _sides[i] + ", nous avons: ";
                                    hasOne = true;
                                }
                                _detailed += a.Name + "; ";
                            }
                        }
                    }
                _detailed += "\n";
            }
            

            _technical += "Résultat de l'escarmouche sans ajout: \n";

            for (int i = 0; i < SIDE; i++)
            {
                _totalForceBonus[i] = _force[i] + _add[i];
                _technical += "Côté " + _sides[i] + ": " + _totalForceBonus[i] + "\n";
            }

            // Phase 2 continues, add another round of reéquilibration
            int[] order = { 0, 1 };

            _detailed += Flavor.Fighting();

            // Normally, if 1 is greater than 0, 1 can have another shot.
            // If 0 is greater than 1, than 1 has the rights.
            if (_totalForceBonus[0] > _totalForceBonus[1])
            {
                _detailed += "L'armée " + _sides[1] + " est en mauvaise posture...\n";
                order[0] = 1;
                order[1] = 0;
                
            }
            else if(_totalForceBonus[0] == _totalForceBonus[1])
            {
                _detailed += "La situation est tendue, chaque côté maintient son bout de terrain!\n";
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
            else
            {
                _detailed += "L'armée " + _sides[0] + " est en mauvaise posture...\n";
            }
            
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
                        _detailed += "Mais attention! Le groupe " + _sides[order[0]] + " contre attaque avec " + a.Name + "\n";
                    }
                }
                _detailed += "Est-ce que ce sera suffisant?\n";
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
                            _detailed += "Attention, le groupe " + _sides[order[1]] + " n'en a pas fini! Une contre attaque avec " + a.Name + "\n";
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
            _technical += "\n";
        }

        /// <summary>
        /// Phase 3 of the skirmish.
        /// Calculate the implication of the Saboteur
        /// At this stage, the battle is decided according to strenght, but the applicaiton of it can be tweak a little bit.
        /// </summary>
        public void Phase3()
        {
            //_totalForceBonus base on this
            // Calculate Saboteur implication
            // If possible, add necessary (Balance equation of failing)
            // Delete army - % units
            _technical += "Intervention des saboteurs\n";

            // Phase 2 continues, add another round of reéquilibration
            int[] order = { 0, 1 };
            // Normally, if 1 is greater than 0, 1 can have another shot.
            // If 0 is greater than 1, than 1 has the rights.
            if (_totalForceBonus[0] > _totalForceBonus[1])
            {
                order[0] = 1;
                order[1] = 0;
                _technical += "Côté " + _sides[1] + " est l'actuelle perdant.\n";
            }
            else if (_totalForceBonus[0] == _totalForceBonus[1])
            {
                _technical += "Bataille égale, pas besoin de Saboteur.\n";
                return;
            }
            else
            {
                _technical += "Côté " + _sides[0] + " est l'actuelle perdant.\n";
            }
            


            // If order 0 is worst, check if Saboteur available, use only one.
            General saboteur = null;
            foreach(General g in _general[order[1]])
            {
                if (g.Saboteur && g.IsValid())
                {
                    saboteur = g;
                    break;
                }
            }
            if (saboteur != null)
            {
                ImplicationSaboteur imp = saboteur.GetImplicationSaboteur();
                _saboteurImpact[order[0]] = imp;
                _technical += "Saboteur " + saboteur.Name + ": " + _saboteurImpact[order[0]].ToTechnicalString() + " est diponible.\n";
                int delta = _totalForceBonus[order[1]] - _totalForceBonus[order[0]];
                if (delta > imp.ArmyBonus)
                {
                    _technical += "Le saboteur ne peut rien y faire, il reste tranquille.\n";
                }
                else
                {
                    _totalForceBonus[order[0]] += delta * 2;
                    Console.WriteLine(delta + " and " + saboteur.NbArmy);
                    saboteur.ReduceArmy(imp.ReduceArmy(delta));
                    Console.WriteLine(saboteur.NbArmy);
                    _technical += "Le saboteur " + saboteur.Name + " a changé la balance, il lui reste " + saboteur.NbArmy + " armées.";
                    
                    // Can get busted
                    if (WarMath.ResultChance(saboteur.Stat.ShowOff))
                    {
                        _technical += "Nous savons qu'il s'agit de " + saboteur.Name + "\n";
                        _detailed += "Un revirement de situation vient d'avoir lieu!\n";
                        _detailed += "On aurait juré voir " + saboteur.Name + " nuire à son groupe, c'est un saboteur!\n";
                    }
                    else
                    {
                        _technical += "Le saboteur reste dans l'anonimat.\n";
                    }
                    
                }

                _technical += "Résultat de l'escarmouche avec saboteur: \n";

                for (int i = 0; i < SIDE; i++)
                {
                    _technical += "Côté " + _sides[i] + ": " + _totalForceBonus[i] + "\n";
                }
            }
            else
            {
                _technical += "Même résultat de l'escarmouche car aucun saboteur: \n";

                for (int i = 0; i < SIDE; i++)
                {
                    _technical += "Côté " + _sides[i] + ": " + _totalForceBonus[i] + "\n";
                }
            }
            _technical += "\n";
        }

        /// <summary>
        /// Phase 4 of the skirmish
        /// Calculate the General implication
        /// </summary>
        public void Phase4()
        {
            _detailed += "Les dés sont jetés pour cette " + _round + " escarmouche.\n";
            _technical += "Les généraux qui se sont démarqués pendant l'escarmouche: \n";
            for (int i=0; i<SIDE; i++)
            {
                _technical += "Côté " + _sides[i] + "\n";
                _detailed += "Du côté " + _sides[i] + ", les héros de ce combat sont ";
                _imp[i].Sort();
                _technical += _general[i][_imp[i][0].Id].ToString() + "\n";
                _generalBest[i].Add(_general[i][_imp[i][0].Id]);
                _detailed += _general[i][_imp[i][0].Id].Name;
                if (_imp[i].Count > 1 && _imp[i][1] != null)
                {
                    _technical += _general[i][_imp[i][1].Id].ToString() + "\n";
                    _generalBest[i].Add(_general[i][_imp[i][1].Id]);
                    _detailed += " et " + _general[i][_imp[i][1].Id].Name;
                }
                _detailed += "\n";
                
            }
            _technical += "\n";
            _detailed += "\n";
        }

        /// <summary>
        /// Phase 5 of the skirmish
        /// Calculate the lost if any (building fortification or general may be affected)
        /// </summary>
        public void Phase5()
        {
            int l = 0;
            int w = 1;
            int lost = Math.Abs(_totalForceBonus[0] - _totalForceBonus[1]);
            // Get losing side
            if (_totalForceBonus[0] == _totalForceBonus[1])
            {
                _technical += "Rien de perdu, match égale.\n\n";
                _detailed += "Aucune armée n'a laissé un pouce de terrain!\n\n";
                return;
            }
            else if(_totalForceBonus[0] < _totalForceBonus[1])
            {
                l = 0;
                w = 1;
            }
            else
            {
                l = 1;
                w = 0;
            }

            int attackLost = (int)(Math.Min(_generalBest[l][0].NbArmy, lost) * 0.5f);
            _technical += "L'armée " + _generalBest[w][0].Name + " a réussi la percée.\n";
            _detailed += "Le responsable de la percée est nul autre que " + _generalBest[w][0].Name + "!\n";
            

            // Check the losing general OR fortification
            _technical += "Perdant: " + _sides[l] + "\n";
            _detailed += "Le groupe " + _sides[l] + " sembre perdre cette escarmouche.\n";


            // Losing can only be highlight general and fortication!
            // Fortification level + Defense level
            bool isOnFor = WarMath.ResultChance(Config.GetForticationPower(_for[l]) + _generalBest[l][0].Stat.Defense);
            if (isOnFor && _for[l] != 0)
            {
                _technical += "Les défenses absorbe les dégats\n";
                _detailed += "Heureusement, les défenses misent en place assure un repli efficace\n";
                int thresh = (int) (_generalBest[l][0].NbArmy * Config.GetForticationPower(_for[l]));
                if (lost > thresh)
                {
                    attackLost = (int)(attackLost * 0.7f);
                    _for[l]--;
                    _technical += "Et elle brise, c'est maintenant équivalent à: " + Config.EnumToString(_for[l]) + "\n";
                    _detailed += "...et les fortications actuelles prennent de lourdes dégâts.\n";
                }
                else
                {
                    attackLost = (int)(attackLost*0.5f);
                    _technical += "Les défenses tiennent le coup.\n";
                }
            }
            else
            {
                //Add-On available
                if (_addon[l] != null)
                {
                    foreach (AddOn a in _addon[l])
                    {
                        if (a.CanUseDefense())
                        {
                            if (a.Stat.Defense < 0)
                            {
                                _detailed += "Hô non, le " + a.Name + " est un piège, les pertes sont encore plus lourdes..\n";
                                _technical += "Défense externe " + a.Name + " est un piège.\n";
                                int extraLost = (int)((float)lost * -a.Stat.Defense);
                                lost += extraLost;
                                _technical += "Perte additionnel: " + extraLost + "\n";
                                attackLost = (int)(attackLost * 0.5f);
                            }
                            else {
                                bool block = WarMath.ResultChance(a.Stat.Defense);
                                if (block)
                                {
                                    lost = 0;
                                    _detailed += "Heureusement, " + a.Name + " permet de faire un repli efficace en dernier recours\n";
                                    _technical += "Défense externe " + a.Name + " prend l'assault.\n";
                                }
                                else
                                {
                                    _technical += "Défense externe " + a.Name + " n'est pas assez fort pour bloquer l'assault.\n";
                                    attackLost = (int)(attackLost * 0.5f);
                                }
                            }
                            a.UsedAddOn();
                            break;
                        }
                    }
                }

                //Generals take the blow
                if (lost > 0)
                {
                    int leftOver = _generalBest[l][0].LosingArmy(lost);
                    _technical += "Le général " + _generalBest[l][0].Name + " prend le coup.\n";
                    _technical += _generalBest[l][0].ToDetailedHit();
                    _detailed += "Le général " + _generalBest[l][0].Name + " a prit le plus gros de l'assault.\n";
                    _detailed += _generalBest[l][0].ToDetailedHit();


                    if (leftOver > 0 && _generalBest[l].Count > 1)
                    {
                        _technical += "En plus, le général " + _generalBest[l][1].Name + " prend le coup.\n";
                        _generalBest[l][1].LosingArmy(leftOver);
                        _technical += _generalBest[l][1].ToDetailedHit();
                        _detailed += "Le coup fut tellement puissant que le général " + _generalBest[l][1].Name + " a aussi encaissé.\n";
                        _detailed += _generalBest[l][1].ToDetailedHit();
                    }
                }
            }

            _generalBest[w][0].LosingArmy((int)(attackLost));
            _technical += _generalBest[w][0].ToDetailedHit();

            _technical += "\n";
            _detailed += "\n";
        }

        /// <summary>
        /// Phase 6 of the skirmish
        /// Hostage phase
        /// </summary>
        public void Phase6()
        {
            int l;
            int w;
            if (_totalForceBonus[0] == _totalForceBonus[1])
            {
                return;
            }
            else if (_totalForceBonus[0] < _totalForceBonus[1])
            {
                l = 0;
                w = 1;
            }
            else
            {
                l = 1;
                w = 0;
            }
            
            // Step 1, Add-On usable to boost Moral
            //Add-On available
            if (_addon[l] != null)
            {
                foreach (AddOn a in _addon[l])
                {
                    Console.WriteLine("Can use it? " + a.CanUseMorale());
                    if (a.CanUseMorale())
                    {
                        bool useit = false;
                        int i = 0;
                        Console.WriteLine("Recover 0 " + _generalBest[l][0].IsRecoverable());
                        if (_generalBest[l][0].IsRecoverable())
                        {
                            useit = true;
                            i = 0;
                           
                        }
                        else if(_generalBest[l].Count > 1 && _generalBest[l][0].IsRecoverable())
                        {
                            useit = true;
                            i = 1;
                        }
                        if (useit)
                        {
                            bool revive = _generalBest[l][i].IsRevive(a.Stat.Moral);
                            if (revive)
                            {
                                _technical += "L'aide " + a.Name + " donne un boost de morale à " + _generalBest[l][i].Name + ", le groupe revient sur le champ de bataille.\n";
                                _detailed += "Incroyable! " + "L'aide" + a.Name + " donne un boost de morale à " + _generalBest[l][i].Name + ", le groupe revient sur le champ de bataille.\n";
                            }
                            else
                            {
                                _technical += "L'aide " + a.Name + " n'est pas suffisante pour le morale.\n";
                            }
                            a.UsedAddOn();
                            break;
                        }
                    }
                }
            }

            // Step 2, Decision making is defeated but by moral.
            bool[] moraleAction = { false, false };
            if (_generalBest[l][0].CanHostage())
            {
                moraleAction[0] = true;
            }
            if (_generalBest[l].Count > 1 && _generalBest[l][0].CanHostage())
            {
                moraleAction[1] = true;
            }
            if (moraleAction[0] || moraleAction[1])
            {
                _technical += "Il y a des otages.\n";
                _detailed += "Hô non, il y a des otages!";

                int massacre = 0;
                int mercy = 0;
                int hostage = 0;
                foreach (General g in _general[w])
                {
                    if (g.IsValid())
                    {
                        int nego = WarMath.ResultPower(100 + (int)(100 * g.Stat.NegoPower), true);
                        switch (g.Stat.Behaviour)
                        {
                            case Config.EndBehaviour.None:
                            case Config.EndBehaviour.Mercy:
                                mercy += nego;
                                break;
                            case Config.EndBehaviour.Carnage:
                                massacre += nego;
                                break;
                            case Config.EndBehaviour.Hostage:
                                hostage += nego;
                                break;
                        }
                    }
                }
                _technical += "Décision sur les fuyards: \n";
                _technical += Config.EnumToString(Config.EndBehaviour.Mercy) + ": " + mercy + "\n";
                _technical += Config.EnumToString(Config.EndBehaviour.Carnage) + ": " + massacre + "\n";
                _technical += Config.EnumToString(Config.EndBehaviour.Hostage) + ": " + hostage + "\n";

                // Sorting
                Config.EndBehaviour decision;
                if (mercy > massacre)
                {
                    if (hostage > mercy)
                    {
                        decision = Config.EndBehaviour.Hostage;
                    }
                    else
                    {
                        decision = Config.EndBehaviour.Mercy;
                    }
                }
                else
                {
                    if (hostage > massacre)
                    {
                        decision = Config.EndBehaviour.Hostage;
                    }
                    else
                    {
                        decision = Config.EndBehaviour.Carnage;
                    }
                }
                _technical += "Décision final: " + Config.EnumToString(decision);
                _detailed += "Le conseil de guerre a décidé, la décision sur les otages est: " + Config.EnumToString(decision);

                for (int i = 0; i < 2; i++)
                {
                    // Impact on both general
                    if (moraleAction[i])
                    {
                        if (decision == Config.EndBehaviour.Carnage && decision == Config.EndBehaviour.Hostage)
                        {
                            // Last resort! Check on aid to help escape!
                            if (_addon[l] != null)
                            {
                                foreach (AddOn a in _addon[l])
                                {
                                    if (a.CanUseEscape())
                                    {
                                        bool escape = WarMath.ResultChance(a.Stat.Escape);
                                        if (escape)
                                        {
                                            _technical += "Le général " + _generalBest[l][i].Name + " s'enfuit grâce à " + a.Name + "\n";
                                            _detailed += "Le général " + _generalBest[l][i].Name + " utilise son dernier atout, " + a.Name + ", et s'enfuit.\n";
                                            moraleAction[i] = false;
                                            continue;
                                        }
                                        else
                                        {
                                            _technical += "Aide de " + a.Name + " insuffisante pour " + _generalBest[l][i].Name + "\n";

                                        }
                                    }
                                }
                            }

                            _technical += "Le groupe du général " + _generalBest[l][i].Name + "\n";
                            _detailed += "Le groupe du général " + _generalBest[l][i].Name + "\n";

                            if (decision == Config.EndBehaviour.Hostage)
                            {
                                _technical += " est prit en otage\n";
                                _detailed += " est prit en otage\n";
                            }
                            if (decision == Config.EndBehaviour.Carnage)
                            {
                                _technical += " se fait massacrer\n";
                                _detailed += " se fait massacrer\n";
                            }
                            if (_generalBest[l][i].GeneralDead)
                            {
                                _technical += "Le général " + _generalBest[l][i].Name + " est";
                                _detailed += "Le général " + _generalBest[l][i].Name + " est";
                            }
                            if (decision == Config.EndBehaviour.Hostage)
                            {
                                _technical += " prit en otage\n";
                                _detailed += " prit en otage\n";
                            }
                            if (decision == Config.EndBehaviour.Carnage)
                            {
                                _technical += " massacré\n";
                                _detailed += " massacré\n";
                            }

                        }
                        else
                        {
                            _technical += "\nLes fuyards ont quitter le champ de bataille.";
                            _detailed += "\nLes fuyards ont quitter le champ de bataille.";
                        }
                        // Mercy and nothing is normal
                    }
                }
            }
            
            //Delete defeated general
            for (int j = 0; j < SIDE; j++)
            {
                for (int i = _general[j].Count - 1; i >= 0; i--)
                {
                    if (_general[j][i].Defeated)
                    {
                        _general[j].RemoveAt(i);
                    }
                }
            }

            //Checkout the battle output
            int adverse = 1;
            for(int i=0; i<SIDE; i++)
            {
                bool sideLose = true;
                foreach (General g in _general[i])
                {
                    if (!g.Saboteur)
                    {
                        sideLose = false;
                        break;
                    }
                }
                if (sideLose)
                {
                    _technical += "\n\nLa bataille est fini " + _sides[i] + " est perdant.\n";
                    _technical += _sides[adverse] + " a gagné la bataille de " + _battleName;
                    _detailed += "La bataille est fini! " + _sides[i] + " a perdu la bataille de " + _battleName + "\n";
                    _detailed += _sides[adverse] + " est victorieux!!\n";

                    Final = true;
                    return;
                }
                adverse = 0;
            }
        }

        public void UpdateStatus(out Config.Fortification for1, out Config.Fortification for2)
        {
            for1 = _for[0];
            for2 = _for[1];
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
