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
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            try
            {
                //Autentica las credenciales y devuelve un usuario
                var user = await authService.Authenticate(loginUser);

                if (user != null)
                {
                    //Si existe el usuario y las credenciales son validas, se genera el token a partir de ese usuario
                    var token = jwtJervice.Generate(user);

                    //Se retorna el token
                    return Ok(token);
}
                else
                {
                    return BadRequest();
                }
            }
            catch (AppException ex)
            {
                //Si el usuario no existe o las credenciales son invalidas retorna el error y su respectivo mensaje con código de estado.
                var response = new BaseResponse<object>(ex.Message, null, (int)HttpStatusCode.InternalServerError);
                return StatusCode(response.StatusCode, response);
            }

        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
        //Devuelve los datos del usuario logueado
            var userId = userContextService.GetCurrentUser();
            var user =await userService.GetUserById(userId);
            if (user == null) throw new AppException("User not found", HttpStatusCode.NotFound);
            var userResponse = new UserResponseDTO()
            {
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                Points = user.Points
            };
            return Ok(userResponse);

        }
    }
}
