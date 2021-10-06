using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Interfaces
{
    /// <summary>
    ///     Mediator interface.
    /// </summary>
    public interface IGameMediator
    {
        void Notify(object sender, string ev);
    }
}
