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
    ///     Concrete class, that implements IGameMediator.
    /// </summary>
    public class Game
    {
        // rows on the play grid
        private const short _ROWS = 8;
        // columns on the play grid
        private const short _COLUMNS = 10;
        // field that stores finish score
        public static short lastScore;
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
        // field that stores a previous number of pressed cube button
        private static short _previousButtonNumber = 0;

        // collection of play grid cube buttons
        private static ObservableCollection<Button> _playGridCubeButtons { get; set; } = new();
        public static ObservableCollection<Button> PlayGridCubeButtons
        {
            get => _playGridCubeButtons;
            set => _playGridCubeButtons = value;
        }

        // realizing singleton instance
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
        ///     Play field initialization of cube buttons.
        /// </summary>
        public void StartGame()
        {
            try
            {
                // create a cube buttons, initialize and add them to the play grid
                CreateAndAddCubeButtonToPlayField();
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
            // if current buttons are not 0, then
            if (_cubes.Count != 0)
            {
                // clear cube button collection
                _cubes.Clear();
                // reset previous button number
                _previousButtonNumber = 0;
                // reset pressed buttons counter
                Counter.PressedButtonsCounter = 0;
            }
            // generate numbers and coords for cube buttons and initialize them.
            InitializeGameCubes();
            // create cube buttons at play field.
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

                // get resource dictionary with styles
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
        ///     Set white style to cube buttons.
        /// </summary>
        /// <param name="btn"></param>
        private void SetWhiteStyleToCubeButtons(ContentControl btn)
        {
            // get resource dictionary with styles
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
            try
            {
                // explicit cast from RoutedEventArgs to Button
                Button button = (Button)e.Source;
                // if pressed cube button number is bigger by 1 from the previous number
                if (short.Parse(button.Content.ToString()) == ++_previousButtonNumber)
                {
                    // remove cube button from the play grid after clicking on it
                    _playGridCubeButtons.Remove(button);
                    // increase pressed buttons counter
                    Counter.PressedButtonsCounter++;

                    if (Counter.Score > 4 && Counter.PressedButtonsCounter > 0)
                    {
                        // set style for each button in play grid cube button collection
                        foreach (var btn in _playGridCubeButtons)
                        {
                            // set white style to cube buttons
                            SetWhiteStyleToCubeButtons(btn);
                        }
                    }
                    return;
                }
                // reset previous button number
                _previousButtonNumber = 0;
                // reset pressed button counter
                Counter.PressedButtonsCounter = 0;
                // if current strike == MAX_STRIKES (3)
                if (Counter.Strikes == Counter.MAX_STRIKES)
                {
                    // reset strikes
                    Counter.Strikes = 1;
                    // change to finish game tab
                    Navigator.Create().CurrentViewModel = new FinishGameTableViewModel();
                    lastScore = Counter.Score;
                    // reset score
                    Counter.Score = 4;

                    return;
                }
                // else change to score table tab
                Navigator.Create().CurrentViewModel = new ScoreTableViewModel();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
