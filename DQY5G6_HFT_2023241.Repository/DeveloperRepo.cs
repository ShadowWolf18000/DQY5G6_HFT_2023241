using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Repository
{
    public class DeveloperRepo : GenericRepository<Developer>, IRepository<Developer>
    {
        public DeveloperRepo(GameDbContext ctx) : base(ctx) { }

        public override Developer Read(int id)
        {
            return gameDbContext.Developers.FirstOrDefault(d => d.DeveloperID == id);
        }

        public override void Update(Developer dev)
        {
            var oldDev = Read(dev.DeveloperID);
            foreach (var property in oldDev.GetType().GetProperties())
            {
                property.SetValue(oldDev, property.GetValue(dev));
            }
            gameDbContext.SaveChanges();
        }
    }
}
