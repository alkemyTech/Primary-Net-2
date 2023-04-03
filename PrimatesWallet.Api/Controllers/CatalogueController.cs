using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Application.Services.Auth;
using static System.Net.Mime.MediaTypeNames;

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
            var product = await _catalogueService.GetProductById(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct(CatalogueProductDto catalogueProductDTO)
        {
            var userId =  _userContextService.GetCurrentUser();
            var response = await _catalogueService.CreateProduct(catalogueProductDTO, userId);
            return Ok(response);
        }

        /// <summary>
        /// this end point is to obtain the products listed by 10, depending on the page number.
        /// </summary>
        /// <returns>An object with the url of the previous page, the results and the url of the next page.</returns>
        [HttpGet("paginate")]
        [Authorize]
        public async Task<IActionResult> CataloguePagination([FromQuery] int page, int pageSize = 10)
        {
            //We try to convert the 'page' query that comes to us by parameter to an integer. If we can't the default number is 1.
            string url = CurrentURL.Get(HttpContext.Request);

            var response = await _catalogueService.CataloguePagination(page, url, pageSize);

            return Ok(response);
        }

    }
}
