using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping.Transaction;
using System.Net;


namespace PrimatesWallet.Application.Services
{
    //servicio de transaccion con su logica de negocio
    //luego lo inyectaremos en el ctor del Controller de Transaction 

    public class TransactionService : ITransactionService
    {
        public IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TransactionDTO>> GetAllByUser(int userId)
        {
        
            //con el id del usuario buscamos el id de su cuenta
            var accountId = await _unitOfWork.Accounts.GetIdAccount(userId)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            //devolvemos las transacciones de ese usuario ya sea: deposito, transferencia realizada o recibida
            var transactions = await _unitOfWork.Transactions.GetAllByAccount(accountId)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var transactionsDTO = transactions.Select(x => 
                new TransactionDTOBuilder()
                .WithId(x.Id)
                .WithAmount(x.Amount)
                .WithConcept(x.Concept)
                .WithDate(x.Date)
                .WithType(x.Type)
                .WithAccountId(x.Account_Id)
                .WithToAccountId(x.Account_Id)
                .Build()).ToList();
                
            return transactionsDTO;
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            try
            {
                return await _unitOfWork.Transactions.GetById(transactionId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> DeleteTransaction(int transactionId, int userId)
        {

            var user = await _unitOfWork.UserRepository.GetById(userId);
            var isAdmin = await _unitOfWork.UserRepository.IsAdmin(user);
            if (!isAdmin) 
            {
                throw new AppException("Invalid Credentials", HttpStatusCode.Forbidden);
            }
            var transaction = await _unitOfWork.Transactions.GetById(transactionId);
            
            if(transaction == null)
            {
                throw new AppException($"Transaction {transactionId} not found", HttpStatusCode.NotFound);
            }
            _unitOfWork.Transactions.Delete(transaction);
            _unitOfWork.Save();
            return true;

        }
    }
}
