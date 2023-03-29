using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
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
            try
            {
                var fixedTermDeposit = await unitOfWotk.FixedTermDeposits.GetById(id);
                return fixedTermDeposit;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
