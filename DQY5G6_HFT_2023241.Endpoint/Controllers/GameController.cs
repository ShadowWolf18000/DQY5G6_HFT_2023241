using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : Controller
    {
        IGameLogic logic;

        public GameController(IGameLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Game game)
        {
            logic.Update(game);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
