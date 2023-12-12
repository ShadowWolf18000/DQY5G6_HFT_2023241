using DQY5G6_HFT_2023241.Repository;
using System;

namespace DQY5G6_HFT_2023241
{
    internal class Program
    {
        // TÁVOLÍTSD EL A FÜGGŐSÉGEKET A VÉSŐ COMMIT ELŐTT

        static void Main(string[] args)
        {
            GameDbContext ctx = new GameDbContext();
            var a = ctx.Games;
            var b = ctx.Developers;
            var c = ctx.Launchers;

            ;


            Console.ReadKey();
        }
    }
}
