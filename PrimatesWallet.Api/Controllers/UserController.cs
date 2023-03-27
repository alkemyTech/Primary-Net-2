using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
    }
}
