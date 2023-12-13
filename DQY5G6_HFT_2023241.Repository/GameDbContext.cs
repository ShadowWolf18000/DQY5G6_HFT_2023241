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
                new Game("1#Counter-Strike: Global Offensive#1#1#9.1"),
                new Game("2#Dota 2#1#1#7.7"),
                new Game("3#PUBG#1#2#5.0"),
                new Game("4#The Witcher 3: Wild Hunt#1#3#9.7"),
                new Game("5#Grand Theft Auto V#1#4#8.7"),
                new Game("6#Half-Life: Alyx#1#1#9.8"),
                new Game("7#Among Us#1#5#9.4"),
                new Game("8#Cyberpunk 2077#1#3#5.3"),
                new Game("9#Hades#1#6#9.6"),
                new Game("10#Stardew Valley#1#7#9.7"),
                new Game("11#Rainbow Six Siege#1#8#7.9"),
                new Game("12#Fall Guys: Ultimate Knockout#1#9#8.3"),
                new Game("13#Red Dead Redemption 2#1#4#8.8"),
                new Game("14#Portal 2#1#1#9.8"),
                new Game("15#Terraria#1#10#9.8"),
                new Game("16#Monster Hunter: World#1#11#7.9"),
                new Game("17#Rocket League#1#12#9.0"),
                new Game("18#Dark Souls III#1#13#8.9"),
                new Game("19#Civilization VI#1#14#7.5"),
                new Game("20#Borderlands 3#1#15#8.1"),
                new Game("21#Halo: The Master Chief Collection#2#16#5.4"),
                new Game("22#Gears 5#2#17#8.4"),
                new Game("23#Forza Horizon 4#2#18#9.2"),
                new Game("24#Sea of Thieves#2#19#6.3"),
                new Game("25#Ori and the Blind Forest#2#20#9.6"),
                new Game("26#Microsoft Flight Simulator#2#21#9.2"),
                new Game("27#Minecraft#2#20#9.4"),
                new Game("28#Age of Empires IV#2#22#7.7"),
                new Game("29#State of Decay 2#2#23#7.6"),
                new Game("30#Quantum Break#2#24#7.7"),
                new Game("31#Fortnite#3#25#3.5"),
                new Game("32#Unreal Tournament (Alpha)#3#25#2.5"),
                new Game("33#The Division 2#3#8#7.0"),
                new Game("34#Satisfactory#3#26#9.5"),
                new Game("35#Control#3#27#8.6"),
                new Game("36#Metro Exodus#3#15#8.2"),
                new Game("37#Borderlands: The Handsome Collection#3#6#9.0"),
                new Game("38#Hades#3#24#9.6"),
                new Game("39#Alan Wake Remastered#3#8#3.8"),
                new Game("40#Watch Dogs: Legion#3#8#5.8"),
                new Game("41#Assassin's Creed Valhalla#4#8#7.4"),
                new Game("42#Far Cry 6#4#8#5.1"),
                new Game("43#Rainbow Six Siege#4#8#7.9"),
                new Game("44#The Division 2#4#8#7.0"),
                new Game("45#Watch Dogs: Legion#4#8#5.8"),
                new Game("46#Immortals Fenyx Rising#4#8#8.5"),
                new Game("47#Ghost Recon Breakpoint#4#8#6.0"),
                new Game("48#Assassin's Creed Odyssey#4#8#7.5"),
                new Game("49#Anno 1800#4#8#8.2"),
                new Game("50#The Crew 2#4#8#5.4"),
                new Game("51#World of Warcraft#5#28#9.8"),
                new Game("52#Overwatch#5#28#8.2"),
                new Game("53#Diablo III#5#28#7.8"),
                new Game("54#Hearthstone#5#28#6.0"),
                new Game("55#StarCraft II#5#28#8.4"),
                new Game("56#Heroes of the Storm#5#28#6.4"),
                new Game("57#Warcraft III: Reforged#5#28#4.2"),
                new Game("58#Call of Duty: Warzone#5#29#7.4"),
                new Game("59#Diablo IV#5#28#7.9"),
                new Game("60#Overwatch 2#5#28#8.7")
            });

            modelBuilder.Entity<Launcher>().HasData(
            new Launcher[]
            {
                new Launcher("1#Steam#Valve Corporation"),
                new Launcher("2#Microsoft Store#Microsoft"),
                new Launcher("3#Epic Games Store#Epic Games"),
                new Launcher("4#Ubisoft Connect#Ubisoft"),
                new Launcher("5#Blizzard Launcher#Blizzard Entertainment")
            });

            modelBuilder.Entity<Developer>().HasData(
            new Developer[]
            {
                new Developer("1#Valve#1996"),
                new Developer("2#PUBG Corporation#2018"),
                new Developer("3#CD Projekt RED#1994"),
                new Developer("4#Rockstar Games#1998"),
                new Developer("5#InnerSloth#2015"),
                new Developer("6#Supergiant Games#2009"),
                new Developer("7#ConcernedApe#2012"),
                new Developer("8#Ubisoft#1986"),
                new Developer("9#Mediatonic#2005"),
                new Developer("10#Re-Logic#2011"),
                new Developer("11#Capcom#1979"),
                new Developer("12#Psyonix#2000"),
                new Developer("13#FromSoftware#1986"),
                new Developer("14#Firaxis Games#1996"),
                new Developer("15#Gearbox Software#1999"),
                new Developer("16#343 Industries#2007"),
                new Developer("17#The Coalition#2010"),
                new Developer("18#Playground Games#2010"),
                new Developer("19#Rare#1985"),
                new Developer("20#Mojang#2009"),
                new Developer("21#Asobo Studio#2002"),
                new Developer("22#Relic Entertainment#1997"),
                new Developer("23#Undead Labs#2009"),
                new Developer("24#Remedy Entertainment#1995"),
                new Developer("25#Epic Games#1991"),
                new Developer("26#Coffee Stain Studios#2010"),
                new Developer("27#4A Games#2005"),
                new Developer("28#Blizzard Entertainment#1991"),
                new Developer("29#Infinity Ward#2002")
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
