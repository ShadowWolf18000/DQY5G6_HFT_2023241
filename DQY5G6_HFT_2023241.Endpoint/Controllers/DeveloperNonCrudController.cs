using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DeveloperNonCrudController : Controller
    {
        IDeveloperLogic logic;

        public DeveloperNonCrudController(IDeveloperLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("{launcherName}")]
        public IEnumerable<Developer> DevelopersByLauncher(string launcherName)
        {
            return logic.DevelopersByLauncher(launcherName);
        }
    }
}
