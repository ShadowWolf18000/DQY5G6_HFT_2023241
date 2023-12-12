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
    public class GameLogic : IGameLogic
    {
        IRepository<Game> repository;

        public GameLogic(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public void Create(Game game)
        {
            if (game.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(game) == null))
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
            if (game.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(game) == null))
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

        // Legkedveltebb játékok listája egy adott fejlesztő alapján, egy adott platformon: rating 9.5 vagy nagyobb
        public IEnumerable<Game> GetTopGamesByDeveloperOnPlatform(string developerName, string launcherName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == developerName)
                .ToList()
                .Where(g => g.Rating >= 9.5 && g.Launcher.LauncherName == launcherName)
                .OrderByDescending(g => g.Rating)
                .ToList();
        }

        // Összes játék egy bizonyos értékelési intervallumon belül, egy adott fejlesztőtől
        public IEnumerable<Game> GetGamesByRatingRange(double minRating, double maxRating, string developerName)
        {
            if (minRating < 0.0 || maxRating > 10.0)
                throw new ArgumentOutOfRangeException();
            else
                return repository.ReadAll()
                    .Where(g => g.Rating >= minRating && g.Rating <= maxRating && g.Developer.DeveloperName == developerName)
                    .ToList();
        }

        // Egy fejlesztő különböző játékainak Launcherei
        public IEnumerable<Launcher> GetLaunchersForDeveloper(string devName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == devName)
                .Select(g => g.Launcher)
                .Distinct()
                .ToList();
        }
    }
}
