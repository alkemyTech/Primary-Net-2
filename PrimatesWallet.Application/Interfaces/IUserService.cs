using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<int> Signup(RegisterUserDTO user);
        
        /// <summary>
        /// Service to retrieve a paged list of users.
        /// </summary>
        /// <param name="page">The page number to return.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns a list of users.</returns>
        /// <exception cref="AppException">Thrown if no users are found.</exception>
        Task<IEnumerable<UserResponseDTO>> GetUsers(int page,int pageSize);

        /// <summary>
        /// Obtains the total number of pages of users for a paged list.
        /// </summary>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns the total number of pages.</returns>
        Task<int> TotalPageUsers(int PageSize);
    }
}
