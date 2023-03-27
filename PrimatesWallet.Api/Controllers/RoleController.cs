using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Interfaces;

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

    }
}
