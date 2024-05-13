using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;
using DQY5G6_HFT_2023241.Endpoint.Services;
using Microsoft.AspNetCore.SignalR;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LauncherController : Controller
    {
        ILauncherLogic logic;
        IHubContext<SignalRHub> hub;

        public LauncherController(ILauncherLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            hub.Clients.All.SendAsync("LauncherCreated", launcher);
        }

        [HttpPut]
        public void Update([FromBody] Launcher launcher)
        {
            logic.Update(launcher);
            hub.Clients.All.SendAsync("LauncherUpdated", launcher);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var launcherToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("LauncherDeleted", launcherToDelete);
        }
    }
}
