using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    public class Cube : GameComponent
    {
        private short _value;
        public short Value
        {
            get => _value;
            set
            {
                try
                {
                    if (value < 1)
                    {
                        throw new ArgumentException("Value cannot be less than 1");
                    }
                    _value = value;
                    this._gameMediator.Notify(this, $"Value was changed to {value}");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private Point _coords = new();
        public Point Coords
        {
            get => _coords;
            set
            {
                try
                {
                    if (value.X < 0 || value.Y <0)
                    {
                        throw new ArgumentException("Point cannot have values less than 0");
                    }
                    _coords.X = value.X;
                    _coords.Y = value.Y;
                    this._gameMediator.Notify(this, $"Point values were changed: X: {value.X}, Y: {value.Y}");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }
    }
}
