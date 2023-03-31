﻿using PrimatesWallet.Application.DTOS;
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
        Task<IEnumerable<User>> GetUsers();
        Task<int> Signup(RegisterUserDTO user);
    }
}
