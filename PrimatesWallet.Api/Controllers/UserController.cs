using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var Users = await userService.GetUserById(id);
                var response = new BaseResponse<User>(ReplyMessage.MESSAGE_QUERY, Users, (int)HttpStatusCode.OK);
                return Ok(response);

            }
            catch (AppException ex)
            {
                //atrapamos las excepciones y le damos un formato,
                //pendiente de middleware para definir esto en un solo lugar
                var response = new BaseResponse<object>(ex.Message, null, (int)ex.StatusCode);
                return StatusCode(response.StatusCode, response);

            }
            catch (Exception ex)
            {
                var response = new BaseResponse<object>(ex.Message, null, (int)HttpStatusCode.InternalServerError);
                return StatusCode(response.StatusCode, response);
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, int pageSize = 10)
        {
            var users = await userService.GetUsers(page, pageSize); //obtenemos solo los usuarios que necesitamos
            var totalPages = await userService.TotalPageUsers(pageSize); //obtenemos el total de paginas
            string url = getURL(); //esamos el metodo para obtener la url


            var response = new BasePaginateResponse<IEnumerable<User>>()
            {
                Message = ReplyMessage.MESSAGE_QUERY,
                Result = users,
                Page = page,
                NextPage = (page < totalPages) ? $"{url}?page={page + 1}" : "None",
                PreviousPage = (page == 1) ? "none" : $"{url}?page={page - 1}",
                StatusCode = (int)HttpStatusCode.OK
            };
            return Ok(response);
        }



        /// <summary>
        /// Returns the current URL of the HTTP request received in the current context.
        /// </summary>
        /// <returns>A string representing the current URL.</returns>
        private string getURL()
        {
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host;
            var pathBase = HttpContext.Request.PathBase;
            var path = HttpContext.Request.Path;
            return $"{scheme}://{host}{pathBase}{path}";
        }
    }
}
