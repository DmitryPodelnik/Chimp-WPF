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
        private Counter _counter = new();
        private NumberGenerator _numberGenerator;

        public Game (Counter counter)
        {
            //this._counter = counter;
            this._numberGenerator = new(_counter);
        }
        
        private void InitializeGameCubes ()
        {
            _numberGenerator.GenerateNumbers();
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
