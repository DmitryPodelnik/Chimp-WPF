﻿using First_App.Views.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    /// <summary>
    ///     Class of generating number for cube buttons.
    /// </summary>
    public class NumberGenerator
    {
        private short _generatedNumber;
        public short GeneratedNumber
        {
            get => _generatedNumber;
            set
            {
                try
                {
                    if (value < 1 && value > Counter.Score)
                    {
                        throw new ArgumentException("Score must have value between 1 and number score");
                    }
                    _generatedNumber = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        // min number in cube buttons
        private static short _minGenerableNumber = 1;
        public static short MinGenerableNumber
        {
            get => _minGenerableNumber;
            set => _minGenerableNumber = value;
        }

        /// <summary>
        ///     NumberGenerator constructor().
        /// </summary>
        public NumberGenerator () {}

        /// <summary>
        ///     Generate number for cube buttons
        ///     starting from minGenerableNumber to Counter.Score.
        /// </summary>
        /// <param name="cubes">Collection of cube buttons.</param>
        public void GenerateNumbersForCubes (ObservableCollection<Cube> cubes, short count = -1)
        {
            short amount;
            if (count == -1)
            {
                amount = Counter.Score;
            }
            else
            {
                amount = count;
            }
            for (; _minGenerableNumber <= amount; _minGenerableNumber++)
            {
                // add new cube to collection with minGenerableNumber number
                cubes.Add(new Cube(_minGenerableNumber));
            }
            // reset min generable number to 1
            _minGenerableNumber = 1;
        }
    }
}
