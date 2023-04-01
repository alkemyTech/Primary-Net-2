using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping.User;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services
{
    public class FixedTermDepositService : IFixedTermDepositService
    {

        public readonly IUnitOfWork unitOfWotk;

        public FixedTermDepositService(IUnitOfWork unitOfWotk)
        {
            this.unitOfWotk = unitOfWotk;
        }

        public async Task<IEnumerable<FixedTermDeposit>> GetByUser(int userId)
        {
            /* Se puede acceder por el repo de fixedTerm pero implica dos consultas a la base de datos
             *  1 para obtener el accountId mediante el userId y otro para hacer la consulta en la tabla fixed,
             *  es mas eficiente realizar una sola consulta a la base con la tabla de account y hacer un join con fixed
             */
            var account = await unitOfWotk.Accounts.GetByUserId_FixedTerm(userId);
            var fixedTermDeposit = account.FixedTermDeposit;

            return fixedTermDeposit is null
                ? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound)
                : fixedTermDeposit;
        }

        public async Task<FixedTermDeposit> GetFixedTermDepositById(int id)
        {
                var fixedTermDeposit = await unitOfWotk.FixedTermDeposits.GetById(id);
                if (fixedTermDeposit == null) throw new AppException("Deposit not found", HttpStatusCode.NotFound);
                return fixedTermDeposit;
        }

        public async Task<FixedTermDepositDetailDTO> GetFixedTermDepositDetails(int id, int userId)
        {
                var fixedTermDeposit = await unitOfWotk.FixedTermDeposits.GetFixedTermByIdAndUserId(id, userId);
                if (fixedTermDeposit == null) throw new AppException("User/Deposit not found", HttpStatusCode.NotFound);
                var response = new FixedTermDepositDetailDTO()
                { Amount= fixedTermDeposit.Amount , Closing_Date=fixedTermDeposit.Closing_Date, Creation_Date=fixedTermDeposit.Creation_Date };
                return response;
        }

     
        public async Task<int> TotalPageDeposits(int pageSize)
        {
            var totalUsers = await unitOfWotk.FixedTermDeposits.GetCount();
            //contamos el total de Plazos fijos y calculamos cuantas paginas hay en total
            return (int)Math.Ceiling((double)totalUsers / pageSize);
        }


        public async Task<IEnumerable<FixedTermDepositDetailDTO>> GetDeposits(int page, int pageSize)
        {
            var allDeposits = await unitOfWotk.FixedTermDeposits.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var deposits = allDeposits.Select(x =>
                new FixedTermDepositDetailDTO() { Amount = x.Amount , Creation_Date = x.Creation_Date, Closing_Date = x.Closing_Date });
                
            return deposits;
        }

    }
}
