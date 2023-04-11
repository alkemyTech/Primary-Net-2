using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
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

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="loginUser">The user's login information.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="400">Invalid email/password</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Authenticate user and generate JWT token.", Description = "Authenticates the user and generates a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid email/password")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
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


        /// <summary>
        /// Retrieves the details of the authenticated user.
        /// </summary>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">The requested resource was not found.</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(Summary = "Get authenticated user details.", Description = "Retrieves the details of the authenticated user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
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
