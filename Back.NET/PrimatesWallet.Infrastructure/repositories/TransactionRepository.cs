using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.repositories;
using System.Data;

namespace PrimatesWallet.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Transaction>> GetAll()
        {
            return await _dbContext.Transactions.Where(x => !x.IsDeleted).ToListAsync();
        }

        public override async Task<Transaction> GetById(int id)
        {
            return await _dbContext.Transactions.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public override async Task<Transaction> GetByIdDeleted(int id)
        {
            return await _dbContext.Transactions.Where(x => x.Id == id && x.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllByAccount(int id, int page, int pageSize)
        {
            return await base._dbContext.Transactions
                 .Where(t => (t.Type == TransactionType.topup && t.Account_Id == id) //depositos
                     || (t.Type == TransactionType.payment && t.Account_Id == id) //transferencia realizadas
                     || (t.Type == TransactionType.payment && t.To_Account_Id == id)//transferencias recibidas
                     || (t.Type == TransactionType.repayment && t.To_Account_Id == id) //reembolso recibido
                     || (t.Type == TransactionType.repayment && t.Account_Id == id)) // reembolsos realizados (solo admins)
                 .Where(x => x.IsDeleted == false)
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
        }

        public async Task InsertWithStoredProcedure(Transaction transaction)
        {
            //asignamos los parametros
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Amount", transaction.Amount),
                new SqlParameter("@Concept", transaction.Concept),
                new SqlParameter("@Date", transaction.Date),
                new SqlParameter("@Type", ConvertTransactionTypeToString(transaction.Type)),
                new SqlParameter("@Account_Id", transaction.Account_Id),
                new SqlParameter("@To_Account_Id", transaction.To_Account_Id),
            };

            // Ejecuta el procedimiento almacenado
            await base._dbContext.Database.ExecuteSqlRawAsync("EXEC InsertTransactionWithValidation"
                + " @Amount, @Concept, @Date, @Type, @Account_Id, @To_Account_Id",
                parameters.ToArray());
        }

        /// <summary>
        /// Converts a TransactionType enumeration value to its corresponding string representation.
        /// </summary>
        /// <param name="type">The TransactionType value to convert.</param>
        /// <returns>A string representation of the TransactionType value.</returns>
        private string ConvertTransactionTypeToString(TransactionType type)
        {
            /*
            en model builder definimos para que se guarde como string el enum, cuando trate de usar el stored lo guardaba como numero
            asi que cree esta clase para que se guarde tambien como string
            */
            switch (type)
            {
                case TransactionType.topup:
                    return "topup";
                case TransactionType.payment:
                    return "payment";
                case TransactionType.repayment:
                    return "repayment";
                default:
                    throw new ArgumentException("Invalid Transaction type.", nameof(type));
            }
        }
        public async Task<IEnumerable<Transaction>> GetAll(int page, int pageSize)
        {
            return await base._dbContext.Transactions
                .Where(a => a.IsDeleted == false)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetCount()
        {
            return await base._dbContext.Transactions.Where(a => a.IsDeleted == false).CountAsync();
        }

        public async Task<int> GetCountByUser(int id)
        {
            return await base._dbContext.Transactions
                .Where(t => (t.Type == TransactionType.topup && t.Account_Id == id) 
                     || (t.Type == TransactionType.payment && t.Account_Id == id) 
                     || (t.Type == TransactionType.payment && t.To_Account_Id == id)
                     || (t.Type == TransactionType.repayment && t.To_Account_Id == id) 
                     || (t.Type == TransactionType.repayment && t.Account_Id == id))
                 .Where(x => x.IsDeleted == false)
                 .CountAsync();
        }
    }
}
