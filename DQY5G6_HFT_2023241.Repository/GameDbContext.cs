using DQY5G6_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Repository
{
    public class GameDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Launcher> Launchers { get; set; }

        public GameDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured != true)
            {
                optionsBuilder.UseInMemoryDatabase("Game").UseLazyLoadingProxies();
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Developer>(developer => developer
            //.HasKey(d => d.DeveloperID));

            //modelBuilder.Entity<Launcher>()
            //    .HasKey(l => l.LauncherID);

            //modelBuilder.Entity<Game>()
            //.HasKey(g => g.GameID);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Launcher)
                .WithMany(l => l.Games)
                .HasForeignKey(g => g.LauncherID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Game>().HasData(
            new Game[]
            {
                //new Game("1#The Witcher 3#1#2#9.3"),
                //new Game("2#Red Dead Redemption 2#2#1#9.8"),
                //new Game("3#Minecraft#3#3#9.2"),
                //new Game("4#Cyberpunk 2077#1#2#6.2"),
                //new Game("5#The Elder Scrolls V: Skyrim#4#1#9.4"),
                //new Game("6#Among Us#5#3#8.5"),
                //new Game("7#Grand Theft Auto V#2#1#9.7"),
                //new Game("8#Fortnite#6#3#8.0"),
                //new Game("9#Counter-Strike: Global Offensive#7#4#8.6"),
                //new Game("10#Overwatch#8#4#9.0"),
                //new Game("11#The Legend of Zelda: Breath of the Wild#10#10#9.5"),
                //new Game("12#Horizon Zero Dawn#11#4#8.9"),
                //new Game("13#The Last of Us Part II#12#10#9.6"),
                //new Game("14#Dark Souls III#13#4#9.1"),
                //new Game("15#Final Fantasy VII Remake#14#4#9.4"),
                //new Game("16#Resident Evil Village#15#4#8.7"),
                //new Game("17#Doom Eternal#16#4#8.9"),
                //new Game("18#Cyber Shadow#17#3#7.9"),
                //new Game("19#Monster Hunter: World#18#4#8.6"),
                //new Game("20#Sekiro: Shadows Die Twice#19#4#9.2"),
                //new Game("21#Hades#20#4#9.3"),
                //new Game("22#Assassin's Creed Valhalla#9#5#8.8"),
                //new Game("23#Bioshock Infinite#21#4#9.0"),
                //new Game("24#Star Wars Jedi: Fallen Order#22#4#8.7"),
                //new Game("25#Fallout 4#4#1#8.6"),
                //new Game("26#Terraria#23#3#9.0"),
                //new Game("27#The Outer Worlds#24#1#8.4"),
                //new Game("28#Celeste#25#4#9.3"),
                //new Game("29#Stardew Valley#26#3#9.1"),
                //new Game("30#Dead by Daylight#27#4#7.8")

                new Game("1#The Witcher 3#1#2#9.3"),
                new Game("2#Red Dead Redemption 2#2#2#9.8"),
                new Game("3#Minecraft#2#3#8.7"),
                new Game("4#Fallout 4#4#9#8.8"),
                new Game("5#Among Us#5#5#8.0"),
                new Game("6#Fortnite#6#6#7.5"),
                new Game("7#Half-Life 2#4#6#9.5"),
                new Game("8#Diablo III#8#8#9.1"),
                new Game("9#Assassin's Creed Valhalla#9#9#8.6"),
                new Game("10#Halo Infinite#10#10#8.9"),
                new Game("11#Far Cry 5#1#2#8.6"),
                new Game("12#Destiny 2#2#2#8.9"),
                new Game("13#Stardew Valley#3#3#9.5"),
                new Game("14#The Last of Us Part II#10#10#9.7"),
                new Game("15#Rainbow Six Siege#7#6#9.1"),
                new Game("16#FIFA 22#6#6#8.4"),
                new Game("17#The Outer Worlds#4#9#8.2"),
                new Game("18#Starcraft II#8#8#9.0"),
                new Game("19#Subnautica#6#4#8.8"),
                new Game("20#Civilization VI#2#2#9.6"),
                new Game("21#Rust#3#5#7.8"),
                new Game("22#No Man's Sky#10#10#8.3"),
                new Game("23#Call of Duty: Warzone#2#9#8.7"),
                new Game("24#Mortal Kombat 11#7#6#8.5"),
                new Game("25#Resident Evil Village#1#2#9.2"),
                new Game("26#Rocket League#6#4#8.6"),
                new Game("27#The Legend of Zelda: Breath of the Wild#10#10#9.5"),
                new Game("28#World of Warcraft#8#8#9.2"),
                new Game("29#Microsoft Flight Simulator#6#6#8.8"),
                new Game("30#Assassin's Creed Odyssey#9#9#8.4")
            });

            modelBuilder.Entity<Launcher>().HasData(
            new Launcher[]
            {
                new Launcher("1#Rockstar Launcher#Rockstar Games"),
                new Launcher("2#GOG Galaxy#CD Projekt"),
                new Launcher("3#Epic Games Store#Epic Games"),
                new Launcher("4#Steam#Valve Corporation"),
                new Launcher("5#UPlay#Ubisoft"),
                new Launcher("6#Blizzard Battle.net#Blizzard Entertainment"),
                new Launcher("7#Origin#Electronic Arts"),
                new Launcher("8#Xbox Game Pass#Microsoft"),
                new Launcher("9#PlayStation Store#Sony Interactive Entertainment"),
                new Launcher("10#Nintendo eShop#Nintendo")
            });

            modelBuilder.Entity<Developer>().HasData(
            new Developer[]
            {
                new Developer("1#CD Projekt Red#1994"),
                new Developer("2#Rockstar Games#1998"),
                new Developer("3#Mojang Studios#2009"),
                new Developer("4#Bethesda Game Studios#2001"),
                new Developer("5#Innersloth#2015"),
                new Developer("6#Epic Games#1991"),
                new Developer("7#Valve Corporation#1996"),
                new Developer("8#Blizzard Entertainment#1991"),
                new Developer("9#Ubisoft#1986"),
                new Developer("10#343 Industries#2007")
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
