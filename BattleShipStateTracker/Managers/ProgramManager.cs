using BattleShipStateTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker.Managers
{
    internal class ProgramManager : IProgramManager
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ISetupManager _setupManager;
        private readonly IGameStateManager _gameStateManager;
        public ProgramManager(IConsoleManager consoleManager, ISetupManager setupManager, IGameStateManager gameStateManager)
        {
            _consoleManager = consoleManager;
            _setupManager = setupManager;
            _gameStateManager = gameStateManager;
        }

        public void Run()
        {
            //Instructions
            _consoleManager.WriteLine("Battleship State Tracker");


            //Setup            
            var gameboard = _setupManager.SetupGameboard();

            //Game loop
            _gameStateManager.PlayGame(gameboard);



        }
    }
}
