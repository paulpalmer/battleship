using BattleShipStateTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Interfaces
{
    public interface IGameStateManager
    {
        void PlayGame(Gameboard gameboard);

    }
}
