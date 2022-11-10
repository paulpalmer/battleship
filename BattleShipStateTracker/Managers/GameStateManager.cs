using BattleShipStateTracker.Interfaces;
using BattleShipStateTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BattleShipStateTrackerTests")]
namespace BattleShipStateTracker.Managers
{
    public class GameStateManager : IGameStateManager
    {
        private readonly IConsoleManager _consoleManager;

        public GameStateManager(IConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
        }

        public void PlayGame(Gameboard gameboard)
        {
            var gameInProgress = true;

            while (gameInProgress) {
                try
                {
                    var xCoordinate = GetCoordinate("X");
                    var yCooridnate = GetCoordinate("Y");

                    var coordinate = new Coordinate(xCoordinate, yCooridnate);

                    ProcessAttack(gameboard, coordinate);

                    if (gameboard.Battleships.Any(x => x.IsAlive()))
                    {
                        _consoleManager.WriteLine(Constants.StillAlive);
                    }
                    else
                    {
                        gameInProgress = false;
                        _consoleManager.WriteLine(Constants.YouLost);
                    }
                }
                catch(Exception ex)
                {
                    _consoleManager.WriteLine(ex.Message);
                }
            }

        }

        internal void ProcessAttack(Gameboard gameboard, Coordinate coordinate)
        {
            var battleship = gameboard.Battleships.SingleOrDefault(x => x.Coordinates.ContainsKey(coordinate));
            if (battleship != null && battleship.Coordinates.GetValueOrDefault(coordinate) == false)
            {
                _consoleManager.WriteLine(Constants.HitMessage);
                battleship.Coordinates[coordinate] = true;
            }
            else
            {
                _consoleManager.WriteLine(Constants.MissMessage);
            }

        }

        private int GetCoordinate(string coordinateName)
        {
            _consoleManager.WriteLine($"Enter the {coordinateName} coordinate of the attack");

            var input = _consoleManager.ReadInput();
            if (int.TryParse(input, out var coordinateValue))
            {
                return coordinateValue;
            }
            else
            {
                throw new ArgumentException(Constants.InvalidCoordinateValue);
            }
        }

    }
}
