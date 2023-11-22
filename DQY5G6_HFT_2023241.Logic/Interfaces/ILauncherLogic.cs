using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic
{
    internal interface ILauncherLogic
    {
        void Create(Launcher l);
        void Delete(int id);
        Launcher Read(int id);
        IQueryable<Launcher> ReadAll();
        void Update(Launcher l);
    }
}
