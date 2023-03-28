using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;

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

        [HttpGet]
        public async Task<IActionResult> GetRoleById(int id)
        {
            Role role = await _roleService.GetRoleById(id);
            if (role == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No role found by id{id}");
            }
            return StatusCode(StatusCodes.Status200OK, role);

        }
    }
}
