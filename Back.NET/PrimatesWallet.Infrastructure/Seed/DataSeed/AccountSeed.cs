using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class AccountSeed : SeedData<Account>
    {
        protected override IEnumerable<Account> GetData()
        {
            return new List<Account>()
            {
                new Account()
                {
                    CreationDate= new DateTime(2022,12, 10),
                    Money= 173419180,
                    IsBlocked= false,
                    UserId= 1
                },

               new Account()
                {
                    CreationDate= new DateTime(2023,1, 16),
                    Money= 121667310,
                    IsBlocked= false,
                    UserId= 2
                },

               new Account()
                {
                    CreationDate= new DateTime(2023,1, 10),
                    Money= 25450,
                    IsBlocked= false,
                    UserId= 3
                },
               new Account()
               {
                    CreationDate= new DateTime(2023,1, 10),
                    Money= 111955890,
                    IsBlocked= false,
                    UserId= 4
               },
               new Account()
               {
                    CreationDate= new DateTime(2023,2, 24),
                    Money= 21153,
                    IsBlocked= false,
                    UserId= 5
               },
            };
        }
    }
}
