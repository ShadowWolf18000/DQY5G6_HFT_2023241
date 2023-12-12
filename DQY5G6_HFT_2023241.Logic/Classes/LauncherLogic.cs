using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DQY5G6_HFT_2023241.Logic
{
    public class LauncherLogic : ILauncherLogic
    {
        IRepository<Launcher> repository;

        public LauncherLogic(IRepository<Launcher> repo)
        {
            this.repository = repo;
        }

        public void Create(Launcher l)
        {
            repository.Create(l);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Launcher Read(int id)
        {
            return repository.Read(id);
        }

        public IQueryable<Launcher> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Launcher l)
        {
            repository.Update(l);
        }

        

    }
}
