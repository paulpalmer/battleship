// See https://aka.ms/new-console-template for more information
using BattleShipStateTracker;

var iocContainer = new IocContainer();

var programManager = iocContainer.GetProgramManager();
programManager.Run();
