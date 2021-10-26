using First_App.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    /// <summary>
    ///     Class that indicates current value of buttons numbers(score).
    /// </summary>
    public static class Counter
    {
        private static short _animationCubesCount = 10;
        public static short AnimationCubesCount
        {
            get => _animationCubesCount;
            set => _animationCubesCount = value;
        }

        // field that stores the max value of numbers
        private static short _score = 4;
        public static short Score
        {
            get => _score;
            set
            {
                try
                {
                    if (value < 4)
                    {
                        throw new ArgumentException("Score cannot be less than 4");
                    }
                    _score = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // field that stores count of pressed cube buttons
        private static short _pressedButtonsCounter = 0;
        public static short PressedButtonsCounter
        {
            get => _pressedButtonsCounter;
            set
            {
                try
                {
                    if (value < 0 || value > _score)
                    {
                        throw new ArgumentException($"Score must have value between {0} and {_score}");
                    }
                    _pressedButtonsCounter = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    // if pressed button counter == Counter.Score then increase score,
                    // reset pressed buttons counter and the re-generate cube buttons
                    if (_pressedButtonsCounter == _score)
                    {
                        // increase score
                        _score++;
                        // reset pressed buttons counter
                        _pressedButtonsCounter = 0;
                        // re-generate cube buttons
                        Game.Create().CreateAndAddCubeButtonToPlayField();
                    }
                }
            }
        }

        public const short MAX_STRIKES = 3;
        // field that stores value of current strikes
        private static short _strikes = 1;
        public static short Strikes
        {
            get => _strikes;
            set
            {
                try
                {
                    if (value < 0 && value > 3)
                    {
                        throw new ArgumentException("Strikes must have value between 1 and 3");
                    }
                    _strikes = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
