using AutoMapper;
using Hangfire;
using Microsoft.Extensions.Logging;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Mapping.User;
using PrimatesWallet.Core.Enums;
using PrimatesWallet.Core.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services
{
    public class FixedTermDepositService : IFixedTermDepositService
    {

        public readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FixedTermDepositService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FixedTermDeposit>> GetByUser(int userId)
        {
            /* Se puede acceder por el repo de fixedTerm pero implica dos consultas a la base de datos
             *  1 para obtener el accountId mediante el userId y otro para hacer la consulta en la tabla fixed,
             *  es mas eficiente realizar una sola consulta a la base con la tabla de account y hacer un join con fixed
             */
            var account = await unitOfWork.Accounts.GetByUserId_FixedTerm(userId);
            var fixedTermDeposit = account.FixedTermDeposit;

            return fixedTermDeposit is null
                ? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound)
                : fixedTermDeposit;
        }

        public async Task<FixedTermDeposit> GetFixedTermDepositById(int id)
        {
                var fixedTermDeposit = await unitOfWork.FixedTermDeposits.GetById(id);
                if (fixedTermDeposit == null) throw new AppException("Deposit not found", HttpStatusCode.NotFound);
                return fixedTermDeposit;
        }

        public async Task<FixedTermDepositDetailDto> GetFixedTermDepositDetails(int userId, int id)
        {
            // Para obtener el Plazo fijo requerido tomamos en cuenta las siguentes validaciones:

            // Si no existe el Id proveído , cortar la ejecucion para optimizar recursos.
                var fixedTermDeposit = await unitOfWork.FixedTermDeposits.GetFixedTermDepositById(userId, id);
                if (fixedTermDeposit == null) throw new AppException("Fixed Term Deposit not found", HttpStatusCode.NotFound);

            // Si el cliente que envia la peticion no es el propietario del plazo fijo no deberá tener acceso al mismo.
                var requestUser = await unitOfWork.Users.GetById(userId);
                if (requestUser.Account.Id != fixedTermDeposit.AccountId) throw new AppException("Invalid Credentials", HttpStatusCode.Forbidden);
               
            
                var response = new FixedTermDepositDetailDto()
                { Id = fixedTermDeposit.Id, Amount= fixedTermDeposit.Amount , Closing_Date=fixedTermDeposit.Closing_Date, Creation_Date=fixedTermDeposit.Creation_Date };
                return response;
        }


        public async Task<int> TotalPageDeposits(int pageSize)
        {
            var totalUsers = await unitOfWork.FixedTermDeposits.GetCount();
            //contamos el total de Plazos fijos y calculamos cuantas paginas hay en total
            return (int)Math.Ceiling((double)totalUsers / pageSize);
        }


        public async Task<IEnumerable<FixedTermDepositDetailDto>> GetDeposits(int page, int pageSize)
        {
        // Listado de todos los plazos fijos para el desarrollador con paginacion ya incluida
            var allDeposits = await unitOfWork.FixedTermDeposits.GetAll(page, pageSize)
                 ?? throw new AppException(ReplyMessage.MESSAGE_QUERY_EMPTY, HttpStatusCode.NotFound);

            var deposits = allDeposits.Select(x =>
                new FixedTermDepositDetailDto() { Id = x.Id ,Amount = x.Amount , Creation_Date = x.Creation_Date, Closing_Date = x.Closing_Date });
                
            return deposits;
        }

        public DateTime oneMonth = DateTime.Now.AddMonths(1);
        public DateTime treeMonths = DateTime.Now.AddMonths(3);
        public DateTime oneYear = DateTime.Now.AddYears(1);


        /// <summary>
        /// This method verifies if there is a fixed-term deposit, if it exists and it is not closed, the money is returned without interest to the client's account and finally the fixed-term deposit is eliminated.
        /// </summary>
        /// <param name="id">fixed-term deposit id</param>
        /// <returns>boolean indicating if the operation went well or not</returns>
        /// <exception cref="AppException">if the fixed term does not exist, an error is thrown</exception>
        /// <exception cref="AppException">if the closing date of the fixed term is less than the current one, an error is thrown indicating that the fixed term is closed </exception>
        public async Task<bool> DeleteFixedtermDeposit(int id)
        {
            var fixedTermDeposit = await unitOfWork.FixedTermDeposits.GetById(id);

            if (fixedTermDeposit is null) throw new AppException("Fixed term deposit not found", HttpStatusCode.NotFound);

            if (fixedTermDeposit.Closing_Date < DateTime.Now) throw new AppException("This fixed term deposit is closed", HttpStatusCode.BadRequest);

            // Return of money without interest.

            var userAccount = await unitOfWork.Accounts.GetById(fixedTermDeposit.AccountId);

            var interestRate = CalculateInterestRate(fixedTermDeposit.Creation_Date, fixedTermDeposit.Closing_Date);

            var calculateAmount = fixedTermDeposit.Amount / (1 + interestRate);

            userAccount.Money += calculateAmount;

            unitOfWork.Accounts.Update(userAccount);
            unitOfWork.FixedTermDeposits.Delete(fixedTermDeposit);

            var response = unitOfWork.Save();

            if (response > 0) return true;
            else return false;
        }

        public Task<IQueryable<FixedTermDeposit>> GetAllDepositsQueryable()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(int id, FixedTermDepositRequestDTO fixedTermDTO)
        {
            var account = await unitOfWork.Accounts.GetByUserId_FixedTerm(id); //buscamos la cuenta del user logeado

            //si no hay fondos suficientes se rechaza la operacion
            if(account.Money - fixedTermDTO.Amount < 0) throw new AppException("Insufficient funds: You do not have enough money in your account to make this investment.", HttpStatusCode.BadRequest);

            IsValidDate(fixedTermDTO); //verificamos si es correcta la relacion entre fecha de cierre y apertura

            decimal interestRate = CalculateInterestRate(fixedTermDTO.Creation_Date, fixedTermDTO.Closing_Date); //calculamos el interes a recibir

            account.Money -= fixedTermDTO.Amount; //quitamos de la cuenta el monto a invertir

            var newFixedTerm = new FixedTermDeposit()
            {
                Amount = (fixedTermDTO.Amount * (1 + interestRate)), //agregamos el monto mas los intereses
                Creation_Date = fixedTermDTO.Creation_Date,
                Closing_Date = fixedTermDTO.Closing_Date,
            };

            account.FixedTermDeposit.Add(newFixedTerm);
            var response = unitOfWork.Save();
            return response > 0;
        }


        /// <summary>
        /// Validates the creation and closing dates for a fixed-term deposit request.
        /// </summary>
        /// <param name="fixedTermDTO">The fixed-term deposit request object containing the creation and closing dates.</param>
        /// <exception cref="AppException">Thrown when the creation or closing date is invalid.</exception>
        private void IsValidDate(FixedTermDepositRequestDTO fixedTermDTO)
        {
            /*
                Vemos si con las fechas otorgadas es valido realizar una inversion,
                la inversion no puede ser en una fecha pasada, la fecha de cierre no puede ser menor que la de apertura,
                el tiempo minimo para un plazo fijo es 30 dias
            */

            bool isCreationDateValid = fixedTermDTO.Creation_Date.Date >= DateTime.Today;
            if (!isCreationDateValid) throw new AppException("The creation date must be equal to or later than today's date.", HttpStatusCode.BadRequest);

            bool isClosingDateValid = fixedTermDTO.Closing_Date > fixedTermDTO.Creation_Date;
            if (!isClosingDateValid) throw new AppException("The closing date cannot be earlier than the creation date.", HttpStatusCode.BadRequest);

            bool isMinimumTime = (fixedTermDTO.Closing_Date - fixedTermDTO.Creation_Date).TotalDays >= 30;
            if (!isMinimumTime) throw new AppException("The minimum term for the deposit is 30 days.", HttpStatusCode.BadRequest);
        }

        /// <summary>
        /// Calculates the interest rate for a fixed-term deposit based on the duration of the investment.
        /// </summary>
        /// <param name="fixedTermDTO">The fixed-term deposit request object containing the creation and closing dates.</param>
        /// <returns>The applicable interest rate based on the duration of the investment.</returns>
        private decimal CalculateInterestRate(DateTime Creation_Date, DateTime Closing_Date)
        {
            /*
             calculamos el interes segun los dias, en los requerimientos no habia detalle sobre tasas y dias, se selecciono
             menor a 90 dias 5.8 de interes, menor a un año 19 y mas de un año 85
             */

            decimal InterestLess_90Days = 0.05M;
            decimal InterestLess_365Days = 0.19M;
            decimal interestGreater_1Year = 0.85M;

            var totalDays = (Closing_Date - Creation_Date).TotalDays;

            if (totalDays <= 90) return InterestLess_90Days;
            if (totalDays <= 365) return InterestLess_365Days;

            return interestGreater_1Year;
        }

        /// <summary>
        /// This asynchronous method is responsible for liquidating fixed term deposits that have been closed.
        /// It retrieves the deposits through the "unitOfWork" object and performs a series of operations on each of them,
        /// adding the deposit amount to the associated account, registering a transaction, and updating the "account" object.
        /// Finally, it calls the "Save" method of the "unitOfWork" object to save the changes to the database.
        /// </summary>
        public async Task LiquidateFixedTermDeposit()
        {

            var fixedTermDeposits = await unitOfWork.FixedTermDeposits.GetClosedFixedTermDeposits();

            foreach (var deposit in fixedTermDeposits)
            {
                var account = deposit.Account;

                account.Money += deposit.Amount;

                var transaction = new Transaction() 
                { 
                    Concept = $"Fixed Term deposit {deposit.Id}",
                    Type = TransactionType.topup,
                    Account_Id= account.Id,
                    Amount = deposit.Amount,
                    To_Account_Id = account.Id,
                    Date = deposit.Closing_Date
                };

                await unitOfWork.Transactions.Add(transaction);

                unitOfWork.Accounts.Update(account);
            }

            unitOfWork.Save();
        }

        public async Task<string> ActivateFixedTermDeposit(int depositId)
        {
            var deposit = await unitOfWork.FixedTermDeposits.GetByIdDeleted(depositId);
            unitOfWork.FixedTermDeposits.Activate(deposit);
            unitOfWork.Save();
            return $"deposit n° {depositId} activated";
        }

        public async Task<bool> UpdateFixedTermDeposit(int id, FixedTermDepositRequestDTO fixedTermDeposit)
        {
            var dbFixedTerm = await unitOfWork.FixedTermDeposits.GetById(id);

            if (dbFixedTerm == null) throw new AppException("Fixed term not fount", HttpStatusCode.NotFound);

            var account = await unitOfWork.Accounts.GetById(dbFixedTerm.AccountId);

            if (account == null) throw new AppException("Account not found", HttpStatusCode.NotFound);


            var interestRate = CalculateInterestRate(dbFixedTerm.Creation_Date, dbFixedTerm.Closing_Date);

            var calculateAmount = dbFixedTerm.Amount / (1 + interestRate);

            account.Money += calculateAmount;

            account.Money -= fixedTermDeposit.Amount;

            var newInterestRate = CalculateInterestRate(fixedTermDeposit.Creation_Date, fixedTermDeposit.Closing_Date);

            dbFixedTerm.Amount = fixedTermDeposit.Amount * (1 + newInterestRate);
            dbFixedTerm.Creation_Date = fixedTermDeposit.Creation_Date;
            dbFixedTerm.Closing_Date = fixedTermDeposit.Closing_Date;

            unitOfWork.Accounts.Update(account);
            unitOfWork.FixedTermDeposits.Update(dbFixedTerm);
            int response = unitOfWork.Save();

            if (response > 0) return true;

            return false;
        }
    }
}
