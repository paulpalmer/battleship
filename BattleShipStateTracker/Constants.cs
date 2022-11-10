using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker
{
    internal class Constants
    {
        public const string HitMessage = "That was a hit!";
        public const string MissMessage = "That was a miss.";
        public const string YouLost = "All your ships have been sunk.";
        public const string StillAlive = "You are still alive";
        public const string InvalidCoordinateValue = "Coordinate must be an integer.";


        public const int BoardSizeX = 10;
        public const int BoardSizeY = 10;
    }
}
