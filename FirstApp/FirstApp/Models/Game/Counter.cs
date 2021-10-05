using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    public static class Counter
    {
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
    }
}
