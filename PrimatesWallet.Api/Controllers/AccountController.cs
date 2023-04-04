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
            var account = await _account.GetAccountById(id);
            if (account is null) throw new AppException($"The account with id: {id} does not exist", HttpStatusCode.NotFound);
            return Ok(account);
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

        [HttpPost("deposit/{id}")]
        [Authorize]
        public async Task<IActionResult> Deposit([FromRoute] int id, [FromBody] TopUpDto topUpDTO)
        {
            var idUser = _userContextService.GetCurrentUser();
            if (idUser != id) throw new AppException("User not authorized", HttpStatusCode.Unauthorized);

            var response = await _account.DepositToAccount(id, topUpDTO);
            var result = new BaseResponse<bool>("Operation succeded!", response ,(int)HttpStatusCode.OK);
               
            return Ok(result);
        }

        /// <summary>
        /// this endpoint is to make a transfer from one account to another
        /// </summary>
        /// <param name="accountId">sender account id</param>
        /// <param name="transferDTO">a DTO with the transaction information(receiver user email, amount, type of transaction, concept)</param>
        [HttpPost("transfer/{accountId}")]
        [Authorize]
        public async Task<IActionResult> Transfer(int accountId, [FromBody] TransferDto transferDTO)
        {
            var userId = _userContextService.GetCurrentUser();

            if (transferDTO.Amount <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Amount must be positive");

            var isValidAccount = await _account.ValidateAccount(userId, accountId);

            if (!isValidAccount) return StatusCode(StatusCodes.Status401Unauthorized, "Invalid credentials");

            var transaction = await _account.Transfer(userId, transferDTO);

            var response = new BaseResponse<TransferDetailDto>("Operation succeded!", transaction, (int)HttpStatusCode.OK);

            return Ok(response);
        }



        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] AccountUpdateDto accountUpdateDTO)
        {
            var updatedAccount = await _account.UpdateAccountAdmin(accountId, accountUpdateDTO);
            return Ok(updatedAccount);

        }



        [HttpPut("{accountId}")]
        public async Task<IActionResult> ActivateAccount(int accountId)
        {
            var account = await _account.ActivateAccount(accountId);
            return Ok(account);

        }

    }

}

