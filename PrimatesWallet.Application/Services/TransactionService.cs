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
                    .WithToAccountId((int)x.To_Account_Id)
                    .Build()).ToList();

            return transactionsDTO;
        }

        public async Task<TransactionDTO> GetTransactionById(int transactionId)
        {

            var transaction = await _unitOfWork.Transactions.GetById(transactionId);

            var transactionDTO = new TransactionDTOBuilder()
                    .WithId(transaction.Id)
                    .WithAmount(transaction.Amount)
                    .WithConcept(transaction.Concept)
                    .WithDate(transaction.Date)
                    .WithType(transaction.Type)
                    .WithAccountId(transaction.Account_Id)
                    .WithToAccountId((int)transaction.To_Account_Id)
                    .Build();

            return transactionDTO;

        }

        public async Task<IEnumerable<TransactionDTO>> GetAllTransactions()
        {
            var transactions = await _unitOfWork.Transactions.GetAll();

            var transactionsDTO = transactions.Select(x =>
                                    new TransactionDTOBuilder()
                                    .WithId(x.Id)
                                    .WithAmount(x.Amount)
                                    .WithConcept(x.Concept)
                                    .WithDate(x.Date)
                                    .WithType(x.Type)
                                    .WithAccountId(x.Account_Id)
                                    .WithToAccountId((int)x.To_Account_Id)
                                    .Build()).ToList();

            return transactionsDTO;
        }


        /// <summary>
        /// this method makes a repayment.
        /// It looks up the transaction in the database and then performs the necessary operations to return the money to the sender of the payment.
        /// </summary>
        /// <param name="transactionId">id of the transaction</param>
        /// <param name="concept">uptdate concept</param>
        /// <returns>a boolean indicating whether the operation was successful</returns>
        /// <exception cref="AppException">status code indicating that the transaction was not found</exception>
        public async Task<bool> UpdateTransaction(int transactionId, string concept = "repayment")
        {
            var dbTransaction = await _unitOfWork.Transactions.GetById(transactionId);
            if (dbTransaction == null) throw new AppException("Transaction not found", HttpStatusCode.NotFound);
            if (dbTransaction.Type == TransactionType.repayment) throw new AppException("This transaction has already been reversed", HttpStatusCode.BadRequest);
            
            //Account of the client who received the payment.
            var remitentAccount = await _unitOfWork.Accounts.GetById((int)dbTransaction.To_Account_Id);

            //Account of the client who made the payment.
            var receiverAccount = await _unitOfWork.Accounts.GetById(dbTransaction.Account_Id);

            //-------repayment-------
            remitentAccount.Money -= dbTransaction.Amount;

            receiverAccount.Money += dbTransaction.Amount;

            dbTransaction.Type = TransactionType.repayment;
            dbTransaction.Account_Id = remitentAccount.Id;
            dbTransaction.To_Account_Id = receiverAccount.Id;
            dbTransaction.Concept = concept;
            dbTransaction.Date = DateTime.Now;

            _unitOfWork.Transactions.Update(dbTransaction);

            var response = _unitOfWork.Save();

            if (response > 0) return true;
            return false;
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
