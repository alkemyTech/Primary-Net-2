using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/FixedDeposit")]
    [ApiController]
    public class FixedTermDepositController : ControllerBase
    {
        private readonly IFixedTermDepositService _fixedTermDeposit;

        public FixedTermDepositController(IFixedTermDepositService fixedTermDeposit)
        {
            _fixedTermDeposit = fixedTermDeposit;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFixedTermDepositById( [FromBody] int id)
        {

        // Falta la validacion del ID de account que hace el request que se recibe ese id por JWT
            var fixedTermDeposit = await GetFixedTermDepositById(id);

            if (fixedTermDeposit == null) { return NotFound(); }

            return Ok(fixedTermDeposit);

        }
    }
}