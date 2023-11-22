using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic.Interfaces
{
    internal interface IGameLogic
    {
        void Create(Game game);
        void Delete(int id);
        Game Read(int id);
        IQueryable<Game> ReadAll();
        void Update(Game game);
    }
}
