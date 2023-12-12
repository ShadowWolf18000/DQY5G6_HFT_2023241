using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic
{
    internal class DeveloperLogic : IDeveloperLogic
    {
        IRepository<Developer> repository;

        public DeveloperLogic(IRepository<Developer> repository)
        {
            this.repository = repository;
        }

        public void Create(Developer dev)
        {
            if (dev == null)
                throw new ArgumentNullException("Developer cannot be null.");
            else
                repository.Create(dev);
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id");
            else
                repository.Delete(id);
        }

        public Developer Read(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id");
            else
                return repository.Read(id);
        }

        public IQueryable<Developer> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Developer dev)
        {
            if (dev == null)
                throw new ArgumentException("Developer cannot be null!");
            else
            repository.Update(dev);
        }
        

        // Egy fejlesztő különböző játékainak Launcherei
        public IEnumerable<Launcher> GetLaunchersForDeveloper(string devName)
        {
            return repository.ReadAll().
                SelectMany(e => e.Games).Select(e => e.Launcher).Distinct();
        }

        // Összes fejlesztő kilistázása, akik fejlesztettek egy adott Launcherre
        public IEnumerable<Developer> GetDevelopersByLauncher(string launcherName)
        {
            return repository.ReadAll()
                .Where(d => d.Games.Any(g => g.Launcher.LauncherName == launcherName))
                .ToList();
        }

    }
}
