using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Core.Interfaces
{
    public interface IUnitOfWotk : IDisposable
    {
        IUserRepository UserRepository { get; }
        int Save();
    }
}
