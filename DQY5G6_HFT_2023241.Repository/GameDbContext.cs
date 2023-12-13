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
                new Game("3#PUBG#2#1#5.0"),
                new Game("4#The Witcher 3: Wild Hunt#3#1#9.7"),
                new Game("5#Grand Theft Auto V#4#1#8.7"),
                new Game("6#Half-Life: Alyx#1#1#9.8"),
                new Game("7#Among Us#5#1#9.4"),
                new Game("8#Cyberpunk 2077#3#1#5.3"),
                new Game("9#Hades#6#1#9.6"),
                new Game("10#Stardew Valley#7#1#9.7"),
                new Game("11#Rainbow Six Siege#8#1#7.9"),
                new Game("12#Fall Guys: Ultimate Knockout#9#1#8.3"),
                new Game("13#Red Dead Redemption 2#4#1#8.8"),
                new Game("14#Portal 2#1#1#9.8"),
                new Game("15#Terraria#10#1#9.8"),
                new Game("16#Monster Hunter: World#11#1#7.9"),
                new Game("17#Rocket League#12#1#9.0"),
                new Game("18#Dark Souls III#13#1#8.9"),
                new Game("19#Civilization VI#14#1#7.5"),
                new Game("20#Borderlands 3#15#1#8.1"),
                new Game("21#Halo: The Master Chief Collection#16#2#5.4"),
                new Game("22#Gears 5#17#2#8.4"),
                new Game("23#Forza Horizon 4#18#2#9.2"),
                new Game("24#Sea of Thieves#19#2#6.3"),
                new Game("25#Ori and the Blind Forest#20#2#9.6"),
                new Game("26#Microsoft Flight Simulator#21#2#9.2"),
                new Game("27#Minecraft#20#2#9.4"),
                new Game("28#Age of Empires IV#22#2#7.7"),
                new Game("29#State of Decay 2#23#22#7.6"),
                new Game("30#Quantum Break#24#2#7.7"),
                new Game("31#Fortnite#25#3#3.5"),
                new Game("32#Unreal Tournament (Alpha)#25#3#2.5"),
                new Game("33#The Division 2#8#3#7.0"),
                new Game("34#Satisfactory#26#3#9.5"),
                new Game("35#Control#27#3#8.6"),
                new Game("36#Metro Exodus#15#3#8.2"),
                new Game("37#Borderlands: The Handsome Collection#6#3#9.0"),
                new Game("38#Hades#24#3#9.6"),
                new Game("39#Alan Wake Remastered#8#3#3.8"),
                new Game("40#Watch Dogs: Legion#8#3#5.8"),
                new Game("41#Assassin's Creed Valhalla#8#4#7.4"),
                new Game("42#Far Cry 6#8#4#5.1"),
                new Game("43#Rainbow Six Siege#8#4#7.9"),
                new Game("44#The Division 2#8#4#7.0"),
                new Game("45#Watch Dogs: Legion#8#4#5.8"),
                new Game("46#Immortals Fenyx Rising#8#4#8.5"),
                new Game("47#Ghost Recon Breakpoint#8#4#6.0"),
                new Game("48#Assassin's Creed Odyssey#8#4#7.5"),
                new Game("49#Anno 1800#8#4#8.2"),
                new Game("50#The Crew 2#8#4#5.4"),
                new Game("51#World of Warcraft#28#5#9.8"),
                new Game("52#Overwatch#28#5#8.2"),
                new Game("53#Diablo III#28#5#7.8"),
                new Game("54#Hearthstone#28#5#6.0"),
                new Game("55#StarCraft II#28#5#8.4"),
                new Game("56#Heroes of the Storm#28#5#6.4"),
                new Game("57#Warcraft III: Reforged#28#5#4.2"),
                new Game("58#Call of Duty: Warzone#29#5#7.4"),
                new Game("59#Diablo IV#28#5#7.9"),
                new Game("60#Overwatch 2#28#5#8.7")
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
