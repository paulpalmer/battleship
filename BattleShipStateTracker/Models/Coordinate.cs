using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Models
{
    public class Coordinate
    {
        public Coordinate(int xAxis, int yAxis)
        {
            XAxis = xAxis;
            YAxis = yAxis;
        }

        public int XAxis { get; set; }
        public int YAxis { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as Coordinate;
            return other != null && other.XAxis == XAxis && other.YAxis == YAxis;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(XAxis, YAxis);
        }


        public override string ToString()
        {
            return $"X{XAxis} Y{YAxis}";
        }
    }
}
