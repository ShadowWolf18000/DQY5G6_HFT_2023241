using Microsoft.AspNetCore.Mvc;
using DQY5G6_HFT_2023241.Logic;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;

namespace DQY5G6_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeveloperController : Controller
    {
        IDeveloperLogic logic;

        public DeveloperController(IDeveloperLogic logic)
        {
            this.logic = logic;
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
        }

        [HttpPut]
        public void Update([FromBody] Developer developer)
        {
            logic.Update(developer);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }
    }
}
