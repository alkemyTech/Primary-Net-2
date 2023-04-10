using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IUserContextService
    {
        /// <summary>
        /// Gets the ID of the currently authenticated user.
        /// </summary>
        /// <returns>The ID of the currently authenticated user.</returns>
        int GetCurrentUser();
    }
}
