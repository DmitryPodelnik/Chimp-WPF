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
        private static Game _game = Game.Create();

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
                    MessageBox.Show(ex.Message, "Error");
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
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    if (_pressedButtonsCounter == _score)
                    {
                        _score++;
                        _pressedButtonsCounter = 0;
                        _game.CreateAndAddCubeButtonToPlayField();
                    }
                }
            }
        }
    }
}
