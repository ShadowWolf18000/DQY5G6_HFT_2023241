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
    public class DeveloperLogic : IDeveloperLogic
    {
        IRepository<Developer> repository;

        public DeveloperLogic(IRepository<Developer> repository)
        {
            this.repository = repository;
        }

        public void Create(Developer dev)
        {
            try
            {
                if (dev.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(dev) == null))
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
                else if (dev.DeveloperID < 0)
                    throw new ArgumentOutOfRangeException(nameof(dev.DeveloperID));
                else
                    repository.Create(dev);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id");
                else
                    repository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Developer Read(int id)
        {
            try
            {
                if (id < 0)
                    throw new ArgumentOutOfRangeException("id");
                else
                    return repository.Read(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<Developer> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Developer dev)
        {
            try
            {
                if (dev.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(dev) == null))
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be updated!");
                else
                    repository.Update(dev);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Összes fejlesztő kilistázása, akik fejlesztettek egy adott Launcherre
        public IEnumerable<Developer> DevelopersByLauncher(string launcherName)
        {
            return repository.ReadAll()
                .Where(d => d.Games.Any(g => g.Launcher.LauncherName == launcherName))
                .ToList();
        }
    }
}
