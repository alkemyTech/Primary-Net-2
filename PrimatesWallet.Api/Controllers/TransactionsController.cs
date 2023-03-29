using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TransactionsController : ControllerBase
    {
        //Se deja preparado el controlador de Transacciones con la DI de servicios
        //Se deja pendiente el desarrollo de los endpoints asignados.
        private readonly ITransactionService _transaction;

        public TransactionsController(ITransactionService transaction)
        {
            _transaction = transaction;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById([FromBody] int id)
        {
            try
            {
                var transaction = await GetTransactionById(id);

                if (transaction == null) { return NotFound(); }

                return Ok(transaction);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
