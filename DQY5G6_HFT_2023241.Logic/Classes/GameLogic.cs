using DQY5G6_HFT_2023241.Models;
using DQY5G6_HFT_2023241.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Logic
{
    internal class GameLogic : IGameLogic
    {
        IRepository<Game> repository;

        public GameLogic(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public void Create(Game game)
        {
            if (game.GetType().GetProperties().Any(x => x.GetValue(game) == null))
                throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
            else
                repository.Create(game);
        }

        public void Delete(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id");
            else
                repository.Delete(id);
        }

        public Game Read(int id)
        {
            if (id < 0)
                throw new ArgumentOutOfRangeException("id");
            else
                return repository.Read(id);
        }

        public IQueryable<Game> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Game game)
        {
            if (game.GetType().GetProperties().Any(x => x.GetValue(game) == null))
                throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
            else
                repository.Update(game);
        }

        // Összes játék kilistázása adott fejlesztőtől
        public IEnumerable<Game> GetGamesByDeveloper(string developerName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == developerName)
                .ToList();
        }

        // Legkedveltebb játékok listája egy adott fejlesztő alapján: rating 9.5 vagy nagyobb
        public IEnumerable<Game> GetTopGamesByDeveloper(string developerName)
        {
            return GetGamesByDeveloper(developerName)
                .Where(g => g.Rating >= 9.5)
                .OrderByDescending(g => g.Rating)
                .ToList();
        }

        // Összes játék kilistázása adott értékelési tartománnyal
        public IEnumerable<Game> GetGamesByRatingRange(double minRating, double maxRating)
        {
            return repository.ReadAll()
                .Where(g => g.Rating >= minRating && g.Rating <= maxRating)
                .ToList();
        }
    }
}
