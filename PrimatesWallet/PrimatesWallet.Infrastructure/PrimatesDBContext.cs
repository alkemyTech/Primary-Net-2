﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure
{
    public class PrimatesDBContext : DbContext
    {
        public PrimatesDBContext(DbContextOptions<PrimatesDBContext> options) : base(options)
        {
        }
    }
}
