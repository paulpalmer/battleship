using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Interfaces
{
    public interface IConsoleManager
    {
        void WriteLine(string message);
        string ReadInput();
    }
}
