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

        // field that stores the value of numbers
        private static short _pressedNumbersCounter = 0;
        public static short PressedNumbersCounter
        {
            get => _pressedNumbersCounter;
            set
            {
                try
                {
                    if (value < 0 || value > _score)
                    {
                        throw new ArgumentException($"Score must have value between {0} and {_score}");
                    }
                    _pressedNumbersCounter = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
                finally
                {
                    if (_pressedNumbersCounter == _score)
                    {
                        _pressedNumbersCounter = 0;
                    }
                }
            }
        }
    }
}
