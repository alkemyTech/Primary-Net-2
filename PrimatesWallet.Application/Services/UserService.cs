using AutoMapper;
using BCrypt.Net;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping.User;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PrimatesWallet.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                var user = await unitOfWork.UserRepository.GetById(id);
                return user is null ?
                    throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound)
                    : user; //OBS: Falta mapping para DTO (configuracion en equipo)

                //si no existe el usuario lanzamos un exception personalizada
                //en otra parte del codigo la atrapamos y le damos un formato
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsers(int page, int pageSize)
        {
            var users = await unitOfWork.UserRepository.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var usersDTO = users.Select(x => 
                new UserResponseDTOBuilder()
                .WithUserId(x.UserId)
                .WithFirstName(x.First_Name)
                .WithLastName(x.Last_Name)
                .WithEmail(x.Email)
                .WithPoints(x.Points)
                .WithRolId(x.Rol_Id)
                .Build()).ToList();

            return usersDTO;
        }

        public async Task<int> TotalPageUsers(int pageSize)
        {
            var totalUsers = await unitOfWork.UserRepository.GetCount();
            //contamos el total de usuarios y calculamos cuantas paginas hay en total
            return (int)Math.Ceiling((double)totalUsers / pageSize);
        }

        public async Task<int> Signup(RegisterUserDTO user)

        {
            var isRegistered = await unitOfWork.UserRepository.IsRegistered(user.Email);

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
           
            await unitOfWork.UserRepository.Add(newUser);
            var response = unitOfWork.Save();

            var userId = await unitOfWork.UserRepository.GetUserIdByEmail(newUser.Email);
            if (userId == 0) throw new AppException($"No user with id {userId}", HttpStatusCode.BadRequest);
            

            if (response > 0)  return userId;
            return 0;

        }
        public async Task<bool> UpdateUser(int UserId, UserUpdateDTO userUpdateDTO)
        {
            var user = await unitOfWork.UserRepository.GetById(UserId);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);

            user.First_Name = userUpdateDTO.First_Name;
            user.Last_Name = userUpdateDTO.Last_Name;
            user.Email = userUpdateDTO.Email;
            user.Password = userUpdateDTO.Password;

            unitOfWork.UserRepository.Update(user);
            unitOfWork.Save();
            return true;
        }

    }
}
