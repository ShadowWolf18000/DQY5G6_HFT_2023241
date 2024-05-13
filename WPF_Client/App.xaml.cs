using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection().
                AddSingleton<IDeveloperService, DeveloperViaWindow>().
                AddSingleton<ILauncherService, LauncherViaWindow>().
                AddSingleton<IGameService, GameViaWindow>().
                BuildServiceProvider()
                );
        }
    }
}
