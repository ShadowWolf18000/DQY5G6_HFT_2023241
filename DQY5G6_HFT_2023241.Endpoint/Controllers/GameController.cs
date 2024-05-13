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
    public class GameController : Controller
    {
        IGameLogic logic;
        IHubContext<SignalRHub> hub;

        public GameController(IGameLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Game> ReadAll()
        {
            return logic.ReadAll();
        }

        [HttpGet("{id}")]
        public Game Read(int id)
        {
            return logic.Read(id);
        }

        [HttpPost]
        public void Create([FromBody] Game game)
        {
            logic.Create(game);
            hub.Clients.All.SendAsync("GameCreated", game);
        }

        [HttpPut]
        public void Update([FromBody] Game game)
        {
            logic.Update(game);
            hub.Clients.All.SendAsync("GameUpdated", game);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = logic.Read(id);
            logic.Delete(id);
            hub.Clients.All.SendAsync("GameDeleted", gameToDelete);
        }
    }
}
