using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services;
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

        public FixedTermDepositController(IFixedTermDepositService fixedTermDeposit, IUserContextService userContextService)
        {
            _fixedTermDeposit = fixedTermDeposit;
            this.userContextService = userContextService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixedTermDepositById([FromBody] int id)
        {

            // Falta la validacion del ID de account que hace el request que se recibe ese id por JWT
            try
            {
                var fixedTermDeposit = await GetFixedTermDepositById(id);

                if (fixedTermDeposit == null) { return NotFound(); }

                return Ok(fixedTermDeposit);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


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
    }
}