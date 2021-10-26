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
    ///     Class of cube buttons.
    /// </summary>
    public class Cube
    {
        // field of cube button number
        private short _value;
        public short Value
        {
            get => _value;
            set
            {
                try
                {
                    if (value < 1 && value > Counter.Score)
                    {
                        throw new ArgumentException($"Value must have value between 1 and {Counter.Score}");
                    }
                    _value = value;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // field of cube button coords
        private Point _coords = new ();
        public Point Coords
        {
            get => _coords;
            set
            {
                try
                {
                    if (value.X < 0 || value.Y < 0)
                    {
                        throw new ArgumentException("Point cannot have values less than 0");
                    }
                    _coords.X = value.X;
                    _coords.Y = value.Y;
                }
                catch (ArgumentException ex)
                {
                    MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public Cube () {}

        /// <summary>
        ///     Cube constructor(value).
        /// </summary>
        /// <param name="value">Number of button.</param>
        public Cube (short value)
        {
            this._value = value;
        }
    }
}
