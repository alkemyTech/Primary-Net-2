using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.ServiceExtension
{
    public class PrimatesDBContext : DbContext
    {
        public PrimatesDBContext(DbContextOptions<PrimatesDBContext> options) : base(options)
        {

        }
  
    }
}
