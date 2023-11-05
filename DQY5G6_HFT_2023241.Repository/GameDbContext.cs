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
        public DbSet<Game> Games{ get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Launcher> Launchers { get; set; }

        public GameDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured != true)
            {
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("Game");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>(developer => developer
            .HasKey(d => d.DeveloperID));

            modelBuilder.Entity<Launcher>()
                .HasKey(l => l.LauncherID);

            modelBuilder.Entity<Game>()
            .HasKey(g => g.GameID);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperID);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.Launcher)
                .WithMany(l => l.Games)
                .HasForeignKey(g => g.LauncherID);




        }












    }
}
