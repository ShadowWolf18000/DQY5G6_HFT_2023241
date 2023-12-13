using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LauncherController : Controller
    {
        ILauncherLogic logic;

        public LauncherController(ILauncherLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<Launcher> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Launcher Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Launcher launcher)
        {
            logic.Create(launcher);
        }

        [HttpPut]
        public void Update([FromBody] Launcher launcher)
        {
            logic.Update(launcher);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
