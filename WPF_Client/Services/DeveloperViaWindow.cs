using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Client
{
    class DeveloperViaWindow : IDeveloperService
    {
        public void Open(RestCollection<Developer> developers, RestCollection<Game> games)
        {
            new DeveloperWindow(developers, games).Show();
        }
    }
}
