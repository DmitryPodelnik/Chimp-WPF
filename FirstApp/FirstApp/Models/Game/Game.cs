using First_App.Interfaces;
using First_App.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace First_App.Models.Game
{
    public class Game : IGameMediator
    {
        private const short _ROWS = 8;
        private const short _COLUMNS = 10;

        private static bool _isGameStarted = false;
        public static bool IsGameStarted
        {
            get => _isGameStarted;
            set => _isGameStarted = value;
        }

        public static short ROWS { get => _ROWS; }
        public static short COLUMNS { get => _COLUMNS; }

        private List<Cube> _cubes = new();
        private NumberGenerator _numberGenerator = new();
        private CoordsGenerator _coordsGenerator = new();
        private Chimp _chimpWindow = (Chimp)Application.Current.MainWindow;

        public Game ()
        {
            InitializeGameCubes();
        }

        private RelayCommand _deleteButtonCommand;
        public RelayCommand DeleteButtonCommand
        {
            get
            {
                return _deleteButtonCommand =
                (_deleteButtonCommand = new RelayCommand(obj =>
                {
                    MessageBox.Show("Hello");
                }));
            }
        }

        private void InitializeGameCubes()
        {
            _numberGenerator.GenerateNumbersForCubes(_cubes);
            _coordsGenerator.GenerateCoordsForCubes(_cubes);
        }

        public void StartGame()
        {
            for (int i = 0; i < Counter.Score; i++)
            {
                Button newButton = new();
                newButton.Content = _cubes[i].Value;
                newButton.Name = $"playButton{i}";

                //// создаем привязку команды
                //CommandBinding commandBinding = new();
                //// устанавливаем команду
                //commandBinding.Command = DeleteButtonCommand;
                //// устанавливаем метод, который будет выполняться при вызове команды
                //commandBinding.Executed += DeleteButton_Executed;
                //// добавляем привязку к коллекции привязок элемента Button
                //newButton.CommandBindings.Add(commandBinding);

                newButton.Click += DeleteButton_Executed;

                Grid.SetRow(newButton, Convert.ToInt32(_cubes[i].Coords.Y));
                Grid.SetColumn(newButton, Convert.ToInt32(_cubes[i].Coords.X));
                _chimpWindow.playGrid.Children.Add(newButton);
            }
        }

        private void DeleteButton_Executed(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.Source;
            _chimpWindow.playGrid.Children.Remove(button);
        }

        public void Notify(object sender, string ev)
        {
            if (ev == "A")
            {
                // Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                // this._component2.DoC();
            }
            if (ev == "D")
            {
                // Console.WriteLine("Mediator reacts on D and triggers following operations:");
                // this._component1.DoB();
                // this._component2.DoC();
            }
        }
    }
}
