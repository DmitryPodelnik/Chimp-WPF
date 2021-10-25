using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.Game
{
    class MainAnimation
    {
        // cube buttons with number, which must be pressed
        private ObservableCollection<Cube> _cubes = new();
        // generator of numbers at cube buttons
        private NumberGenerator _numberGenerator = new();
        // generator of coords for cube buttons on the play field
        private CoordsGenerator _coordsGenerator = new();

        public MainAnimation()
        {
            InitializeGameCubes();
        }

        /// <summary>
        ///     Generate numbers and coords for cube buttons and initialize them.
        /// </summary>
        private void InitializeGameCubes()
        {
            // initializing cube collection of numbers
            _numberGenerator.GenerateNumbersForCubes(_cubes, Counter.AnimationCubesCount);
            // initializing cube collection of coords on the play grid
            _coordsGenerator.GenerateCoordsForCubes(_cubes, Counter.AnimationCubesCount);
        }
    }
}
