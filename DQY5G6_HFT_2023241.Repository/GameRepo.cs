using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Repository
{
    public class GameRepo : GenericRepository<Game>, IRepository<Game>
    {
        public GameRepo(GameDbContext ctx) : base(ctx) { }

        public override Game Read(int id)
        {
            return gameDbContext.Games.FirstOrDefault(g => g.GameID == id);
        }

        public override void Update(Game game)
        {
            var old = Read(game.GameID);
            foreach (var property in old.GetType().GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    property.SetValue(old, property.GetValue(game));
            }
            gameDbContext.SaveChanges();
        }
    }
}
