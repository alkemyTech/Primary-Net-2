using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using PrimatesWallet.Infrastructure.Repositories;
using System;
using System.Net;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Principal;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;
        private readonly IUserContextService _userContextService;

        public AccountController(IAccountService accountService, IUserContextService userContextService)
        {
            _account = accountService;
            _userContextService = userContextService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10)
        {
            var accounts = await _account.GetAccounts(page, pageSize);
            var totalPages = await _account.TotalPageAccounts(pageSize);
            string url = CurrentURL.Get(HttpContext.Request);
            var response = new BasePaginateResponse<IEnumerable<AccountResponseDTO>>()
            {
                Message = ReplyMessage.MESSAGE_QUERY,
                Result = accounts,
                Page = page,
                NextPage = (page < totalPages) ? $"{url}?page={page + 1}" : "None",
                PreviousPage = (page == 1) ? "none" : $"{url}?page={page - 1}",
                StatusCode = (int)HttpStatusCode.OK
            };
            return Ok(response);
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
                var result = new BaseResponse<bool>("Operacion Exitosa!", Response, (int)HttpStatusCode.OK);
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

        /// <summary>
        /// this endpoint is to make a transfer from one account to another
        /// </summary>
        /// <param name="accountId">sender account id</param>
        /// <param name="transferDTO">a DTO with the transaction information(receiver user email, amount, type of transaction, concept)</param>
        [HttpPost("transfer/{accountId}")]
        [Authorize]
        public async Task<IActionResult> Transfer(int accountId, [FromBody] TransferDTO transferDTO)
        {
            var userId = _userContextService.GetCurrentUser();

            if (transferDTO.Amount <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Amount must be positive");

            var isValidAccount = await _account.ValidateAccount(userId, accountId);

            if (!isValidAccount) return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");

            var transaction = await _account.Transfer(userId, transferDTO);

            var response = new BaseResponse<TransferDetailDTO>("Tranferencia exitosa!", transaction, (int)HttpStatusCode.OK);

            return Ok(response);
        }



        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] AccountUpdateDTO accountUpdateDTO)
        {
            var updatedAccount = await _account.UpdateAccountAdmin(accountId, accountUpdateDTO);
            return Ok(updatedAccount);


        }


    }

}

