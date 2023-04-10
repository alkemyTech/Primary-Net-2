using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;
using PrimatesWallet.Application.Services.Auth;
using PrimatesWallet.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Security.Principal;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/FixedDeposit")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositService _fixedTermDeposit;
        private readonly IUserContextService userContextService;

        public FixedTermDepositController(IFixedTermDepositService fixedTermDepositService, IUserContextService userContextService)
        {
            _fixedTermDeposit = fixedTermDepositService;
            this.userContextService = userContextService;

        }


        /// <summary>
        /// This endpoint is to get a fixed-term deposit, user must have token to access it
        /// </summary>
        /// <param name="id">Fixed-term deposit id</param>
        /// <returns>if everything goes well, it returns a status code 200 returning the deposit correctly</returns>
        [Authorize]
        [HttpGet("{id}", Name = "Get one FixedTermDeposit")]
        public async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            // Obtener un plazo fijo espescifico por id
            var userRequestId = userContextService.GetCurrentUser();
            var fixedTermDeposit = await _fixedTermDeposit.GetFixedTermDepositDetails(userRequestId, id);
            var response = new BaseResponse<FixedTermDepositDetailDto>(ReplyMessage.MESSAGE_QUERY, fixedTermDeposit, (int)HttpStatusCode.OK);
            return Ok(response);
        }
        /// <summary>
        /// This endpoint is to get all fixed-term deposits from one user, using own user token to validate it.
        /// </summary>
        /// <returns>if everything goes well, it returns a status code 200 and returning user deposits correctly</returns>

        [Authorize]
        [HttpGet("UserDeposits")]
        public async Task<IActionResult> GetByUser()


        //Obtener todos los Plazos fijos de un usuario
        {
            try
            {
                int userId = userContextService.GetCurrentUser();
                var FixedTermDeposit = await _fixedTermDeposit.GetByUser(userId);
                var response = new BaseResponse<IEnumerable<FixedTermDeposit>>(ReplyMessage.MESSAGE_QUERY, FixedTermDeposit, (int)HttpStatusCode.OK);
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
        /// This endpoint is to delete a fixed-term deposit, only users with Admin role can access it.
        /// </summary>
        /// <param name="id">Fixed-term deposit id</param>
        /// <returns>if everything goes well, it returns a status code 200 indicating that it was deleted correctly</returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFixedTermDeposit(int id)
        {
            var response = await _fixedTermDeposit.DeleteFixedtermDeposit(id);

            var result = new BaseResponse<bool>("Fixed term deposit eliminated", response, (int)HttpStatusCode.OK);

            return Ok(result);
        }

        /// <summary>
        /// This endpoint is to Get all a fixed-term deposits, only users with Admin role can access it.
        /// </summary>
        /// <param name="page">Fixed-term deposit page</param>
        /// <param name="pageSize">Fixed-term deposit items per page</param>
        /// <returns>if everything goes well, it returns a status code 200 returning all deposits paginated by page size</returns>

        [HttpGet(Name = "Get All")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFixedTermDeposits([FromQuery] int page = 1, int pageSize = 10)
        {
            var allDeposits = await _fixedTermDeposit.GetDeposits(page, pageSize); //obtenemos solo los plazos fijos que necesitamos
            var totalPages = await _fixedTermDeposit.TotalPageDeposits(pageSize); //obtenemos el total de paginas
            string url = CurrentURL.Get(HttpContext.Request); //Clase estatica en helpers para obtener la url como string


            var response = new BasePaginateResponse<IEnumerable<FixedTermDepositDetailDto>>()
            {
                Message = ReplyMessage.MESSAGE_QUERY,
                Result = allDeposits,
                Page = page,
                NextPage = (page < totalPages) ? $"{url}?page={page + 1}" : "None",
                PreviousPage = (page == 1) ? "none" : $"{url}?page={page - 1}",
                StatusCode = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }

        /// <summary>
        /// Creates a new fixed term deposit for the current user.
        /// </summary>
        /// <param name="fixedTermDTO">The FixedTermDepositRequestDTO object containing the fixed term deposit data to be created.</param>
        /// <returns>Returns a BaseResponse object with a boolean value indicating if the fixed term deposit was created successfully or not.</returns>
        /// <response code="200">Returns the requested fixed term deposit.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Create a new fixed term deposit for the current user", Description = "Creates a new fixed term deposit for the current user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CreateFixedTermDeposit([FromBody] FixedTermDepositRequestDTO fixedTermDTO)
        {
            var idUser = userContextService.GetCurrentUser();
            var fixedTerm = await _fixedTermDeposit.Insert(idUser, fixedTermDTO);
            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_CREATE_SUCCESS, fixedTerm, (int)HttpStatusCode.OK);
            return Ok(response);
        }

        /// <summary>
        /// Activates a fixed term deposit by its ID.
        /// </summary>
        /// <param name="depositId">The ID of the fixed term deposit to activate.</param>
        /// <response code="200">Successful operation</response>
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">The requested resource was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPut("activate/{depositId}")]
        [SwaggerOperation(Summary = "Activate a fixed term deposit.", Description = "Activates a fixed term deposit by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> ActivateFixedDeposit( int depositId)
        {
            var deposit = await _fixedTermDeposit.ActivateFixedTermDeposit(depositId);
            return Ok(deposit);

        }


        /// <summary>
        /// Updates a fixed term deposit by its ID.
        /// </summary>
        /// <param name="id">The ID of the fixed term deposit to update.</param>
        /// <param name="fixedTermDeposit">The data to update the fixed term deposit.</param>
        /// <response code="200">Successful operation</response>
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">The requested resource was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a Fixed Term Deposit.", Description = "Updates a fixed term deposit by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> UpdateFixedTerm(int id, [FromBody] FixedTermDepositRequestDTO fixedTermDeposit)
        {
            var result = await _fixedTermDeposit.UpdateFixedTermDeposit(id, fixedTermDeposit);

            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_QUERY, result, (int)HttpStatusCode.OK);

            return Ok(response);
        }
    }
}