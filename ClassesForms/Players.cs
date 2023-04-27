using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    internal class Players
    {
        private List<Player> _players;
        public int count { get { return _players.Count; } }
        public Players()
        {
            _players = new List<Player>();
        }
        public void Add(Player p) 
        {
            _players.Add(p);
        }
        public void Remove(Player p) 
        {
            _players.Remove(p);
        }

    }

    internal class Player
    {
        private string _name;
        private int _score;
        private Bonuses _bonuses;
        private PlayerTile title;

        public Player(string name)
        {
            _name = name;
            _bonuses = new Bonuses();
        }
    }
}
