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
        Task<int> Signup(RegisterUserDto user);
        
        /// <summary>
        /// Service to retrieve a paged list of users.
        /// </summary>
        /// <param name="page">The page number to return.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns a list of users.</returns>
        /// <exception cref="AppException">Thrown if no users are found.</exception>
        Task<IEnumerable<UserResponseDto>> GetUsers(int page,int pageSize);

        /// <summary>
        /// Obtains the total number of pages of users for a paged list.
        /// </summary>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A task that returns the total number of pages.</returns>
        Task<int> TotalPageUsers(int PageSize);

        Task<bool> DeleteUser(int userId);
        Task<UserResponseDto> GetUserById(int id);
        Task<string> ActivateUser(int userId);
        Task<bool> UpdateUser(int UserId, UserUpdateDto userUpdateDTO);

        /// <summary>
        /// Updates the points of a user by subtracting the specified amount.
        /// </summary>
        /// <param name="userId">The ID of the user whose points will be updated.</param>
        /// <param name="points">The number of points to subtract from the user's balance.</param>
        /// <exception cref="AppException">Thrown when the user does not have enough points.</exception>
        Task UpdatePoints(int userId, int points);
    }
}
