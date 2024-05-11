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
    class DeveloperViewModel : ObservableRecipient
    {
		private int developerID; public int DeveloperID
		{
			get { return developerID; }
			set { SetProperty(ref developerID, value); }
		}
		private string developerName; public string DeveloperName
		{
			get { return developerName; }
			set { SetProperty(ref developerName, value); }
		}
		private int foundingYear; public int FoundingYear
		{
			get { return foundingYear; }
			set { SetProperty(ref foundingYear, value); }
		}

        public bool IsSomethingSelected { get; set; } = false;
        public RestCollection<Developer> Developers { get; set; }
        public RestCollection<Launcher> Launchers { get; set; }
        public RestCollection<Game> Games { get; set; }

        private Developer selectedDeveloper;

        public Developer SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set {
                if (value != null)
                {
                    selectedDeveloper = new Developer(value.DeveloperID, value.DeveloperName, value.FoundingYear, value.Games);
                    DeveloperID = value.DeveloperID;
                    DeveloperName = value.DeveloperName;
                    FoundingYear = value.FoundingYear;

                    IsSomethingSelected = true;
                    OnPropertyChanged();
                }
                else
                {
                    SelectedDeveloper = new Developer();
                    IsSomethingSelected = false;
                }
                (DeleteDeveloperCommand as RelayCommand)?.NotifyCanExecuteChanged();
                (UpdateDeveloperCommand as RelayCommand)?.NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateDeveloperCommand { get; set; }
        public ICommand UpdateDeveloperCommand { get; set; }
        public ICommand DeleteDeveloperCommand { get; set; }

        public DeveloperViewModel() { }

        public DeveloperViewModel(RestCollection<Developer> developers, RestCollection<Launcher> launchers, RestCollection<Game> games)
        {
            if (!IsInDesignMode)
            {
                Developers = developers;
                Launchers = launchers;
                Games = games;

                CreateDeveloperCommand = new RelayCommand(
                    () => Developers.Add(new Developer(SelectedDeveloper.DeveloperID, SelectedDeveloper.DeveloperName, SelectedDeveloper.FoundingYear)));
                // ICollection<Game>??

                DeleteDeveloperCommand = new RelayCommand(
                    async ()=>
                    {
                        await Developers.Delete(SelectedDeveloper.DeveloperID);
                        await Launchers.Refresh();
                        await Games.Refresh();
                        IsSomethingSelected = false;
                    },
                    () => IsSomethingSelected == true
                    );

                UpdateDeveloperCommand = new RelayCommand(
                    () => Developers.Update(SelectedDeveloper),
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
