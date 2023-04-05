using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService transactionService;
        private readonly IUserContextService UserContextService;


        public TransactionsController(ITransactionService transaction, IUserContextService userContextService)

        {
            transactionService = transaction;
            UserContextService = userContextService;
        }

        /// <summary>
        /// Retrieves all transactions for the currently logged-in user.
        /// </summary>
        /// <returns>An IActionResult representing the response containing the user's transactions.</returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpGet]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetTransactionsByUserId()
        {
            var userId = UserContextService.GetCurrentUser(); // Gets the ID of the currently logged-in user.
            var transactions = await transactionService.GetAllByUser(userId); // Retrieves all transactions for the user.

            // Builds a BaseResponse object containing the user's transactions and returns an Ok response.
            var response = new BaseResponse<IEnumerable<TransactionDto>>(ReplyMessage.MESSAGE_QUERY, transactions, (int)HttpStatusCode.OK);
            return Ok(response);
        }

        /// <summary>
        /// Get all transactions in the database. This operation can only be accessed by an administrator.
        /// </summary>
        /// <returns>A list of all transactions in the database.</returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetAll()
        {
            // Retrieve all transactions from the database
            var transactions = await transactionService.GetAllTransactions();

            // Build a new BaseResponse object with a success message, the retrieved transactions, and a success status code
            var response = new BaseResponse<IEnumerable<TransactionDto>>(ReplyMessage.MESSAGE_QUERY, transactions, (int)HttpStatusCode.OK);

            // Return a success response with the BaseResponse object as the response body
            return Ok(response);
        }

        /// <summary>
        /// Gets a transaction by its ID for the currently logged-in user.
        /// </summary>
        /// <param name="id">The ID of the transaction.</param>
        /// <returns>An IActionResult containing a BaseResponse object with a TransactionDto and metadata about the response.</returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            // Get the transaction with the given ID.
            var transaction = await transactionService.GetTransactionById(id);

            // Create a BaseResponse object with the transaction and metadata about the response.
            var response = new BaseResponse<TransactionDto>(ReplyMessage.MESSAGE_QUERY, transaction, (int)HttpStatusCode.OK);

            // Return the response as an IActionResult.
            return Ok(response);
        }

        /// <summary>
        /// Deletes a transaction by its Id. This operation can only be accessed by an administrator.
        /// </summary>
        /// <param name="transactionId">The Id of the transaction to delete.</param>
        /// <returns>An IActionResult containing a BaseResponse object indicating if the transaction was deleted or not; otherwise, the middleware detects the error and returns the same
        /// </returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpDelete("{transactionId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            var result = await transactionService.DeleteTransaction(transactionId);

            var response = new BaseResponse<bool>($"Transaction deleted.", result, (int)HttpStatusCode.OK);

            return Ok(response);
        }

        /// <summary>
        /// Endpoint to reverse a payment. This operation can only be accessed by an administrator.
        /// </summary>
        /// <param name="transactionId">id of the transaction</param>
        /// <param name="concept">concept of the transaction (by default is repayment), extrated from the body</param>
        /// <returns>if all goes well, it returns a 200 status code with a BaseResponse object; otherwise, the middleware detects the error and returns the same</returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpPut("{transactionId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> UpdateTransaction(int transactionId, [FromBody] RepaymentResponseDto repaymentResponse)

        {
            var repayment = await transactionService.UpdateTransaction(transactionId, repaymentResponse.Concept );
            var response = new BaseResponse<bool>("successful repayment", repayment, (int)HttpStatusCode.OK);

            return Ok(response);
        }

        /// <summary>
        /// Creates a new transaction in the database. This operation can only be accessed by an administrator.
        /// </summary>
        /// <param name="transactionDTO">The TransactionRequestDto object containing the transaction data to be created.</param>
        /// <returns>Returns a BaseResponse object with a boolean value indicating if the transaction was created successfully or not.</returns>
        /// <response code="200">Returns the requested transaction.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested transaction was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDto transactionDTO)
        {
            var transaction = await transactionService.Insert(transactionDTO);

            // Builds a new BaseResponse object using a boolean value indicating if the transaction was created successfully or not, and the corresponding HTTP status code.
            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_CREATE_SUCCESS, transaction, (int)HttpStatusCode.Created);
            return Ok(response);
        }


        [HttpPut("activate/{transactionId}")]
        public async Task <IActionResult> ActivateTransaction(int transactionId)
        {
            var transaction = await transactionService.ActivateTransaction(transactionId);
            return Ok(transaction);
        }
    }
}
