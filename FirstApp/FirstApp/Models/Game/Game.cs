using First_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace First_App.Models.Game
{
    public class Game : IGameMediator
    {
        private List<Cube> _cubes = new();
        private NumberGenerator _numberGenerator;

        public Game ()
        {
            InitializeGameCubes();
        }
        
        private void InitializeGameCubes ()
        {
            _numberGenerator.GenerateNumbersForCubes(_cubes);
        }

        public void Notify (object sender, string ev)
        {
            if (ev == "A")
            {
                Console.WriteLine("Mediator reacts on A and triggers folowing operations:");
                // this._component2.DoC();
            }
            if (ev == "D")
            {
                Console.WriteLine("Mediator reacts on D and triggers following operations:");
                // this._component1.DoB();
                // this._component2.DoC();
            }
        }
    }
}
