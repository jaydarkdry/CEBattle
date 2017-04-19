using System;

public class Player
{
    public string name;
    private int _hp;
    private int _mp;
    private string[] _equips;

	public Player(string name, int hp, int mp, string[] equips)
	{
        this.name = name;
        _hp = hp;
        _mp = mp;
        _equips = equips;
    }

    /// <summary>
    /// Show the stat, to you know, the shit,....
    /// </summary>
    public void ShowStatCmd()
    {
        Console.WriteLine("Name: " + name);
        Console.WriteLine("HP: " + _hp);
        Console.WriteLine("MP: " + _mp);
        if (_equips == null)
        {
            return;
        }
        string txt = "Equips: (";
        for (int i=0; i<_equips.Length; i++)
        {
            txt += _equips[i];
            if (i != (_equips.Length - 1))
            {
                txt += ", ";
            }
        }
        txt += ")";
        Console.WriteLine(txt);
    }

    public void Attack(Player p, int hit)
    {
        if(p.Perte(hit))
        {
            Console.WriteLine("Me " + name + " declared " + p.name + " dead, hahahahahah");
        }
    }

    /// <summary>
    /// Perte de degat
    /// </summary>
    /// <param name="hit">The hit hp</param>
    /// <returns>If he's dead, yo</returns>
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
