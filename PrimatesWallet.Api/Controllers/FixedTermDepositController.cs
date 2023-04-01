using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixedTermDepositById(int id)
        {
            try
            {
                var userRequestId =  userContextService.GetCurrentUser();
                var fixedTermDeposit = await _fixedTermDeposit.GetFixedTermDepositDetails(userRequestId, id);
                if ( fixedTermDeposit == null) { return NotFound("No se encontró el plazo fijo"); }

                return Ok(fixedTermDeposit);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetByUser()
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
    }
}