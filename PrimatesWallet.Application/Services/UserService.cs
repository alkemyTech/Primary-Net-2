using AutoMapper;
using BCrypt.Net;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
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

        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                var users = await unitOfWork.UserRepository.GetAll();
                return users;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> Signup(RegisterUserDTO user)
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

            if (response > 0)  return true;
            return false;


        }

    }
}
