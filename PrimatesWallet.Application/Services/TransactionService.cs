using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
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

        public async Task<IEnumerable<Transaction>> GetAllByUser(int userId)
        {
        
            //con el id del usuario buscamos el id de su cuenta
            var accountId = await _unitOfWork.Accounts.GetIdAccount(userId)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            //devolvemos las transacciones de ese usuario ya sea: deposito, transferencia realizada o recibida
            var transactions = await _unitOfWork.Transactions.GetAllByAccount(accountId)
                ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            return transactions;
        }
    }
}
