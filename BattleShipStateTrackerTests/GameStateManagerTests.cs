using BattleShipStateTracker;
using BattleShipStateTracker.Interfaces;
using BattleShipStateTracker.Managers;
using BattleShipStateTracker.Models;
using Moq;
using Xunit;

namespace BattleShipStateTrackerTests
{
    public class GameStateManagerTests
    {

        public GameStateManagerTests()
        {

        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, true)]
        [InlineData(2, 1, false)]
        [InlineData(1, 3, false)]
        public void ProcessAttack_WhenAttackHitsBattleship_ReportsAHitOrMiss(int x, int y, bool shouldReportHit)
        {
            var gameboard = new Gameboard()
            {
                Battleships = new List<Battleship>{
                    CreateBattleship(new List<Coordinate> { new Coordinate(1, 1), new Coordinate(1, 2) })
                }
            };

            var mockConsoleManager = new Mock<IConsoleManager>();
            
            var gameStateManager = new GameStateManager(mockConsoleManager.Object);
            gameStateManager.ProcessAttack(gameboard, new Coordinate(x,y));


            mockConsoleManager.Verify(x => x.WriteLine(Constants.HitMessage), shouldReportHit ? Times.Once() : Times.Never());

            mockConsoleManager.Verify(x => x.WriteLine(Constants.MissMessage), shouldReportHit ? Times.Never() : Times.Once());


        }

        [Fact]
        public void PlayGame_KeepsAcceptingAttacksUntilAllBattleshipsAreSunk()
        {
            var gameboard = new Gameboard()
            {
                Battleships = new List<Battleship>{
                    CreateBattleship(new List<Coordinate> { new Coordinate(1, 1), new Coordinate(1, 2) }),
                    CreateBattleship(new List<Coordinate> { new Coordinate(5, 6), new Coordinate(6, 6),  new Coordinate(7, 6) })
                }
            };

            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("1")
                .Returns("1")
                .Returns("1")
                .Returns("2")
                .Returns("5")
                .Returns("6")
                .Returns("6")
                .Returns("6")
                .Returns("7")
                .Returns("6");


            var gameStateManager = new GameStateManager(mockConsoleManager.Object);
            gameStateManager.PlayGame(gameboard);

            mockConsoleManager.Verify(x => x.WriteLine(Constants.HitMessage), Times.Exactly(5));
            mockConsoleManager.Verify(x => x.WriteLine(Constants.StillAlive), Times.Exactly(4));
            mockConsoleManager.Verify(x => x.WriteLine(Constants.YouLost), Times.Once());


        }

        [Fact]
        public void PlayGame_HandlesBadInput()
        {
            var gameboard = new Gameboard()
            {
                Battleships = new List<Battleship>{
                    CreateBattleship(new List<Coordinate> { new Coordinate(1, 1)})
                }
            };

            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("Not an integer")
                .Returns("1")
                .Returns("1");


            var gameStateManager = new GameStateManager(mockConsoleManager.Object);
            gameStateManager.PlayGame(gameboard);

            mockConsoleManager.Verify(x => x.WriteLine(Constants.InvalidCoordinateValue), Times.Exactly(1));
            mockConsoleManager.Verify(x => x.WriteLine(Constants.StillAlive), Times.Never());
            mockConsoleManager.Verify(x => x.WriteLine(Constants.YouLost), Times.Once());


        }


        private Battleship CreateBattleship(IEnumerable<Coordinate> coordinates)
        {
            return new Battleship(coordinates);
        }

        
    }
}