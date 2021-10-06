using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    /// <summary>
    ///     Class of generating number for cube buttons.
    /// </summary>
    public class NumberGenerator : GameComponent
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
                   // this._gameMediator.Notify(this, $"Generator generated value: {value}");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
        // min number in cube buttons
        private short _minGenerableNumber = 1;

        /// <summary>
        ///     NumberGenerator constructor().
        /// </summary>
        public NumberGenerator ()
        {

        }

        /// <summary>
        ///     Generate number for cube buttons
        ///     starting from minGenerableNumber to Counter.Score.
        /// </summary>
        /// <param name="cubes">Collection of cube buttons.</param>
        public void GenerateNumbersForCubes (IList<Cube> cubes)
        {
            for (; _minGenerableNumber <= Counter.Score; _minGenerableNumber++)
            {
                // add new cube to collection with minGenerableNumber number
                cubes.Add(new Cube(_minGenerableNumber));
            }
        }
    }
}
