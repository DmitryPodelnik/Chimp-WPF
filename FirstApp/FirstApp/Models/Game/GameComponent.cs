using First_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.Game
{
    public class GameComponent
    {
        protected IGameMediator _gameMediator;

        public GameComponent(IGameMediator gameMediator = null)
        {
            this._gameMediator = gameMediator;
        }

        public void SetGameMediator(IGameMediator gameMediator)
        {
            this._gameMediator = gameMediator;
        }
    }
}
