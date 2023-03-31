using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using System.Net;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService userService;
        private readonly IAccountService accountService;

        public UserController(IUserService userService, IAccountService accountService)
        {
            this.userService = userService;
            this.accountService = accountService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async  Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var Users = await userService.GetUserById(id);
                var response = new BaseResponse<User>(ReplyMessage.MESSAGE_QUERY, Users, (int)HttpStatusCode.OK);
                return Ok(response);

            } catch (AppException ex)
            {
                //atrapamos las excepciones y le damos un formato,
                //pendiente de middleware para definir esto en un solo lugar
                var response = new BaseResponse<object>(ex.Message, null, (int)ex.StatusCode);
                return StatusCode(response.StatusCode, response);

            }catch(Exception ex) 
            {
                var response = new BaseResponse<object>(ex.Message,null, (int)HttpStatusCode.InternalServerError);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            //Falta la validacion del JWT para el Rol de admin , pero esta ubicado en tickets posteriores

            var users = await userService.GetUsers();
            
            if ( users == null) { return NotFound(); }

            return Ok(users);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO user)
        {
            var newUser = await userService.Signup(user);
                if (newUser == 0) return BadRequest();
            var account = accountService.Create(newUser);
            var userDTO = new RegisterUserDTO() { First_Name = user.First_Name, Last_Name = user.Last_Name, Email = user.Email, Password = user.Password };
            var response = new BaseResponse<RegisterUserDTO>(ReplyMessage.MESSAGE_QUERY, userDTO, (int)HttpStatusCode.Created);
            

            return StatusCode(response.StatusCode, response);
        }
   

    }
}
