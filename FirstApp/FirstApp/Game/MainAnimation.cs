﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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


        // collection of play grid cube buttons
        private static ObservableCollection<Button> _animationGridCubeButtons { get; set; } = new();
        public static ObservableCollection<Button> AnimationGridCubeButtons
        {
            get => _animationGridCubeButtons;
            set => _animationGridCubeButtons = value;
        }

        protected MainAnimation()
        {
            InitializeGameCubes();
        }

        /// <summary>
        ///     Game constructor().
        ///     Initializing cube buttons of number and coords.
        /// </summary>
        protected MainAnimation(ObservableCollection<Button> animationGrid)
        {
            _animationGridCubeButtons = animationGrid;
        }

        // realizing singleton instance
        private static MainAnimation _instance = null;
        public static MainAnimation Create()
        {
            if (_instance == null)
            {
                _instance = new MainAnimation();
            }
            return _instance;
        }

        /// <summary>
        ///     Create or singlton instance of game class.
        /// </summary>
        /// <param name="playGrid">Cube buttons collection.</param>
        /// <returns>Existing or not instance of game class.</returns>
        public static MainAnimation Create(ObservableCollection<Button> animationGrid)
        {
            if (_instance == null)
            {
                _instance = new MainAnimation(animationGrid);
            }
            _animationGridCubeButtons = animationGrid;

            return _instance;
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

        /// <summary>
        ///     Start game after creating play field.
        ///     Play field initialization of cube buttons.
        /// </summary>
        public void StartAnimation()
        {
            try
            {
                // generate numbers and coords for cube buttons and initialize them
                InitializeGameCubes();
                //  create cube buttons at animation field
                CreateCubeButtonsAtPlayField();
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        ///     Create cube buttons at animation field.
        /// </summary>
        private void CreateCubeButtonsAtPlayField()
        {
            for (int i = 0; i < Counter.AnimationCubesCount; i++)
            {
                // create new cube Button
                Button newButton = new();
                // assign a cube number
                newButton.Content = _cubes[i].Value;
                // assign new font size
                newButton.FontSize = 40.0;
                // set new button x:Name
                newButton.Name = $"playButton{i}";

                // get resource dictionary with styles
                ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries[0];

                // set cubeButton style to the button
                newButton.Style = (Style)resourceDictionary["cubeButton"];

                // set row on the play grid for button
                Grid.SetRow(newButton, Convert.ToInt32(_cubes[i].Coords.Y));
                // set column on the play grid for button
                Grid.SetColumn(newButton, Convert.ToInt32(_cubes[i].Coords.X));

                // add button to play grid button collection
                _animationGridCubeButtons.Add(newButton);
            }
        }
    }
}
