using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services.Auth;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueService _catalogueService;
        private readonly IUserContextService _userContextService;

        public CatalogueController(ICatalogueService catalogueService, IUserContextService userContextService) 
        {
            _catalogueService = catalogueService;
            _userContextService = userContextService;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(CatalogueProductDto catalogueProductDTO)
        {
            var userId =  _userContextService.GetCurrentUser();
            var response = await _catalogueService.CreateProduct(catalogueProductDTO, userId);
            return Ok(response);
        }

    }
}
