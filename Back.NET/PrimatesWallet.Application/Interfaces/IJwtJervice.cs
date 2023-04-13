using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IJwtJervice
    {
        /// <summary>
        /// Generates a JSON Web Token for the given user.
        /// </summary>
        /// <param name="user">The user for which to generate the JWT.</param>
        /// <returns>A JWT.</returns>
        string Generate(User user);
    }
}
