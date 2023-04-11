using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user with the provided credentials.
        /// </summary>
        /// <param name="login">The user credentials to authenticate.</param>
        /// <returns>The authenticated user.</returns>
        Task<User> Authenticate(LoginUserDto login);
    }
}
