using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client
{
    class LauncherViaWindow : ILauncherService
    {
        public void Open(RestCollection<Launcher> launchers, RestCollection<Game> games)
        {
            new LauncherWindow(launchers, games).Show();
        }
    }
}
