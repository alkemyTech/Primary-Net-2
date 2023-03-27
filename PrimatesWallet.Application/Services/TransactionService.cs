using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;

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

        //Gestionar operaciones


    }
}
