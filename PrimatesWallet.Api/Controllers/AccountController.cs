using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using System.Net;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly IUserContextService _userContextService;

        public AccountController(IAccountService accountService, IUserContextService userContextService )
        {
            _account = accountService;
            _userContextService = userContextService;
        }

        [HttpGet]
        [Authorize]
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

        /// <summary>
        ///     Endpoint to create an account for an registered user.
        /// </summary>
        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create()
        {
            var userId = _userContextService.GetCurrentUser();

            var response = await _account.Create(userId);

            var result = new BaseResponse<bool>("Account created successfully", response, (int)HttpStatusCode.OK);

            return Ok(result);
        }

        [HttpPost("{id}")]
        [Authorize]
        public async Task<IActionResult> Depositar([FromRoute] int id, [FromBody] TopUpDTO topUpDTO)
        {
            try
            {
                var idUser = _userContextService.GetCurrentUser();
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
        [Authorize]
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
