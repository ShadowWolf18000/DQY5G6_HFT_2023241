using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using DQY5G6_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Client
{
    class MainWindowViewModel
    {
        // http://localhost:37523/

        IDeveloperService developerService;
        ILauncherService launcherService;
        IGameService gameService;

        public RestCollection<Developer> Developers { get; set; }
        public RestCollection<Launcher> Launchers { get; set; }
        public RestCollection<Game> Games { get; set; }

        public ICommand GetDevelopersCommand { get; set; }
        public ICommand GetLaunchersCommand { get; set; }
        public ICommand GetGamesCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Developers = new RestCollection<Developer>("http://localhost:37523/", "Developer", "hub", new List<RestCollection> { Games });
                Launchers = new RestCollection<Launcher>("http://localhost:37523/", "Launcher", "hub", new List<RestCollection> { Games });
                Games = new RestCollection<Game>("http://localhost:37523/", "Game", "hub", new List<RestCollection> { Launchers, Developers });

                developerService = Ioc.Default.GetRequiredService<IDeveloperService>();
                launcherService = Ioc.Default.GetRequiredService<ILauncherService>();
                gameService = Ioc.Default.GetRequiredService<IGameService>();

                GetDevelopersCommand = new RelayCommand(
                    () => developerService.Open(Games),
                    () => true
                    );
                GetLaunchersCommand = new RelayCommand(
                    () => launcherService.Open(Games), 
                    () => true
                    );
                GetGamesCommand = new RelayCommand(
                    () => gameService.Open(Developers, Launchers),
                    () => true
                    );
            }
        }

    }
}
