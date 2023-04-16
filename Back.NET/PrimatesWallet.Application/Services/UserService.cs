using AutoMapper;
using BCrypt.Net;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping.User;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

namespace PrimatesWallet.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserResponseDto> GetUserById(int id)
        {
            var user = await unitOfWork.Users.GetById(id);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);
            //si no existe el usuario lanzamos un exception personalizada en otra parte del codigo la atrapamos y le damos un formato
            var response = new UserResponseDto()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Points = user.Points,
                Rol = user.Role.Name,
                UserId = user.UserId,
                Money = user.Account.Money,
                AccountId = user.Account.Id,
                IsAccountBlocked = user.Account.IsBlocked,
            };

            return response;
         }

        public async Task<IEnumerable<UserResponseDto>> GetUsers(int page, int pageSize)
        {
            var users = await unitOfWork.Users.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var usersDTO = users.Select(x => 
                new UserResponseDtoBuilder()
                .WithUserId(x.UserId)
                .WithFirstName(x.First_Name)
                .WithLastName(x.Last_Name)
                .WithEmail(x.Email)
                .WithPoints(x.Points)
                .WithRolId(x.Rol_Id)
                .Build()).ToList();

            return usersDTO;
        }

        public async Task<int> TotalPageUsers(int PageSize)
        {
            var totalUsers = await unitOfWork.Users.GetCount();
            //contamos el total de usuarios y calculamos cuantas paginas hay en total
            return (int)Math.Ceiling((double)totalUsers / PageSize);
        }

        public async Task<int> Signup(RegisterUserDto user)

        {
            var isRegistered = await unitOfWork.Users.IsRegistered(user.Email);

            if (isRegistered) throw new AppException("Email already registered.", HttpStatusCode.BadRequest);

            int salt = 10;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);

            var newUser = new User()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Password = hashedPassword,
            };
           
            await unitOfWork.Users.Add(newUser);
            var response = unitOfWork.Save();

            var userId = await unitOfWork.Users.GetUserIdByEmail(newUser.Email);
            if (userId == 0) throw new AppException($"No user with id {userId}", HttpStatusCode.BadRequest);
            

            if (response > 0)  return userId;
            return userId;
        }


        public async Task <bool> DeleteUser(int userId)
        {
            var user = await unitOfWork.Users.GetById(userId) ?? throw new AppException("User not found", HttpStatusCode.NotFound);
            unitOfWork.Users.Delete(user);
            unitOfWork.Save();
            return true;
        }
        
        public async Task<bool> UpdateUser(int UserId, UserUpdateDto userUpdateDTO)
        {
            var user = await unitOfWork.Users.GetById(UserId);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);

            user.First_Name = userUpdateDTO.First_Name;
            user.Last_Name = userUpdateDTO.Last_Name;
            user.Email = userUpdateDTO.Email;

            unitOfWork.Users.Update(user);
            unitOfWork.Save();
            return true;
        }


        public async Task<string> ActivateUser(int userId)
        {
            var user = await unitOfWork.Users.GetByIdDeleted(userId);
            unitOfWork.Users.Activate(user);
            unitOfWork.Save();
            return $"User {userId} activated";
        }

        public async Task UpdatePoints(int userId, int points)
        {
            var user = await unitOfWork.Users.GetById(userId);

            if (points > user.Points) throw new AppException("You do not have enough points", HttpStatusCode.BadRequest);

            user.Points -= points;

            unitOfWork.Users.Update(user);
            unitOfWork.Save();
        }
    }
}