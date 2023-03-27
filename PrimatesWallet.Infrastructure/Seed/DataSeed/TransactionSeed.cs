using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Infrastructure.Seed.DataSeed
{
    public class TransactionSeed : SeedData<Transaction>
    {
        protected override IEnumerable<Transaction> GetData()
        {
            return new List<Transaction>()
            {
                new Transaction()
                {
                    Amount =  100.00M,
                    Concept = "Deposito a cuenta",
                    Date = DateTime.Now,
                    Type = TransactionType.topup,
                    Account_Id = 1,
                    To_Account_Id = 1,
                },
               new Transaction()
                {
                    Amount =  50.00M,
                    Concept = "Deposito a cuenta",
                    Date = DateTime.Now,
                    Type = TransactionType.topup,
                    Account_Id = 2,
                    To_Account_Id = 2,
                },

               new Transaction()
                {
                    Amount =  20.00M,
                    Concept = "Transferencia a cuenta 1",
                    Date = DateTime.Now,
                    Type = TransactionType.payment,
                    Account_Id = 2,
                    To_Account_Id = 1,
                },
            };
        }
    }
}
