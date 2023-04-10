using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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

        // GET: api/Catalogue/1
        /// <summary>
        /// Get a product by id and show details
        /// </summary>     
        /// <param name="id">Get product searching by id</param>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>        
        /// <response code="404">NotFound. The requested operation was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get a specific product", Description = "Get a specific product by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _catalogueService.GetProductById(id);
            if (product is null) return NotFound();
            return Ok(product);
        }


        // POST: api/Catalogue
        /// <summary>
        /// Creates a new product in the catalogue.
        /// </summary>    
        /// <remarks>
        /// Sample request:
        /// 
        ///     
        ///     {
        ///         "productDescription": "A sample product description.",
        ///         "image": www.example.com/image/231dasda43324,
        ///         "points: 500
        ///     }
        /// 
        /// </remarks>     
        /// <param name="catalogueProductDTO">The details of the product to create.</param>
        /// <response code="200">Successful operation.</response>  
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="404">NotFound. The requested operation was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Create a new product in the catalogue.", Description = "Creates a new product in the catalogue.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CreateProduct(CatalogueProductDto catalogueProductDTO)
        {
            var userId = _userContextService.GetCurrentUser();
            var response = await _catalogueService.CreateProduct(catalogueProductDTO, userId);
            return Ok(response);
        }


        /// <summary>
        /// Returns a list of all products.
        /// </summary>
        /// <remarks>
        /// This endpoint allows the client to retrieve a paginated list of products from the catalogue. 
        /// The response includes an object with the url of the previous page, the results and the url of the next page.
        /// </remarks>
        /// <param name="page">The page number to retrieve (1-based).</param>
        /// <param name="pageSize">The maximum number of products to return per page (default: 10).</param>
        /// <response code="200">Returns the paginated list of products.</response>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="404">"NotFound. The requested operation was not found.</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet]
        [Authorize]
        [SwaggerOperation(Summary = "Get a paginated list of products", Description = "Retrieves a paginated list of products from the catalogue.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> CataloguePagination([FromQuery] int page, int pageSize = 10)
        {
            //We try to convert the 'page' query that comes to us by parameter to an integer. If we can't the default number is 1.
            string url = CurrentURL.Get(HttpContext.Request);

            var response = await _catalogueService.CataloguePagination(page, url, pageSize);

            return Ok(response);
        }


        /// <summary>
        /// Update an existing product in the catalogue.
        /// </summary>
        /// <param name="id">Product ID obtained from the request URL</param>
        /// <param name="catalogue">Product model obtained from the request body</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>   
        /// <response code="404">"NotFound. The requested operation was not found.</response>              
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Update an existing product in the catalogue.", Description = "Update an existing product in the catalogue.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> UpdateCatalogue(int id, [FromBody] CatalogueDTO catalogue)
        {
            var result = await _catalogueService.UpdateProduct(id, catalogue);

            var response = new BaseResponse<bool>(ReplyMessage.MESSAGE_QUERY, result, (int)HttpStatusCode.NoContent);

            return Ok(response);
        }

        /// <summary>
        /// Deletes a product from the catalogue.
        /// </summary>
        /// <param name="id">ID of the product to delete.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">"NotFound. The requested operation was not found.</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpDelete("{catalogueId}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Delete a product.", Description = "Deletes a product from the catalogue by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> DeleteCatalogue(int catalogueId)
        {
            var currentUser = _userContextService.GetCurrentUser();
            var deleteCatalogue = await _catalogueService.DeleteCatalogue(catalogueId, currentUser);
            return Ok(deleteCatalogue);
        }
    }
}
