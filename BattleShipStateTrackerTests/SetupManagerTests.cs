using BattleShipStateTracker.Interfaces;
using BattleShipStateTracker.Managers;
using BattleShipStateTracker.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BattleShipStateTrackerTests
{
    public class SetupManagerTests
    {
        [Theory]
        [InlineData("1", "1", "1", "5", 5)]
        [InlineData("1", "1", "5", "1", 5)]
        [InlineData("1", "6", "1", "1", 6)]
        [InlineData("6", "1", "1", "1", 6)]
        public void AddBattleship_CoordinatesCanBeVerticalOrHorizontal(string startX, string startY, string endX, string endY, int expectedShipSize)
        {
            var gameboard = new Gameboard();


            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns(startX)
                .Returns(startY)
                .Returns(endX)
                .Returns(endY);


            var setupManager = new SetupManager(mockConsoleManager.Object);

            setupManager.AddBattleship(gameboard);



            Assert.Equal(expectedShipSize, gameboard.Battleships.Single().Coordinates.Count);
        }

        [Fact]
        public void AddBattleship_ThrowsExceptionIfShipIsNotVerticalOrHorizontal()
        {
            var gameboard = new Gameboard();


            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("1")
                .Returns("1")
                .Returns("2")
                .Returns("2");


            var setupManager = new SetupManager(mockConsoleManager.Object);

            Assert.Throws<ArgumentException>(()  => setupManager.AddBattleship(gameboard));



        }

        [Fact]
        public void AddBattleship_ShipsCannotOverlapEachOther()
        {
            var gameboard = new Gameboard();


            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("1")
                .Returns("1")
                .Returns("1")
                .Returns("2")
                .Returns("1")
                .Returns("2")
                .Returns("1")
                .Returns("3");


            var setupManager = new SetupManager(mockConsoleManager.Object);

            setupManager.AddBattleship(gameboard);

            Assert.Throws<ArgumentException>(() => setupManager.AddBattleship(gameboard));
        }

        [Fact]
        public void AddBattleship_ShipsMustFitWithinGameboard()
        {
            var gameboard = new Gameboard();


            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("1")
                .Returns("1")
                .Returns("1")
                .Returns("11");


            var setupManager = new SetupManager(mockConsoleManager.Object);

            Assert.Throws<ArgumentException>(() => setupManager.AddBattleship(gameboard));
        }


        [Fact]
        public void SetupGameboard_CanAddMultipleShips()
        {

            var mockConsoleManager = new Mock<IConsoleManager>();

            mockConsoleManager.SetupSequence(x => x.ReadInput())
                .Returns("1") //Add first ship
                .Returns("1") //StartX coordinate
                .Returns("1") //StartY coordinate
                .Returns("1") //EndX coordinate
                .Returns("2") //EndY coordinate
                .Returns("1") //Add second ship
                .Returns("1") //StartX coordinate
                .Returns("3") //StartY coordinate
                .Returns("1") //EndX coordinate
                .Returns("4") //EndY coordinate
                .Returns("3"); //Finish setup


            var setupManager = new SetupManager(mockConsoleManager.Object);

            var gameboard = setupManager.SetupGameboard();

            Assert.Equal(2, gameboard.Battleships.Count());
        }
    }
}
