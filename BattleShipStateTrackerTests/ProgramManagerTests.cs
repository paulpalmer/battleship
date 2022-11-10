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
    public class ProgramManagerTests
    {
        [Fact]
        public void Run_SetsUpGameboardBeforeStartingGame()
        {
            var mockConsoleManager = new Mock<IConsoleManager>();
            var mockSetupManager = new Mock<ISetupManager>();
            var mockGameStateManager = new Mock<IGameStateManager>();

            var gameboard = new Gameboard();

            mockSetupManager.Setup(x => x.SetupGameboard())
                .Returns(gameboard);

            var programManager = new ProgramManager(mockConsoleManager.Object, mockSetupManager.Object, mockGameStateManager.Object);

            programManager.Run();

            mockGameStateManager.Verify(x => x.PlayGame(gameboard), Times.Once());
        }

    }
}
