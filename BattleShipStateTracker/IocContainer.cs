using BattleShipStateTracker.Interfaces;
using BattleShipStateTracker.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipStateTracker
{

    internal class IocContainer
    {
        private IConsoleManager _consoleManager;
        private ISetupManager _setupManager;
        private IGameStateManager _gameStateManager;
        private IProgramManager _programManager;

        public IConsoleManager GetConsoleManager()
        {
            if (_consoleManager == null)
            {
                _consoleManager = new ConsoleManager();
            }
            return _consoleManager;
        }

        public ISetupManager GetSetupManager()
        {
            if (_setupManager == null)
            {
                _setupManager = new SetupManager(GetConsoleManager());
            }
            return _setupManager;
        }

        public IGameStateManager GetGameStateManager()
        {
            if (_gameStateManager == null)
            {
                _gameStateManager = new GameStateManager(GetConsoleManager());
            }
            return _gameStateManager;
        }

        public IProgramManager GetProgramManager()
        {
            if (_programManager == null)
            {
                _programManager = new ProgramManager(GetConsoleManager(), GetSetupManager(), GetGameStateManager());
            }
            return _programManager;
        }

    }
}
