using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueService _catalogueService;

        public CatalogueController(ICatalogueService catalogueService) 
        {
            _catalogueService = catalogueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allCatalogue = await _catalogueService.GetAllProducts();

                return Ok(allCatalogue);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _catalogueService.GetProductById(id);

                if (product is null) return NotFound();
                return Ok(product);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
