using First_App.Interfaces;
using First_App.Models.Commands;
using FirstApp;
using System;
using System.Collections.Generic;
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
        private List<Cube> _cubes = new();
        // generator of numbers at cube buttons
        private NumberGenerator _numberGenerator = new();
        // generator of coords for cube buttons on the play field
        private CoordsGenerator _coordsGenerator = new();
        // chimp window
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        /// <summary>
        ///     Game constructor().
        ///     Initializing cube buttons of number and coords.
        /// </summary>
        public Game ()
        {
            InitializeGameCubes();
        }

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

                ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[0];
                newButton.Style = (Style)resourceDictionary["cubeButton"];


                // set event handler after clicking to cube button
                newButton.Click += DeleteButton_Executed;

                // set row on the play grid for button
                Grid.SetRow(newButton, Convert.ToInt32(_cubes[i].Coords.Y));
                // set column on the play grid for button
                Grid.SetColumn(newButton, Convert.ToInt32(_cubes[i].Coords.X));

                // add button to play grid
                _chimpWindow.playGrid.Children.Add(newButton);
            }
        }

        /// <summary>
        ///     Event handler of clicking cube button to remove it from the play grid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arguments</param>
        private void DeleteButton_Executed(object sender, RoutedEventArgs e)
        {
            // explicit cast from RoutedEventArgs to Button
            Button button = (Button)e.Source;
            // remove cube button from the play grid after clicking on it
            _chimpWindow.playGrid.Children.Remove(button);
        }

        /// <summary>
        ///         
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
