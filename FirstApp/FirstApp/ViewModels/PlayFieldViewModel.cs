using First_App.Models.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace First_App.ViewModels
{
    class PlayFieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // field of game play
        private Game _game;
        private ObservableCollection<Button> _playGrid { get; set; } = new();
        public ObservableCollection<Button> PlayGrid
        {
            get => _playGrid;
            set
            {
                _playGrid = value;
            }
        }
        public PlayFieldViewModel()
        {
            //  call Game constructor() and initialize game cubes
            InitializeGameField();

            _game.StartGame(PlayGrid);
            OnPropertyChanged("PlayGrid");
        }
        private void InitializeGameField()
        {
            _game = new();
        }
    }
}
