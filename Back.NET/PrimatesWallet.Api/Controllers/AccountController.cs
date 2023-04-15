using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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


        /// <summary>
        /// Returns a list of all accounts.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the client to retrieve a paginated list accounts. 
        /// The response includes an object with the url of the previous page, the results and the url of the next page.
        /// </remarks>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The maximum number of products to return per page.</param>
        /// <response code="200">Returns the paginated list of products.</response>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="404">"The requested resource was not found.</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get a paginated list of accounts", Description = "Retrieves a paginated list of accounts.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
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



        /// <summary>
        /// Get an account by id and show details
        /// </summary>     
        /// <param name="id">Get account searching by id</param>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>        
        /// <response code="404">The requested resource was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get a specific Account", Description = "Get a specific account by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAccountDetails(int id)
        {
            var account = await _account.GetAccountById(id);
            if (account is null) throw new AppException($"The account with id: {id} does not exist", HttpStatusCode.NotFound);
            return Ok(account);
        }


        /// <summary>
        /// Create a new account for the logged-in user.
        /// </summary>      
        /// <response code="200">Successful operation.</response>  
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="404">The requested resource was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [Authorize]
        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Create a new account for the logged-in user.", Description = "Creates a new Account in the Wallet App.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Create()
        {
            var userId = _userContextService.GetCurrentUser();

            var response = await _account.Create(userId);

            var result = new BaseResponse<bool>("Account created successfully", response, (int)HttpStatusCode.OK);

            return Ok(result);
        }


        /// <summary>
        /// Deposits funds into the user's own account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     
        ///     {
        ///         "money": 20000,
        ///         "concept: "Deposits funds into the account of the logged-in user.",
        ///     }
        /// 
        /// </remarks>  
        /// <param name="topUpDTO">The details of the top-up.</param>
        /// <returns>The HTTP response indicating the status of the operation.</returns>
        /// <response code="200">The deposit operation was successful.</response>
        /// <response code="401">The user is not authorized to perform the deposit operation.</response>
        /// <response code="404">The requested account or resource was not found.</response>
        /// <response code="500">An error occurred while processing the request.</response>
        [HttpPost("Deposit")]
        [Authorize]
        [SwaggerOperation(Summary = "Deposits funds into the user's own account..", Description = "Performs a top-up transaction by depositing funds to the account of the logged-in user..")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Deposit([FromBody] TopUpDto topUpDTO)
        {
            var idUser = _userContextService.GetCurrentUser();

            var response = await _account.DepositToAccount(idUser, topUpDTO);
            var result = new BaseResponse<bool>("Operation succeded!", response ,(int)HttpStatusCode.OK);
               
            return Ok(result);
        }


        /// <summary>
        /// Transer funds from the logged-in user's account to another.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     
        ///     {
        ///         "concept": "Transfer funds to another account",
        ///         "amount": 10000,
        ///         "email": "receiver@example.com",
        ///     }
        /// 
        /// </remarks>  
        /// <param name="transferDTO">The transaction information. 'Type' property should be set to 1 to indicate a payment</param>
        /// <returns>The HTTP response indicating the status of the operation.</returns>
        /// <response code="200">The deposit operation was successful.</response>
        /// <response code="401">The user is not authorized to perform the deposit operation.</response>
        /// <response code="404">The requested account or resource was not found.</response>
        /// <response code="500">An error occurred while processing the request.</response>
        [HttpPost("Transfer")]
        [Authorize]
        [SwaggerOperation(Summary = "Transer funds.", Description = "Transer funds from the logged-in user's account to another.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> Transfer([FromBody] TransferDto transferDTO)
        {
            var userId = _userContextService.GetCurrentUser();

            if (transferDTO.Amount <= 0) return StatusCode(StatusCodes.Status400BadRequest, "Amount must be positive");

            var transaction = await _account.Transfer(userId, transferDTO);

            var response = new BaseResponse<TransferDetailDto>("Operation succeded!", transaction, (int)HttpStatusCode.OK);

            return Ok(response);
        }


        /// <summary>
        /// Update an existing Account.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     
        ///     {
        ///         "money": 300000,
        ///         "isBlocked": true
        ///     }
        /// 
        /// </remarks>  
        /// <param name="accountId">Account ID obtained from the request URL</param>
        /// <param name="accountUpdateDTO">Account model obtained from the request body</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>   
        /// <response code="404">"The requested resource was not found.</response>              
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPut("{accountId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update an existing Account.", Description = "Update an existing Account.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] AccountUpdateDto accountUpdateDTO)
        {
            var updatedAccount = await _account.UpdateAccountAdmin(accountId, accountUpdateDTO);
            return Ok(updatedAccount);

        }


        /// <summary>
        /// Activates or desactivates an account by its ID.
        /// </summary>
        /// <param name="id">The ID of the account to activate or desactivate.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">"The requested resource was not found..</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPut("Activate/{accountId}")]
        [Authorize]
        [SwaggerOperation(Summary = "Activate or Desactivate an Account.", Description = "Only admins have permission to perform this operation.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> ActivateAccount(int accountId)
        {
            var account = await _account.ActivateAccount(accountId);
            return Ok(account);

        }


        /// <summary>
        /// Deletes an account.
        /// </summary>
        /// <param name="id">ID of the account to delete.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">"The requested resource was not found..</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpDelete("{accountId}")]
        [Authorize]
        [SwaggerOperation(Summary = "Delete an Account.", Description = "Deletes an Account by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var currentUser = _userContextService.GetCurrentUser();
            var deleteAccount = await _account.DeleteAccount(accountId, currentUser);
            return Ok(deleteAccount);
        }

        //[HttpPatch("{accountId}")]
        //[Authorize]
        //[SwaggerOperation(Summary = "Block an Account.", Description = "Blocks an Account by its ID.")]
        //[SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        //[SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        //[SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        //public async Task<IActionResult> BlockAccount(int accountId, JsonPatchDocument<AccountResponseDTO> accountDto)
        //{
        //    var currentUser = _userContextService.GetCurrentUser();
        //    var deleteAccount = await _account.BlockAccount(accountId, currentUser);
        //    return Ok(deleteAccount);
        //}

    }

}

