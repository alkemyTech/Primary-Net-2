using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;
using PrimatesWallet.Core.Models;
using System.Net;

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
        public async Task<IActionResult> GetTransactions()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById([FromBody] int id)
        {
            try
            {
                var transaction = await GetTransactionById(id);

                if (transaction == null) { return NotFound(); }

                return Ok(transaction);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
