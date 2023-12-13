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
            try
            {
                if (l.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(l) == null))
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
                else if (l.LauncherID < 0)
                    throw new ArgumentOutOfRangeException();
                else
                    repository.Create(l);
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

        public Launcher Read(int id)
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

        public IQueryable<Launcher> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Launcher l)
        {
            try
            {
                if (l.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(l) == null))
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
                else
                    repository.Update(l);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
