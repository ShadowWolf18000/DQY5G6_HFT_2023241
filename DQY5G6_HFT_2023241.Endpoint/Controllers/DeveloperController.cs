using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using DQY5G6_HFT_2023241.Endpoint.Services;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeveloperController : Controller
    {
        IDeveloperLogic logic;
        IHubContext<SignalRHub> hub;

        public DeveloperController(IDeveloperLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Developer> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Developer Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Developer developer)
        {
            logic.Create(developer);
            hub.Clients.All.SendAsync("DeveloperCreated", developer);
        }

        [HttpPut]
        public void Update([FromBody] Developer developer)
        {
            logic.Update(developer);
            hub.Clients.All.SendAsync("DeveloperUpdated", developer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var developerToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("DeveloperDeleted", developerToDelete);
        }
    }
}
