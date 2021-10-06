using First_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.Game
{
    /**
     * Base Component class for game components
     * 
     */
    public class GameComponent
    {
        // field for concrete mediator object
        protected IGameMediator _gameMediator;

        /**
         * GameComponent constructor()
         * 
         * @param gameMediator - concrete mediator object
         */
        public GameComponent(IGameMediator gameMediator = null)
        {
            this._gameMediator = gameMediator;
        }

        /**
         * Setter for concrete mediator object
         * 
         * @param gameMediator - concrete mediator object
         */
        public void SetGameMediator(IGameMediator gameMediator)
        {
            this._gameMediator = gameMediator;
        }
    }
}
