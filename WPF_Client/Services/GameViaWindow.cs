using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client
{
    class GameViaWindow : IGameService
    {
        public void Open(RestCollection<Game> games, RestCollection<Developer> developers, RestCollection<Launcher> launchers)
        {
            new GameWindow(developers, launchers, games).Show();
        }
    }
}
