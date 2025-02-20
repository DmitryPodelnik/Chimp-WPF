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
    ///     Class of generating coords for cube buttons on the play grid.
    /// </summary>
    public class CoordsGenerator
    {
        // object for generate random coords for cube buttons on the play grid
        private Random _random = new();
        // already existing buttons with concrete coords
        private List<Point> _existingCoords = new();

        /// <summary>
        ///     Generate unique coords for every cube button on the play grid.
        /// </summary>
        /// <param name="cubes">Collection of cube buttons on the play grid.</param>
        public void GenerateCoordsForCubes(ObservableCollection<Cube> cubes, short count = -1)
        {
            try
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

                // field for checking if button with the same coords already exists
                bool check;
                for (int i = 0; i < amount; i++)
                {
                    // generate new coords while it is generating not unique coords
                    do
                    {
                        check = false;
                        // create new coords for cube button
                        cubes[i].Coords = new Point(_random.Next(0, Game.COLUMNS), _random.Next(0, Game.ROWS));

                        // check for unique coords
                        foreach (var item in _existingCoords)
                        {
                            // if not unique then generate again
                            if (cubes[i].Coords.X == item.X && cubes[i].Coords.Y == item.Y)
                            {
                                check = true;
                                break;
                            }
                        }
                    } while (check);

                    // add button to already existing cube buttons with concrete coords collection
                    _existingCoords.Add(cubes[i].Coords);
                }
                // clear existing coords collection
                _existingCoords.Clear();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBoxWindow.Create().ShowMessageBox(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
