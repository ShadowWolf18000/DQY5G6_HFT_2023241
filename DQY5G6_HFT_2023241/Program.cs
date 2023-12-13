using System;
using System.Linq;
using System.Transactions;
using ConsoleTools;
using DQY5G6_HFT_2023241.Client;
using DQY5G6_HFT_2023241.Models;
using System.Collections.Generic;

// TÁVOLÍTSD EL A FÜGGŐSÉGEKET A VÉSŐ COMMIT ELŐTT
namespace DQY5G6_HFT_2023241
{
    internal class Program
    {
        static RestService rest;

        static void Main(string[] args)
        {
                
        }

        static string TransformToSingleLine(List<string> raw)
        {
            string transformed = "";
            for (int i = 0; i < raw.Count(); i++)
            {
                if (i == raw.Count - 1)
                {
                    transformed += raw[i];
                }
                else
                {
                    transformed += $"{raw[i]}#";
                }
            }
            return transformed;
        }

        static void Create(string entity)
        {
            try
            {
                if (entity == "Developer")
                {
                    var properties = typeof(Developer).GetProperties();
                    Console.WriteLine("Enter developer's data:");
                    List<string> raw = new List<string>();
                    foreach (var prop in properties)
                    {
                        if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                        {
                            Console.WriteLine($"{prop.Name}: ");
                            raw.Add(Console.ReadLine());
                        }
                    }
                    string data = TransformToSingleLine(raw);
                    var newDev = new Developer(data);
                    rest.Post(newDev, "developer");
                    
                }
                else if (entity == "Game")
                {
                    var properties = typeof(Game).GetProperties();
                    Console.WriteLine("Enter game data:");
                    List<string> raw = new List<string>();
                    foreach (var prop in properties)
                    {
                        if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                        {
                            Console.WriteLine($"{prop.Name}: ");
                            raw.Add(Console.ReadLine());
                        }
                    }
                    string data = TransformToSingleLine(raw);
                    var newGame = new Game(data);
                    rest.Post(newGame, "game");
                }
                else if (entity == "Launcher")
                {
                    var properties = typeof(Launcher).GetProperties();
                    Console.WriteLine("Enter launcher data:");
                    List<string> raw = new List<string>();
                    foreach (var prop in properties)
                    {
                        if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                        {
                            Console.WriteLine($"{prop.Name}: ");
                            raw.Add(Console.ReadLine());
                        }
                    }
                    string data = TransformToSingleLine(raw);
                    var newLauncher = new Launcher(data);
                    rest.Post(newLauncher, "launcher");
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        static void List(string entity)
        {
            if (entity == "Developer")
            {
                List<Developer> devs = rest.Get<Developer>("developer");
                foreach (var item in devs)
                {
                    Console.WriteLine(item.DeveloperID + ": " + item.DeveloperName);
                }
            }
            else if (entity == "Game")
            {
                List<Game> games = rest.Get<Game>("game");
                foreach (var item in games)
                {
                    Console.WriteLine(item.GameID + ": " + item.Title);
                }
            }
            else if (entity == "Launcher")
            {
                List<Launcher> launchers = rest.Get<Launcher>("launcher");
                foreach (var item in launchers)
                {
                    Console.WriteLine(item.LauncherID + ": " + item.LauncherName);
                }
            }

            Console.ReadLine();
        }
        static void Update(string entity)
        {
            try
            {
                if (entity == "Developer")
                {
                    Console.WriteLine("Enter developer ID to update: ");
                    int id = int.Parse(Console.ReadLine());
                    Developer dev = rest.Get<Developer>(id, "developer");
                    Console.WriteLine($"New name [Old name: {dev.DeveloperName}]: ");
                    string newName = Console.ReadLine();
                    dev.DeveloperName = newName;
                    rest.Put(dev,"developer");
                }
                else if(entity == "Game")
                {
                    Console.WriteLine("Enter game ID to update: ");
                    int id = int.Parse(Console.ReadLine());
                    Game game = rest.Get<Game>(id, "game");
                    Console.WriteLine($"New title [Old title: {game.Title}]: ");
                    string newTitle = Console.ReadLine();
                    game.Title = newTitle;
                    rest.Put(game, "game");
                }
                else if(entity == "Launcher")
                {
                    Console.WriteLine("Enter launcher ID to update: ");
                    int id = int.Parse(Console.ReadLine());
                    Launcher launcher = rest.Get<Launcher>(id, "launcher");
                    Console.WriteLine($"New name [Old name: {launcher.LauncherName}]: ");
                    string newName = Console.ReadLine();
                    launcher.LauncherName = newName;
                    rest.Put(launcher, "launcher");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        static void Delete(string entity)
        {
            try
            {
                if (entity == "Developer")
                {
                    Console.WriteLine("Enter a developer's id to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "developer");
                }
                else if (entity == "Game")
                {
                    Console.WriteLine("Enter a game's id to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "game");
                }
                else if (entity == "Launcher")
                {
                    Console.WriteLine("Enter a launcher's id to delete: ");
                    int id = int.Parse(Console.ReadLine());
                    rest.Delete(id, "launcher");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }

        // Non-CRUDs
        static void GamesByDeveloper(string entity)
        {
            try
            {
                Console.WriteLine("Enter a developer's name: ");
                string name = Console.ReadLine();
                var games = rest.GetGamesByDeveloper<Game>(entity, "GamesByDeveloper", name);
                if (games.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine($"This developer has no games listed in our database.");
                }
                else
                {
                    Console.WriteLine("Games by chosen developer: ");
                    games.ForEach(x =>
                    {
                        Console.WriteLine($"{x.GameID}: {x.Title}");
                    });
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }
        static void TopGamesByDeveloperOnPlatform(string entity) 
        {
            try
            {
                Console.WriteLine("Enter developer's name: ");
                string devName = Console.ReadLine();
                Console.WriteLine("Enter launcher's name: ");
                string launcherName = Console.ReadLine();

                var games = rest.GetTopGamesByDeveloperOnPlatform<Game>(entity, "TopGamesByDeveloperOnPlatform", devName, launcherName);
                if (games.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine($"No games can be found with '{devName}' and/or '{launcherName}' in the database.");
                }
                else
                {
                    Console.WriteLine($"Games from {devName} on {launcherName} with ratings 9.4<");
                    games.ForEach(x =>
                    {
                        Console.WriteLine($"{x.GameID}: {x.Title} | Rating: {x.Rating}");
                    });
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        static void GamesByRatingRange(string entity) 
        {
            try
            {
                Console.WriteLine("Enter value for minimum rating: ");
                double min = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter value for maximum rating: ");
                double max = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter the developer's name: ");
                string devName = Console.ReadLine();

                var games = rest.GetGamesByRatingRange<Game>(entity, "GamesByRatingRange", min, max, devName);
                if (games.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine("Can't find any game in specified rating interval and/or with specified developer.");
                }
                else
                {
                    Console.WriteLine($"Games by {devName} in rating interval [{min}, {max}]");
                    games.ForEach(x =>
                    {
                        Console.WriteLine($"{x.GameID}: {x.Title} | Rating: {x.Rating}");
                    });
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }   
        }
        static void LaunchersForDeveloper(string entity) 
        {
            try
            {
                Console.WriteLine("Enter a developer's name: ");
                string devName = Console.ReadLine();
                var launchers = rest.GetLaunchersForDeveloper<Launcher>(entity, "LaunchersForDeveloper", devName);
                if (launchers.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine($"No launchers can be found for {devName}.");
                }
                else
                {
                    Console.WriteLine($"The launchers, for which {devName} published games to: ");
                    launchers.ForEach(x =>
                    {
                        Console.WriteLine($"{x.LauncherID}: {x.LauncherName}");
                    });
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
        static void DevelopersByLauncher(string entity) 
        {
            try
            {
                Console.WriteLine("Enter a launcher's name: ");
                string launcherName = Console.ReadLine();
                var devs = rest.GetDevelopersByLauncher<Developer>(entity, "DevelopersByLauncher", launcherName);
                if (devs.ElementAtOrDefault(0) == null)
                {
                    Console.WriteLine($"No developers can be found for '{launcherName}'.");
                }
                else
                {
                    Console.WriteLine($"Developers who published games on {launcherName}: ");
                    devs.ForEach(x =>
                    {
                        Console.WriteLine($"{x.DeveloperID}: {x.DeveloperName}");
                    });
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
