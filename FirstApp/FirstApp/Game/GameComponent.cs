using First_App.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.Game
{
    /// <summary>
    ///     Base Component class for game components.
    /// </summary>
    public class GameComponent
    {
        // field for concrete mediator object
        protected IGameMediator _gameMediator;

        /// <summary>
        ///     GameComponent constructor().
        /// </summary>
        /// <param name="gameMediator">Concrete mediator object.</param>
        public GameComponent(IGameMediator gameMediator = null)
        {
            this._gameMediator = gameMediator;
        }

        /// <summary>
        ///     Setter for concrete mediator object
        /// </summary>
        /// <param name="gameMediator">Concrete mediator object.</param>
        public void SetGameMediator(IGameMediator gameMediator)
        {
            this._gameMediator = gameMediator;
        }
    }
}
