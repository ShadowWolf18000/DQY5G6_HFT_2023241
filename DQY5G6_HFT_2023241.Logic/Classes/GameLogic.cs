using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic
{
    internal class GameLogic : IGameLogic
    {
        IRepository<Game> repository;

        public GameLogic(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public void Create(Game game)
        {
            repository.Create(game);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Game Read(int id)
        {
            return repository.Read(id);
        }

        public IQueryable<Game> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Game game)
        {
            repository.Update(game);
        }
    }
}
