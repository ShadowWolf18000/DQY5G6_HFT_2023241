using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Repository;
using System;
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
            repository.Create(dev);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Developer Read(int id)
        {
            return repository.Read(id);
        }

        public IQueryable<Developer> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Developer dev)
        {
            repository.Update(dev);
        }
    }
}
