﻿using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic
{
    public interface IDeveloperLogic
    {
        void Create(Developer dev);
        void Delete(int id);
        Developer Read(int id);
        IQueryable<Developer> ReadAll();
        void Update(Developer dev);
        IEnumerable<Developer> DevelopersByLauncher(string launcherName);
    }
}
