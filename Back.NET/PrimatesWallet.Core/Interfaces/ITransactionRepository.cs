using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Core.Interfaces
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        /// <summary>
        /// Retorna todas las transacciones de una cuenta específica, depósitos, transferencias realizadas o recibidas.
        /// </summary>
        /// <param name="id">El ID de la cuenta de la cual se quieren obtener las transacciones.</param>
        /// <returns>Una colección de objetos <c>Transaction</c> que representan las transacciones encontradas.</returns>
        /// <exception cref="AppException">Si no se encuentran transacciones asociadas a la cuenta especificada.</exception>
        Task<IEnumerable<Transaction>> GetAllByAccount(int id,int page, int pageSize);


        /// <summary>
        /// Inserts a new Transaction into the database using a stored procedure.
        /// </summary>
        /// <param name="transaction">The Transaction to insert.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task InsertWithStoredProcedure(Transaction transaction);
        Task<IEnumerable<Transaction>> GetAll(int page, int pageSize);
        Task<int> GetCount();
        Task<int> GetCountByUser(int id);
    }
}
