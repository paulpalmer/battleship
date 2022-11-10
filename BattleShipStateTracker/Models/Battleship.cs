using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Models
{
    public class Battleship
    {
        public Battleship(IEnumerable<Coordinate> coordinates)
        {
            Coordinates = new Dictionary<Coordinate, bool>();
            foreach(var coordinate in coordinates)
            {
                Coordinates.Add(coordinate, false);
            }
        }


        /// <summary>
        /// Key is a 2 part coordinate like A7
        /// Value records whether coordinate has been hit
        /// </summary>
        public Dictionary<Coordinate, bool> Coordinates { get; set; }

        public bool IsAlive()
        {
            return Coordinates.Any(x => x.Value == false);
        }

        
    }
}
