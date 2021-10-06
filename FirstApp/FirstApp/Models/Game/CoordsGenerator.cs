using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    /**
     * Class of generating coords for cube buttons on the play grid
     * 
     */
    public class CoordsGenerator : GameComponent
    {
        // object for generate random coords for cube buttons on the play grid
        private Random _random = new();
        // already existing buttons with concrete coords
        private List<Point> _existedCoords = new();

        /**
         * Generate unique coords for every cube button on the play grid
         * 
         * @param cubes - collection of cube buttons on the play grid
         */
        public void GenerateCoordsForCubes(IList<Cube> cubes)
        {
            // field for checking if button with the same coords already exists
            bool check = false;
            for (int i = 0; i < Counter.Score; i++)
            {
                // generate new coords while it is generating not unique coords
                do
                {
                    // create new coords for cube button
                    cubes[i].Coords = new Point(_random.Next(0, Game.COLUMNS), _random.Next(0, Game.ROWS));

                    // check for unique coords
                    foreach (var item in _existedCoords)
                    {
                        // if not unique then generate again
                        if (cubes[i].Coords.X == item.X && cubes[i].Coords.Y == item.Y)
                        {
                            check = true;
                            break;
                        }
                    }
                    // if coords are unique - break loop
                    check = false;
                } while (check);

                // add button to already existing cube buttons with concrete coords collection
                _existedCoords.Add(cubes[i].Coords);
            }
        }
    }
}
