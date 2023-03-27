using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed
{   
    //para poder usar polimorfismo en la clase abstracta
    public interface ISeedData
    {
        void Seed(DbContext context);
    }
}
