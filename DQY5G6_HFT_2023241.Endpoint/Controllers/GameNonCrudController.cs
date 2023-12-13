using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GameNonCrudController : Controller
    {
        IGameLogic logic;

        public GameNonCrudController(IGameLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{developerName}")]
        public IEnumerable<Game> GamesByDeveloper(string developerName)
        {
            return logic.GamesByDeveloper(developerName);
        }

        [HttpGet("{developerName}/{launcherName}")]
        public IEnumerable<Game> TopGamesByDeveloperOnPlatform(string developerName, string launcherName)
        {
            return logic.TopGamesByDeveloperOnPlatform(developerName, launcherName);
        }

        [HttpGet("{minRating}/{maxRating}/{developerName}")]
        public IEnumerable<Game> GamesByRatingRange(double minRating, double maxRating, string developerName)
        {
            return logic.GamesByRatingRange(minRating, maxRating, developerName);
        }

        [HttpGet("{devName}")]
        public IEnumerable<Launcher> LaunchersForDeveloper(string devName)
        {
            return logic.LaunchersForDeveloper(devName);
        }
    }
}
