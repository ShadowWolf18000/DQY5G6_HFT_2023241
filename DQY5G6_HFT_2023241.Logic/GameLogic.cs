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
            try
            {
                if (game.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(game) == null))
                {
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
                }
                else if (game.GameID < 0 || game.DeveloperID < 0 || game.LauncherID < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    repository.Create(game);
                }
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

        public Game Read(int id)
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

        public IQueryable<Game> ReadAll()
        {
            return repository.ReadAll();
        }

        public void Update(Game game)
        {
            try
            {
                if (game.GetType().GetProperties().Where(x => !x.GetMethod.IsVirtual).Any(x => x.GetValue(game) == null))
                    throw new ArgumentNullException("A property in the object is null, therefore it cannot be added to the database.");
                else
                    repository.Update(game);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Összes játék kilistázása adott fejlesztőtől
        public IEnumerable<Game> GamesByDeveloper(string developerName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == developerName)
                .ToList();
        }

        // Legkedveltebb játékok listája egy adott fejlesztő alapján, egy adott platformon: rating 8.5 vagy nagyobb
        public IEnumerable<Game> TopGamesByDeveloperOnPlatform(string developerName, string launcherName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == developerName)
                .ToList()
                .Where(g => g.Rating >= 8.5 && g.Launcher.LauncherName == launcherName)
                .OrderByDescending(g => g.Rating)
                .ToList();
        }

        // Összes játék egy bizonyos értékelési intervallumon belül, egy adott fejlesztőtől
        public IEnumerable<Game> GamesByRatingRange(double minRating, double maxRating, string developerName)
        {
            if (minRating < 0.0 || maxRating > 10.0)
                throw new ArgumentOutOfRangeException();
            else
                return repository.ReadAll()
                    .Where(g => g.Rating >= minRating && g.Rating <= maxRating && g.Developer.DeveloperName == developerName)
                    .ToList();
        }

        // Egy fejlesztő különböző játékainak Launcherei
        public IEnumerable<Launcher> LaunchersForDeveloper(string devName)
        {
            return repository.ReadAll()
                .Where(g => g.Developer.DeveloperName == devName)
                .Select(g => g.Launcher)
                .Distinct()
                .ToList();
        }
    }
}
