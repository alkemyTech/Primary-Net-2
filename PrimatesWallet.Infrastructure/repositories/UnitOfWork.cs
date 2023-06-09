﻿using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWotk
    {
        private readonly ApplicationDbContext _dbContext;

        //EJ: public IUserRepository Users { get; }

        public UnitOfWork(ApplicationDbContext dbContext/* EJ: IUserRepository userRepository*/ )
        {
            _dbContext = dbContext;
            //EJ: Users = userRepository; 

        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }         
    }
}
