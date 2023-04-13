using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Mapping.Transaction;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;

namespace PrimatesWallet.Application.Services
{
    //servicio de transaccion con su logica de negocio
    //luego lo inyectaremos en el ctor del Controller de Transaction 

    public class TransactionService : ITransactionService
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all transactions made by a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of TransactionDto objects representing the user's transactions.</returns>
        /// <exception cref="AppException">Thrown if no transactions are found for the user.</exception>
        public async Task<IEnumerable<TransactionDto>> GetAllByUser(int userId,int page, int pageSize)
        {
            // Retrieves the ID of the user's account using the user ID.
            var accountId = await _unitOfWork.Accounts.GetIdAccount(userId)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            // Retrieves all transactions for the user's account.
            var transactions = await _unitOfWork.Transactions.GetAllByAccount(accountId,page,pageSize)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            // Converts the transactions into a list of TransactionDto objects.
            var transactionsDTO = transactions.Select(x =>
                    new TransactionDtoBuilder()
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
        /// Retrieves a transaction by its ID
        /// </summary>
        /// <param name="id">The ID of the transaction to retrieve</param>
        /// <returns>The transaction DTO</returns>
        /// <exception cref="AppException">Thrown when no transaction with the given ID is found</exception>
        public async Task<TransactionDto> GetTransactionById(int id)
        {

            var transaction = await _unitOfWork.Transactions.GetById(id);

            if (transaction is null) throw new AppException($"No transaction found with id {id}", HttpStatusCode.NotFound);

            // Build a new TransactionDto object using a TransactionDtoBuilder and the data from the Transaction object.
            var transactionDTO = _mapper.Map<TransactionDto>(transaction);

            return transactionDTO;

        }

        /// <summary>
        /// Retrieves all transactions.
        /// </summary>
        /// <returns>A collection of TransactionDto objects representing all transactions.</returns>
        /// <exception cref="AppException">Thrown when no transactions are found in the database.</exception>
        public async Task<IEnumerable<TransactionDto>> GetAllTransactions(int page, int pageSize)
        {
            var transactions = await _unitOfWork.Transactions.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var transactionsDTO = transactions.Select(x =>
                                    new TransactionDtoBuilder()
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
        public async Task<int> TotalPageTransactions(int PageSize)
        {
            var totalTransactions = await _unitOfWork.Transactions.GetCount();
            return (int)Math.Ceiling((double)totalTransactions / PageSize);
        }

        public async Task<int> TotalPageTransactionsByUser(int id,int PageSize)
        {
            var totalTransactions = await _unitOfWork.Transactions.GetCountByUser(id);
            return (int)Math.Ceiling((double)totalTransactions / PageSize);
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

        /// <summary>
        /// Deletes a transaction by its ID
        /// </summary>
        /// <param name="transactionId">The ID of the transaction to be deleted</param>
        /// <returns>A boolean indicating if the deletion was successful or not</returns>
        /// <exception cref="AppException">Thrown when the transaction with the given ID is not found</exception>
        public async Task<bool> DeleteTransaction(int transactionId)
        {                        
            var transaction = await _unitOfWork.Transactions.GetById(transactionId);
            
            if(transaction == null) throw new AppException($"Transaction with id:{transactionId} not found", HttpStatusCode.NotFound);
            
            _unitOfWork.Transactions.Delete(transaction);
            var result = _unitOfWork.Save();
           
            if( result > 0 )return true;

            return false;
        }

        /// <summary>
        /// Inserts a new transaction into the database and updates the corresponding accounts' balances.
        /// </summary>
        /// <param name="transactionDTO">The transaction data transfer object.</param>
        /// <returns>Returns a boolean indicating whether the operation was successful.</returns>
        /// <exception cref="AppException">Throws an exception when trying to perform invalid transactions (e.g. payments between the same account, deposits to other accounts, insufficient balance, etc).</exception>
        public async Task<bool> Insert(TransactionRequestDto transactionDTO)
        {
            /*
             Verificamos si la logica de emisor receptor de una transaccion es correcta
             */
            bool isTopup = transactionDTO.Type is TransactionType.topup;
            bool isPaymentOrRepayment = transactionDTO.Type is (TransactionType.payment or TransactionType.repayment);
            bool isSameAccount = transactionDTO.Account_Id == transactionDTO.To_Account_Id;

            if (!isSameAccount && isTopup) throw new AppException("Deposits to other accounts are not allowed", HttpStatusCode.BadRequest);

            if (isSameAccount && isPaymentOrRepayment) throw new AppException("Payments between the same accounts are not allowed", HttpStatusCode.BadRequest);

            var transaction = new Transaction()
            {
                Amount = transactionDTO.Amount,
                Type = transactionDTO.Type,
                Concept = transactionDTO.Concept,
                Date = DateTime.Now,
                Account_Id = transactionDTO.Account_Id,
                To_Account_Id = transactionDTO.To_Account_Id
            };

            /*
                Se creo un stored procedure donde se comprueba si las cuentas existen,
                dependiendo el tipo de transferencia se ajustan los balances en las cuentas y
                se registra la transaccion, si alguna validacion es incorrecta el middleware atrapa la DbException
                con su respectivo mensaje, las operaciones se realizan un una transaccion y
                en caso de error se hace un rollback
             */
            await _unitOfWork.Transactions.InsertWithStoredProcedure(transaction);
            _unitOfWork.Save();
            return true; //el stored tiene validaciones si falla y las excepciones son atrapadas por el middleware
        }


        public async Task<string> ActivateTransaction(int transactionId)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdDeleted(transactionId);
            _unitOfWork.Transactions.Activate(transaction);
            _unitOfWork.Save();
            return $"Transaction {transactionId} activated";
        }

    }
}
