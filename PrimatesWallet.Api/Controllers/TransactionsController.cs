using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        //Se deja preparado el controlador de Transacciones con la DI de servicios
        //Se deja pendiente el desarrollo de los endpoints asignados.
        private readonly ITransactionService transactionService;
        private readonly IUserContextService UserContextService;


        public TransactionsController(ITransactionService transaction, IUserContextService userContextService)

        {
            transactionService = transaction;
            UserContextService = userContextService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactionsByUserId()
        {
            try
            {
                var userId = UserContextService.GetCurrentUser(); //buscamos el id del usuario que se logeo
                var transactions = await transactionService.GetAllByUser(userId); //buscamos las transacciones solo de ese user

                var response = new BaseResponse<IEnumerable<TransactionDTO>>(ReplyMessage.MESSAGE_QUERY, transactions, (int)HttpStatusCode.OK);
                return Ok(response);
            }
            catch (AppException ex)
            {
                var response = new BaseResponse<object>(ex.Message, null, (int)ex.StatusCode);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse<object>(ex.Message, null, (int)HttpStatusCode.InternalServerError);
                return StatusCode(response.StatusCode, response);
            }
        }

        /// <summary>
        /// this endpoint returns all transactions in the database, it can only be accessed by the administrator
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await transactionService.GetAllTransactions();

            var response = new BaseResponse<IEnumerable<TransactionDTO>>("Ok", transactions, (int)HttpStatusCode.OK);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            TransactionDTO transaction = await transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No transaction found by id{id}");
            }
            return StatusCode(StatusCodes.Status200OK, transaction);
        }

        [HttpDelete("{transactionId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            var requestedUser = UserContextService.GetCurrentUser();
            var response = await transactionService.DeleteTransaction(transactionId, requestedUser);
            if (!response) { return NotFound(); }
            return Ok($"Transaction {transactionId} deleted.");
        }

        /// <summary>
        /// this endpoint is to reverse a payment, it is only accessible by the administrator
        /// </summary>
        /// <param name="transactionId">id of the transaction</param>
        /// <param name="concept">concept of the transaction, extrated from the body</param>
        /// <returns>if all goes well, it returns a 200 status code; otherwise, the middleware detects the error and returns the same</returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{transactionId}")]
        public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] string concept = "repayment")
        {
            var repayment = await transactionService.UpdateTransaction(transactionId, concept);

            var response = new BaseResponse<bool>("successful repayment", repayment, (int)HttpStatusCode.OK);

            return Ok(response);
        }

        /// <summary>
        /// Creates a new transaction from the information provided in the <paramref name="transactionDTO"/> parameter.
        /// </summary>
        /// <param name="transactionDTO">The DTO object containing the transaction information.</param>
        /// <returns>The result of the transaction creation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDTO transactionDTO)
        {
            var transaction = await transactionService.Insert(transactionDTO);
            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_CREATE_SUCCESS, transaction, (int)HttpStatusCode.Created);
            return Ok(response);
        }
    }
}
