using System;
using Moq;
using NUnit.Framework;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Repository;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace DQY5G6_HFT_2023241.Test
{
    [TestFixture]
    public class LogicTest
    {
        DeveloperLogic devLogic;
        GameLogic gameLogic;
        LauncherLogic launcherLogic;

        Mock<IRepository<Developer>> mockDevRepo;
        Mock<IRepository<Game>> mockGameRepo;
        Mock<IRepository<Launcher>> mockLauncherRepo;

        [SetUp]
        public void Init()
        {
            mockDevRepo = new Mock<IRepository<Developer>>();
            mockGameRepo = new Mock<IRepository<Game>>();
            mockLauncherRepo = new Mock<IRepository<Launcher>>();

            List<Developer> fakeDevs = new List<Developer>()
            {
                new Developer("1#DeveloperA#1990"),
                new Developer("2#DeveloperB#1995"),
                new Developer("3#DeveloperC#2000"),
                new Developer("4#DeveloperD#2005")
            };
            List<Game> fakeGames = new List<Game>()
            {
                new Game("1#GameA#1#1#9.0"),
                new Game("2#GameB#2#2#5.1"),
                new Game("3#GameC#3#3#7.2"),
                new Game("4#GameD#4#4#8.5"),
                new Game("5#GameE#4#4#9.8")
            };
            List<Launcher> fakeLaunchers = new List<Launcher>()
            {
                new Launcher("1#LauncherA#KiadoA"),
                new Launcher("2#LauncherB#KiadoB"),
                new Launcher("3#LauncherC#KiadoC"),
                new Launcher("4#LauncherD#KiadoD"),
            };
            
            mockDevRepo.Setup(d => d.ReadAll()).Returns(fakeDevs.AsQueryable());
            mockGameRepo.Setup(d => d.ReadAll()).Returns(fakeGames.AsQueryable());
            mockLauncherRepo.Setup(d => d.ReadAll()).Returns(fakeLaunchers.AsQueryable());

            devLogic = new DeveloperLogic(mockDevRepo.Object);
            gameLogic = new GameLogic(mockGameRepo.Object);
            launcherLogic = new LauncherLogic(mockLauncherRepo.Object);

            fakeDevs.ForEach(developer =>
            {
                developer.Games = fakeGames.FindAll(x => x.DeveloperID == developer.DeveloperID);
            });

            fakeGames.ForEach(game =>
            {
                game.Developer = fakeDevs.Find(x => x.DeveloperID == game.DeveloperID);
                game.Launcher = fakeLaunchers.Find(x => x.LauncherID == game.LauncherID);
            });

            fakeLaunchers.ForEach(launcher =>
            {
                launcher.Games = fakeGames.FindAll(x => x.LauncherID == launcher.LauncherID);
            });
        }

        [Test]
        public void DeveloperCreateValidTest()
        {
            // ARRANGE
            var devValid = new Developer("6#DeveloperX#2020");
            
            // ACT
            devLogic.Create(devValid);
           
            // ASSERT
            mockDevRepo.Verify(e => e.Create(devValid), Times.Once);
        }

        [Test]
        public void DeveloperCreateInvalidTest()
        {
            // ARRANGE
            var devInvalid = new Developer() { DeveloperName = null };

            // ACT
            try
            {
                devLogic.Create(devInvalid);
            }
            catch (ArgumentNullException e)
            {
                Assert.That(true);
            }

            // ASSERT
            mockDevRepo.Verify(e => e.Create(devInvalid), Times.Never);
        }

        [Test]
        public void GameCreateValidTest()
        {
            // ARRANGE
            var gameValid = new Game("7#GameF#4#4#7.9");

            // ACT
            gameLogic.Create(gameValid);

            // ASSERT
            mockGameRepo.Verify(e => e.Create(gameValid), Times.Once);
        }

        [Test]
        public void GameCreateInvalidTest()
        {
            // ARRANGE
            var gameInvalid = new Game() { Title = null };

            // ACT
            try
            {
                gameLogic.Create(gameInvalid);
            }
            catch (ArgumentNullException e)
            {
                Assert.That(true);
            }

            // ASSERT
            mockGameRepo.Verify(e => e.Create(gameInvalid), Times.Never);
        }

        [Test]
        public void LauncherCreateValidTest()
        {
            // ARRANGE
            var launcherValid = new Launcher("7#LauncherF#KiadoF");

            // ACT
            launcherLogic.Create(launcherValid);

            // ASSERT
            mockLauncherRepo.Verify(e => e.Create(launcherValid), Times.Once);
        }

        [Test]
        public void LauncherCreateInvalidTest()
        {
            // ARRANGE
            var launcherInvalid = new Launcher() { LauncherName = null };

            // ACT
            try
            {
                launcherLogic.Create(launcherInvalid);
            }
            catch (ArgumentNullException e)
            {
                Assert.That(true); // remove later?
            }

            // ASSERT
            mockLauncherRepo.Verify(e => e.Create(launcherInvalid), Times.Never);
        }

        [Test]
        public void GamesByDeveloperTest()
        {
            var games = gameLogic.GamesByDeveloper("DeveloperD");
            Assert.That(games.Count() == 2);
        }

        [Test]
        public void TopGamesByDeveloperOnPlatformTest()
        {
            var games = gameLogic.TopGamesByDeveloperOnPlatform("DeveloperD", "LauncherD");
            Assert.That(games.Count() == 1);
        }

        [Test]
        public void GamesByRatingRangeTest()
        {
            var games = gameLogic.GamesByRatingRange(5.0, 9.0,"DeveloperD");
            Assert.That(games.Count() == 1);
        }

        [Test]
        public void LaunchersForDeveloperTest()
        {
            var launchers = gameLogic.LaunchersForDeveloper("DeveloperA");
            Assert.That(launchers.Count() == 1);
        }

        [Test]
        public void DevelopersByLauncherTest()
        {
            var developers = devLogic.DevelopersByLauncher("LauncherD");
            Assert.That(developers.Count() == 1);
        }

        [Test]
        public void GameReadExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                gameLogic.Read(-1);
            });
        }

        [Test]
        public void DeveloperReadExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                devLogic.Read(-1);
            });
        }

        [Test]
        public void LauncherReadExceptionTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                launcherLogic.Read(-1);
            });
        }
    }
}
