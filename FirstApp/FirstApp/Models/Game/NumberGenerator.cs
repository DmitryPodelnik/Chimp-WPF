using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    public class NumberGenerator : GameComponent
    {
        private Counter _counter;

        private short _generatedNumber;
        public short GeneratedNumber 
        {
            get => _generatedNumber;
            set
            {
                try
                {
                    if (value < 1 && value > _counter.Score)
                    {
                        throw new ArgumentException("Score must have value between 1 and number score");
                    }
                    _generatedNumber = value;
                    this._gameMediator.Notify(this, $"Generator generated value: {value}");
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        private static short _minGenerableNumber = 1;

        public NumberGenerator (Counter counter)
        {
            this._counter = counter;
        }

        public void GenerateNumbersForCubes (IList<Cube> cubes)
        {
            for (int i = 1; i <= _counter.Score; i++)
            {
                cubes.Add(new Cube(i));
            }
        }
    }
}
