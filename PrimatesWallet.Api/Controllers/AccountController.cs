using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services.Auth;
using PrimatesWallet.Application.Services;
using System.Net;
using PrimatesWallet.Application.DTOS;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly IUserContextService _userContextService;
        private readonly ITransactionService _transaction;

        public AccountController(IAccountService accountService, IUserContextService userContextService, ITransactionService transactionService )
        {
            _account = accountService;
            _userContextService = userContextService;
            _transaction = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _account.GetAccountsList();
            if (accounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No accounts in database.");
            }

            return StatusCode(StatusCodes.Status200OK, accounts);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAccountDetails(int id)
        {

            try
            {
                var account = await _account.GetAccountById(id);

                if (account is null) return NotFound($"The account with id: {id} does not exist");

                return Ok(account);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }




        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Depositar([FromRoute] int id, [FromBody] TopUpDTO topUpDTO)
        {
            try
            {
                var idUser = _userContext.GetCurrentUser();
                if (idUser != id)
                {
                    throw new AppException("User not authorized", HttpStatusCode.Unauthorized);

                }

                var Response = await _account.DepositToAccount(id, topUpDTO);
                var result = new BaseResponse<bool>("Operacion Exitosa!",Response,(int)HttpStatusCode.OK);
                return Ok(result);

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

        [HttpPost("{accountId}")]
        public async Task<IActionResult> Transfer(int accountId, [FromBody] TransferDTO transferDTO)
        {
            var userId = _userContextService.GetCurrentUser();
            try
            {
                if (transferDTO.Amount <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Amount must be positive");
                
                var isValidAccount = await _account.ValidateAccount(userId, accountId);

                if (!isValidAccount) return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");
              
                var transaction = await _account.Transfer(transferDTO.Amount, userId, transferDTO.Email, transferDTO.Concept);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);

            }
        }



    }
}
