using DQY5G6_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPF_Client
{
    class GameViewModel : ObservableRecipient
    {
		//private int gameID; public int GameId
		//{
		//	get { return gameID; }
		//	set { gameID = value; }
		//}
		//private string title; public string Title
		//{
		//	get { return title; }
		//	set { title = value; }
		//}
		//private int developerID; public int DeveloperID
		//{
		//	get { return developerID; }
		//	set { developerID = value; }
		//}
		//private int launcherID; public int LauncherID
		//{
		//	get { return launcherID; }
		//	set { launcherID = value; }
		//}
		//private double rating; public double Rating
		//{
		//	get { return rating; }
		//	set { rating = value; }
		//}

        public bool IsSomethingSelected { get; set; } = false;
        public RestCollection<Developer> Developers { get; set; }
        public RestCollection<Launcher> Launchers { get; set; }
        public RestCollection<Game> Games { get; set; }
		
		private Game selectedGame;

		public Game SelectedGame
		{
			get { return selectedGame; }
			set 
			{
				if (value != null)
				{
					selectedGame = new Game(value.GameID, value.Title, value.DeveloperID, value.LauncherID, value.Rating, value.Developer, value.Launcher);
					IsSomethingSelected = true;
					OnPropertyChanged();
				}
				else
				{
					SelectedGame = new Game();
					IsSomethingSelected = false;
				}
				(DeleteGameCommand as RelayCommand)?.NotifyCanExecuteChanged();
				(UpdateGameCommand as RelayCommand)?.NotifyCanExecuteChanged();
			}
		}

        public ICommand CreateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }

        public GameViewModel() { }

        public GameViewModel(RestCollection<Developer> developers, RestCollection<Launcher> launchers, RestCollection<Game> games)
        {
            if (!IsInDesignMode)
			{
				Developers = developers;
				Launchers = launchers;
				Games = games;

				CreateGameCommand = new RelayCommand(
					() => Games.Add(new Game(SelectedGame.GameID, SelectedGame.Title, SelectedGame.DeveloperID, SelectedGame.LauncherID, SelectedGame.Rating, SelectedGame.Developer, SelectedGame.Launcher))
					);

				DeleteGameCommand = new RelayCommand(
					async () =>
					{
						await Games.Delete(SelectedGame.GameID);
						await Developers.Refresh();
						await launchers.Refresh();
						IsSomethingSelected = false;
					},
					() => IsSomethingSelected == true
					);

				UpdateGameCommand = new RelayCommand(
					() => Games.Update(SelectedGame),
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
