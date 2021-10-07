using First_App.Models;
using First_App.Models.Commands;
using First_App.Models.DataBase;
using First_App.Models.Game;
using First_App.Models.RegistryData;
using FirstApp.Models.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace First_App.ViewModels
{
    /// <summary>
    ///     Class of chimp view model.
    /// </summary>
    public class ChimpViewModel : INotifyPropertyChanged
    {
        // field of main window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;
        // field of game play
        private Game _game;

        public ChimpViewModel()
        {

        }

        /// <summary>
        ///     Command after clicking start game button.
        /// </summary>
        private RelayCommand _startGameCommand;
        public RelayCommand StartGameCommand
        {
            get
            {
                return _startGameCommand =
                (_startGameCommand = new RelayCommand(obj =>
                {
                    _chimpWindow.startGamePanel.Visibility = Visibility.Hidden;
                    //  call Game constructor() and initialize game cubes
                    InitializeGameField();

                    _game.StartGame();
                }));
            }
        }

        /// <summary>
        ///     Hide panels excepting main tab.
        /// </summary>
        //private void ShowMainTab()
        //{
        //    _chimpWindow.accountNameTextBlock.Text = "";
        //    _chimpWindow.mainText.Visibility = Visibility.Visible;
        //    _chimpWindow.playGrid.Visibility = Visibility.Hidden;
        //    _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        //}

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        private void StartPlay ()
        {
            // Hide panels excepting play tab
            PrepareInterfaceToPlay();
        }

        private void InitializeGameField()
        {
            _game = new();
        }

        /// <summary>
        ///     Hide panels excepting play tab.
        /// </summary>
        //private void PrepareInterfaceToPlay()
        //{
        //    _chimpWindow.accountNameTextBlock.Text = "";
        //    _chimpWindow.playGrid.Visibility = Visibility.Visible;
        //    _chimpWindow.mainText.Visibility = Visibility.Hidden;
        //    _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.recordsGrid.Visibility = Visibility.Hidden;
        //}

        /// <summary>
        ///     Hide panels excepting records tab.
        /// </summary>
        //private void ShowRecords ()
        //{
        //    _chimpWindow.accountNameTextBlock.Text = "";
        //    _chimpWindow.recordsGrid.Visibility = Visibility.Visible;
        //    _chimpWindow.authorizationPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.accountPanel.Visibility = Visibility.Hidden;
        //    _chimpWindow.playGrid.Visibility = Visibility.Hidden;
        //    _chimpWindow.mainText.Visibility = Visibility.Hidden;
        //}

        /// <summary>
        ///     Disable left buttons.
        /// </summary>
        private void DisableLeftButtonsBeforeAuth ()
        {
            //_chimpWindow.recordsButton.IsEnabled = false;
            //_chimpWindow.profileButton.IsEnabled = false;
            //_chimpWindow.playButton.IsEnabled = false;
            //_chimpWindow.mainTabButton.IsEnabled = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged ([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
