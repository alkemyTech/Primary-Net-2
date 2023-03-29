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

        public AuthController(IAuthService authService, IJwtJervice jwtJervice)
        {
            this.authService = authService;
            this.jwtJervice = jwtJervice;
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
    }
}
