using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Interfaces
{
    /// <summary>
    /// Define servicios para manejar transacciones.
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Obtiene todas las transacciones de un usuario a partir de su ID.
        /// </summary>
        /// <param name="userId">ID del usuario del que se desean obtener las transacciones.</param>
        /// <returns>Una colección de objetos Transaction que representan las transacciones realizadas por el usuario.</returns>
        /// <exception cref="AppException">Se lanza cuando no se encuentran transacciones para el usuario.</exception>
        Task<IEnumerable<TransactionDto>> GetAllByUser(int userId);
        
        Task<TransactionDto> GetTransactionById(int id);
        
        Task<bool> DeleteTransaction(int transactionId , int userId);
        Task<bool> UpdateTransaction(int transactionId, string concept = "repayment");
        Task<IEnumerable<TransactionDto>> GetAllTransactions();
    }
}
