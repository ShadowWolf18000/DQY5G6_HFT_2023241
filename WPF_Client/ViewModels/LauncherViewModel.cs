using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DQY5G6_HFT_2023241.Models;
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
    class LauncherViewModel : ObservableRecipient
    {
        public bool IsSomethingSelected { get; set; } = false;
        public RestCollection<Launcher> Launchers { get; set; }
        public RestCollection<Game> Games { get; set; }

        private Launcher selectedLauncher;

        public Launcher SelectedLauncher
        {
            get { return selectedLauncher; }
            set
            {
                if (value != null)
                {
                    selectedLauncher = new Launcher(value.LauncherID, value.LauncherName, value.Owner, value.Games);
                    IsSomethingSelected = true;
                    OnPropertyChanged();
                }
                else
                {
                    SelectedLauncher = new Launcher();
                    IsSomethingSelected = false;
                }
                (DeleteLauncherCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateLauncherCommand as RelayCommand)?.NotifyCanExecuteChanged();

            }
        }

        public ICommand CreateLauncherCommand { get; set; }
        public ICommand DeleteLauncherCommand { get; set; }
        public ICommand UpdateLauncherCommand { get; set; }

        public LauncherViewModel() { }

        public LauncherViewModel(RestCollection<Launcher> launchers, RestCollection<Game> games)
        {
            if (!IsInDesignMode)
            {
                Launchers = launchers;
                Games = games;

                CreateLauncherCommand = new RelayCommand(
                    () => Launchers.Add(new Launcher(SelectedLauncher.LauncherID, SelectedLauncher.LauncherName, SelectedLauncher.Owner, SelectedLauncher.Games))
                    );

                DeleteLauncherCommand = new RelayCommand(
                    async () =>
                    {
                        await Launchers.Delete(SelectedLauncher.LauncherID);
                        await Games.Refresh();
                        IsSomethingSelected = false;
                    },
                    () => IsSomethingSelected == true
                    );

                UpdateLauncherCommand = new RelayCommand(
                    () => Launchers.Update(SelectedLauncher),
                    () => IsSomethingSelected == true
                    );
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
