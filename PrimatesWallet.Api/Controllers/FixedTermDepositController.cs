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

        [Authorize]
        [HttpGet("{id}", Name = "Get one FixedTermDeposit")]
        public async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            // Obtener un plazo fijo espescifico por id
            var userRequestId = userContextService.GetCurrentUser();
            var fixedTermDeposit = await _fixedTermDeposit.GetFixedTermDepositDetails(userRequestId, id);
            var response = new BaseResponse<FixedTermDepositDetailDTO>(ReplyMessage.MESSAGE_QUERY, fixedTermDeposit, (int)HttpStatusCode.OK);
            return Ok(response);

        }

        [Authorize]
        [HttpGet("User Deposits")]
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


        [HttpGet(Name = "Get All")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFixedTermDeposits([FromQuery] int page = 1, int pageSize = 10)
        {
            var allDeposits = await _fixedTermDeposit.GetDeposits(page, pageSize); //obtenemos solo los plazos fijos que necesitamos
            var totalPages = await _fixedTermDeposit.TotalPageDeposits(pageSize); //obtenemos el total de paginas
            string url = CurrentURL.Get(HttpContext.Request); //Clase estatica en helpers para obtener la url como string

            var response = new BasePaginateResponse<IEnumerable<FixedTermDepositDetailDTO>>()
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

        /// // POST: api/FixedDeposit
        /// <remarks>
        /// Creates a new fixed-term deposit with the specified details.
        /// The date format must be yyyy-MM-dd and the amount must be greater than 100.
        /// </remarks>     
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFixedTermDeposit([FromBody] FixedTermDepositRequestDTO fixedTermDTO)
        {
            var idUser = userContextService.GetCurrentUser();
            var fixedTerm = await _fixedTermDeposit.Insert(idUser, fixedTermDTO);
            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_CREATE_SUCCESS, fixedTerm, (int)HttpStatusCode.OK);
            return Ok(response);
        }
    }
}