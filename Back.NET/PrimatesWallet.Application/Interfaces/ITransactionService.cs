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
        Task<IEnumerable<TransactionDto>> GetAllByUser(int userId,int page, int pageSize);
        
        Task<TransactionDto> GetTransactionById(int id);
        
        Task<bool> DeleteTransaction(int transactionId);
        Task<bool> UpdateTransaction(int transactionId, string concept = "repayment");
        Task<IEnumerable<TransactionDto>> GetAllTransactions(int page, int pageSize);


        /// <summary>
        /// Inserts a transaction based on the given transaction DTO.
        /// </summary>
        /// <param name="transactionDTO">The transaction request DTO to use for creating the transaction.</param>
        /// <returns>A boolean indicating whether the transaction was successfully inserted or not.</returns>
        /// <exception cref="AppException">Thrown when the transaction violates business rules.</exception>
        Task<bool> Insert(TransactionRequestDto transactionDTO);
        Task<string> ActivateTransaction(int transactionId);
        Task<int> TotalPageTransactions(int PageSize);
        Task<int> TotalPageTransactionsByUser(int id,int PageSize);
    }
}
