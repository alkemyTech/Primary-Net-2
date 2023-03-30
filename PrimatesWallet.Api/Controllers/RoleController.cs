using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        // GET: api/Role/1
        /// <remarks>
        /// Get role by id and show details
        /// </remarks>     
        /// <param name="id">Get role searching by id</param>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>        
        /// <response code="404">NotFound. The requested operation was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get a specific item", Description = "Get a specific item by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]

        public async Task<IActionResult> GetRoleById(int id)
        {
            Role role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No role found by id{id}");
            }
            return StatusCode(StatusCodes.Status200OK, role);
        }



        // GET: api/Role     
        /// <remarks>
        /// Get roles and show details
        /// </remarks>     
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>        
        /// <response code="404">NotFound. The requested operation was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet]
        [Authorize]

        [SwaggerOperation(Summary = "Get specific list", Description = "Get role list")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            if (roles == null) { return NotFound(); }
            return Ok(roles);
        }

    }
}
