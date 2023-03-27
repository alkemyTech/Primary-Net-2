using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services
{
    public class UserService : IUserService
    {
        public readonly IUnitOfWotk unitOfWork;

        public UserService(IUnitOfWotk unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
    }
}
