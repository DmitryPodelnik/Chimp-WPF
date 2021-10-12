using First_App.Interfaces;
using First_App.Models.Commands;
using First_App.Navigation;
using First_App.ViewModels;
using First_App.Views;
using FirstApp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace First_App.Models.Game
{
    /// <summary>
    ///     Concrete class, that implements IGameMediator
    /// </summary>
    public class Game : IGameMediator
    {
        // rows on the play grid
        private const short _ROWS = 8;
        // columns on the play grid
        private const short _COLUMNS = 10;

        // indicate if game is started
        private static bool _isGameStarted = false;
        public static bool IsGameStarted
        {
            get => _isGameStarted;
            set => _isGameStarted = value;
        }

        public static short ROWS { get => _ROWS; }
        public static short COLUMNS { get => _COLUMNS; }

        // cube buttons with number, which must be pressed
        private ObservableCollection<Cube> _cubes = new();
        // generator of numbers at cube buttons
        private NumberGenerator _numberGenerator = new();
        // generator of coords for cube buttons on the play field
        private CoordsGenerator _coordsGenerator = new();
        // chimp window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        private static short _previousNumber = 0;

        // collection of play grid cube buttons
        private static ObservableCollection<Button> _playGridCubeButtons { get; set; } = new();
        public static ObservableCollection<Button> PlayGridCubeButtons
        {
            get => _playGridCubeButtons;
            set
            {
                _playGridCubeButtons = value;
            }
        }

        private static Game _instance = null;
        public static Game Create()
        {
            if (_instance == null)
            {
                _instance = new Game();
            }
            return _instance;
        }

        /// <summary>
        ///     Create or singlton instance of game class.
        /// </summary>
        /// <param name="playGrid">Cube buttons collection.</param>
        /// <returns>Existing or not instance of game class.</returns>
        public static Game Create(ObservableCollection<Button> playGrid)
        {
            if (_instance == null)
            {
                _instance = new Game(playGrid);
            }
            _playGridCubeButtons = playGrid;
            return _instance;
        }

        protected Game() { }

        /// <summary>
        ///     Game constructor().
        ///     Initializing cube buttons of number and coords.
        /// </summary>
        protected Game(ObservableCollection<Button> playGrid)
        {
            _playGridCubeButtons = playGrid;
        }

        /// <summary>
        ///     Generate numbers and coords for cube buttons and initialize them.
        /// </summary>
        private void InitializeGameCubes()
        {
            // initializing cube collection of numbers
            _numberGenerator.GenerateNumbersForCubes(_cubes);
            // initializing cube collection of coords on the play grid
            _coordsGenerator.GenerateCoordsForCubes(_cubes);
        }

        /// <summary>
        ///     Start game after creating play field.
        ///     Play field initialization with cube buttons.
        /// </summary>
        public void StartGame()
        {
            try
            {
                do
                {
                    CreateAndAddCubeButtonToPlayField();
                } while (false);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     Create a cube buttons, initialize and add them to the play grid.
        /// </summary>
        public void CreateAndAddCubeButtonToPlayField()
        {
            if (_cubes.Count != 0)
            {
                _cubes.Clear();
                _previousNumber = 0;
                Counter.PressedButtonsCounter = 0;
            }
            InitializeGameCubes();

            CreateCubeButtonsAtPlayField();
        }

        /// <summary>
        ///     Create cube buttons at play field.
        /// </summary>
        private void CreateCubeButtonsAtPlayField()
        {
            for (int i = 0; i < Counter.Score; i++)
            {
                // create new cube Button
                Button newButton = new();
                // assign a cube number
                newButton.Content = _cubes[i].Value;
                // assign new font size
                newButton.FontSize = 40.0;
                // set new button x:Name
                newButton.Name = $"playButton{i}";

                // get resource dictionary with cubeButton styles
                ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[0];

                // set cubeButton style to the button
                newButton.Style = (Style)resourceDictionary["cubeButton"];

                // set event handler after clicking to cube button
                newButton.Click += DeleteButton_Executed;

                // set row on the play grid for button
                Grid.SetRow(newButton, Convert.ToInt32(_cubes[i].Coords.Y));
                // set column on the play grid for button
                Grid.SetColumn(newButton, Convert.ToInt32(_cubes[i].Coords.X));

                // add button to play grid button collection
                _playGridCubeButtons.Add(newButton);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="btn"></param>
        private void SetWhiteStyleToCubeButtons(ContentControl btn)
        {
            // get resource dictionary with cubeButton styles
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[0];
            // set white text at the button
            btn.Foreground = new SolidColorBrush(Colors.White);
            // set cubeButton style to the button
            btn.Style = (Style)resourceDictionary["cubeWhiteButton"];
        }

        /// <summary>
        ///     Event handler of clicking cube button to remove it from the play grid.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void DeleteButton_Executed(object sender, RoutedEventArgs e)
        {
            // explicit cast from RoutedEventArgs to Button
            Button button = (Button)e.Source;

            // if pressed cube button number is bigger by 1 from the previous number
            if (short.Parse(button.Content.ToString()) == ++_previousNumber)
            {
                // remove cube button from the play grid after clicking on it
                _playGridCubeButtons.Remove(button);
                Counter.PressedButtonsCounter++;

                if (Counter.Score > 4 && Counter.PressedButtonsCounter > 0)
                {
                    foreach (var btn in _playGridCubeButtons)
                    {
                        SetWhiteStyleToCubeButtons(btn);
                    }
                }
                return;
            }
            _previousNumber = 0;
            Counter.PressedButtonsCounter = 0;
            if (Counter.Strikes == Counter.MAX_STRIKES)
            {
                Counter.Strikes = 1;
                Navigator.Create().CurrentViewModel = new FinishGameTableViewModel();
                Counter.Score = 4;
                return;
            }
            Navigator.Create().CurrentViewModel = new ScoreTableViewModel();
        }

        /// <summary>
        ///     Temporarily not working.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="ev">event info</param>
        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                // Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                // this._component2.DoC();
            }
            if (ev == "D")
            {
                // Console.WriteLine("Mediator reacts on D and triggers following operations:");
                // this._component1.DoB();
                // this._component2.DoC();
            }
        }
    }
}
