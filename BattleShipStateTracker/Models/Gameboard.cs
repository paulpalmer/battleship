using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Models
{
    public class Gameboard
    {
        public Gameboard()
        {
            Battleships = new List<Battleship>();
        }

        public IList<Battleship> Battleships { get; set; }
    }
}
