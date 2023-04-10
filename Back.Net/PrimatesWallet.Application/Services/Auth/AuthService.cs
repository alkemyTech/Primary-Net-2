using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<User> Authenticate(LoginUserDto login)
        {

            var currentUSer = await unitOfWork.Users.GetByEmail(login.UserName);

            if (currentUSer is null) throw new AppException("No se encontro el usuario", HttpStatusCode.NotFound);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(login.Password, currentUSer.Password);

            if (!isValidPassword) throw new AppException("Credenciales invalidas", HttpStatusCode.Unauthorized);
            
            return currentUSer;
        }
    }
}
