using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;


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

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> CreateRole(RoleCreationDto roleCreationDto)
        {
            if (roleCreationDto.Name == null || roleCreationDto.Description == null) throw new AppException("Missing required parameters", HttpStatusCode.BadRequest);
            var response = await _roleService.CreateRole(roleCreationDto);
            return Ok(response);
        }


        /// <summary>
        /// Deletes a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role to be deleted.</param>
        /// <returns>Returns an HTTP status code and a message indicating the success or failure of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Deletes a role.", Description = "Deletes a role by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]

        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var response = await _roleService.DeleteRol(id);

            var message = response is true ? "Deletion successful." : "Resource deletion failed, please contact support.";
            HttpStatusCode statusCode = response is true ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

            var result = new BaseResponse<bool>(message, response, (int)statusCode);
            return StatusCode((int)statusCode, result);
        }

        [HttpPut("activate/{roleId}")]
        public async Task<IActionResult> ActivateFixedDeposit(int roleId)
        {
            var role = await _roleService.ActivateRole(roleId);
            return Ok(role);

        }
    } 
}
