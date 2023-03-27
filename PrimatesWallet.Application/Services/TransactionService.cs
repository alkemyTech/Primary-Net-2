using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;

namespace PrimatesWallet.Application.Services
{
    //Realizamos el servicio de transaccion con su logica de negocio
    //que luego inyectaremos en el ctor del Controller de Transaction 

    public class TransactionService : ITransactionService
    {
        public IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                await _unitOfWork.Transactions.Add(transaction);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteTransaction(int transactionId)
        {
            if (transactionId > 0)
            {
                var transaction = await _unitOfWork.Transactions.GetById(transactionId);
                if (transaction != null)
                {
                    _unitOfWork.Transactions.Delete(transaction);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var TransactionsList = await _unitOfWork.Transactions.GetAll();
            return TransactionsList;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            if (transactionId > 0)
            {
                var transactionById = await _unitOfWork.Transactions.GetById(transactionId);
                if (transactionById != null)
                {
                    return transactionById;
                }
            }
            return null;
        }

        // Es conveniente actualizar una transaccion?...
        public async Task<bool> UpdateTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                var transactionToUpdate = await _unitOfWork.Transactions.GetById(transaction.Id);
                if (transactionToUpdate != null)
                {
                    transactionToUpdate.Concept = transaction.Concept;
                    transactionToUpdate.Amount = transaction.Amount;
                    transactionToUpdate.Date = transaction.Date;
                    transactionToUpdate.ToAccount = transaction.ToAccount;
                    transactionToUpdate.Account_Id = transaction.Account_Id;
                    transactionToUpdate.Type = transaction.Type;

                    _unitOfWork.Transactions.Update(transactionToUpdate);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
