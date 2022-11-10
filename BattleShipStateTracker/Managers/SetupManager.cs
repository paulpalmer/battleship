using BattleShipStateTracker.Interfaces;
using BattleShipStateTracker.Models;

namespace BattleShipStateTracker.Managers
{
    internal class SetupManager : ISetupManager
    {
        private readonly IConsoleManager _consoleManager;

        public SetupManager(IConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
        }

        public Gameboard SetupGameboard()
        {
            var gameboard = new Gameboard();
            while (true)
            {
                try
                {
                    _consoleManager.WriteLine("[1] To add a battleship");
                    _consoleManager.WriteLine("[2] To reset the gameboard");
                    _consoleManager.WriteLine("[3] To finish setup");

                    switch (_consoleManager.ReadInput())
                    {
                        case ("1"):
                            AddBattleship(gameboard);
                            break;
                        case ("2"):
                            gameboard.Battleships.Clear();
                            break;
                        case ("3"):
                            return gameboard;
                        default:
                            _consoleManager.WriteLine("Invalid selection please try again");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    _consoleManager.WriteLine(ex.Message);
                }
            }
        }

        internal void AddBattleship(Gameboard gameboard)
        {
            var xStartCoordinate = GetCoordinate("X", true);
            var yStartCoordinate = GetCoordinate("Y", true);

            var xEndCoordinate = GetCoordinate("X", false);
            var yEndCoordinate = GetCoordinate("Y", false);

            var coordinates = CalculateBattleshipCoordinates(new Coordinate(xStartCoordinate, yStartCoordinate), new Coordinate(xEndCoordinate, yEndCoordinate));

            ValidateBattlehipPosition(gameboard, coordinates);

            var battleship = new Battleship(coordinates);

            gameboard.Battleships.Add(battleship);



        }

        private int GetCoordinate(string coordinateName, bool isStartCoordinate)
        {
            _consoleManager.WriteLine($"Enter the {coordinateName} coordinate of the {(isStartCoordinate?"start":"end")} of the ship.");
            var boardSize = (coordinateName == "X" ? Constants.BoardSizeX : Constants.BoardSizeY);
            if (int.TryParse(_consoleManager.ReadInput(), out var coordinateValue) && coordinateValue > 0 && coordinateValue <= boardSize)
            {
                return coordinateValue;
            }
            else
            {
                throw new ArgumentException($"Coordinates must be an integer between 1 and {boardSize}");
            }
        }

        private IEnumerable<Coordinate> CalculateBattleshipCoordinates(Coordinate startCoordinate, Coordinate endCoordinate)
        {
            if (startCoordinate == null)
            {
                throw new ArgumentNullException(nameof(startCoordinate));
            }
            if (endCoordinate == null)
            {
                throw new ArgumentNullException(nameof(endCoordinate));
            }

            bool isVertical = startCoordinate.XAxis == endCoordinate.XAxis;
            bool isHorizontal = startCoordinate.YAxis == endCoordinate.YAxis;

            if (!isVertical && !isHorizontal)
            {
                throw new ArgumentException("Start and End coordinates must share either an XAxis or YAxis");
            }

            var coordinates = new List<Coordinate>();
            if (isHorizontal)
            {
                for (int i = Math.Min(startCoordinate.XAxis, endCoordinate.XAxis); i <= Math.Max(startCoordinate.XAxis, endCoordinate.XAxis); i++)
                {

                    coordinates.Add(new Coordinate(i, startCoordinate.YAxis));
                }
            }
            else
            {
                for (int i = Math.Min(startCoordinate.YAxis, endCoordinate.YAxis); i <= Math.Max(startCoordinate.YAxis, endCoordinate.YAxis); i++)
                {

                    coordinates.Add(new Coordinate(startCoordinate.XAxis, i));
                }
            }
            return coordinates;

        }

        private void ValidateBattlehipPosition(Gameboard gameboard, IEnumerable<Coordinate> coordinates)
        {
            if (gameboard.Battleships.Any(b => b.Coordinates.Keys.Intersect(coordinates).Any()))
            {
                throw new ArgumentException("New battleship coordinates overlap an existing battleship");
            }
        }

        
    }
}
