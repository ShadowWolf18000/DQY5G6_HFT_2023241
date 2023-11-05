using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Repository
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class
    {
        protected GameDbContext gameDbContext;
        public GenericRepository(GameDbContext ctx)
        {
            this.gameDbContext = ctx;
        }
        public void Create(T item)
        {
            gameDbContext.Set<T>().Add(item);
            gameDbContext.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return gameDbContext.Set<T>();
        }

        public void Delete(int id)
        {
            gameDbContext.Set<T>().Remove(Read(id));
            gameDbContext.SaveChanges();
        }

        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
