using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using System.Net;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IJwtJervice jwtJervice;
        private readonly IUserContextService userContextService;
        private readonly IUserService userService;

        public AuthController(IAuthService authService, IJwtJervice jwtJervice, IUserContextService userContextService, IUserService userService)
        {
            this.authService = authService;
            this.jwtJervice = jwtJervice;
            this.userContextService = userContextService;
            this.userService = userService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUser)
        {
            //Autentica las credenciales y devuelve un usuario
            var user = await authService.Authenticate(loginUser);

            //Si el usuario no existe o las credenciales son invalidas retorna el error y su respectivo mensaje con código de estado.
            if (user == null) throw new AppException("Invalid email/password", HttpStatusCode.BadRequest);
              
            //Si existe el usuario y las credenciales son validas, se genera el token a partir de ese usuario
            var token = jwtJervice.Generate(user);

            //Se retorna el token
            return Ok(token);

        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
        //Devuelve los datos del usuario logueado
            var userId = userContextService.GetCurrentUser();
            var user =await userService.GetUserById(userId);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);

            return Ok(user);

        }
    }
}
