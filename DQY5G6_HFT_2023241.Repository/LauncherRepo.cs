using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Repository
{
    public class LauncherRepo : GenericRepository<Launcher>, IRepository<Launcher>
    {
        public LauncherRepo(GameDbContext ctx) : base(ctx) { }

        public override Launcher Read(int id)
        {
            return gameDbContext.Launchers.FirstOrDefault(l => l.LauncherID == id);
        }

        public override void Update(Launcher launcher)
        {
            var old = Read(launcher.LauncherID);
            foreach (var property in old.GetType().GetProperties())
            {
                property.SetValue(old, property.GetValue(launcher));
            }
            gameDbContext.SaveChanges();
        }
    }
}
