using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Api.Helpers;
using PrimatesWallet.Application.DTOS;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using PrimatesWallet.Application.Services.Auth;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IUserContextService userContextService;

        public UserController(IUserService userService, IAccountService accountService, IUserContextService userContextService)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.userContextService = userContextService;
        }

        // GET: api/User/1
        /// <summary>
        /// Get a User by id and show details
        /// </summary>     
        /// <param name="id">Get user searching by id</param>
        /// <response code="401">Unauthorized user for this operation.</response>              
        /// <response code="200">Successful operation.</response>        
        /// <response code="404">NotFound. The requested operation was not found.</response>
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Get a specific User", Description = "Get a specific user by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "NotFound. The requested operation was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            try
            {
                var users = await userService.GetUserById(id);
                var response = new BaseResponse<UserResponseDto>(ReplyMessage.MESSAGE_QUERY, users, (int)HttpStatusCode.OK);
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

        /// <summary>
        /// Get all users in the database. This operation can only be accessed by an administrator.
        /// </summary>
        /// <returns>A list of all users in the database.</returns>
        /// <response code="200">Returns the requested users.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested users was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        [SwaggerOperation(Summary = "Get a paginated list of accounts", Description = "Retrieves a paginated list of accounts.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, int pageSize = 10)
        {
            var users = await userService.GetUsers(page, pageSize); //obtenemos solo los usuarios que necesitamos
            var totalPages = await userService.TotalPageUsers(pageSize); //obtenemos el total de paginas
            string url = CurrentURL.Get(HttpContext.Request); //Clase estatica en helpers para obtener la url como string


            var response = new BasePaginateResponse<IEnumerable<UserResponseDto>>()
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
        /// Creates a new user in the database. This operation can only be accessed by an administrator.
        /// </summary>
        /// <param name="RegisterUserDto">The RegisterUserDto object containing the user data to be created.</param>
        /// <returns>Returns a BaseResponse object with a boolean value indicating if the user was created successfully or not.</returns>
        /// <response code="200">Returns the requested user.</response>
        /// <response code="401">Returns if the user is unauthorized for this operation.</response>
        /// <response code="404">Returns if the requested user was not found.</response>
        /// <response code="500">Returns if there was an internal server error.</response>
        [HttpPost("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto user)
        {
            var newUser = await userService.Signup(user);
                if (newUser == 0) return BadRequest();
            var account =await accountService.Create(newUser);
            var userDTO = new RegisterUserDto() { First_Name = user.First_Name, Last_Name = user.Last_Name, Email = user.Email };
            var response = new BaseResponse<RegisterUserDto>(ReplyMessage.MESSAGE_QUERY, userDTO, (int)HttpStatusCode.Created);
            

            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Deletes an user.
        /// </summary>
        /// <param name="userId">ID of the user to delete.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">"The requested resource was not found..</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [SwaggerOperation(Summary = "Delete an User.", Description = "Deletes an User by its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await userService.GetUserById(userId);

            await userService.DeleteUser(userId);
            return Ok($"user {user.First_Name} {user.Last_Name} deleted.");
        }

        /// <summary>
        /// Update an existing User.
        /// </summary>
        /// <param name="UserId">User ID obtained from the request URL</param>
        /// <param name="userUpdateDTO">User model obtained from the request body</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>   
        /// <response code="404">"The requested resource was not found.</response>              
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [SwaggerOperation(Summary = "Update an existing Account.", Description = "Update an existing User.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
        [Authorize]
        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser(int UserId, [FromBody] UserUpdateDto userUpdateDTO)
        {
            var updatedUser = await userService.UpdateUser(UserId, userUpdateDTO);
            
            return Ok(updatedUser);
        }

        /// <summary>
        /// Activates an user by its ID.
        /// </summary>
        /// <param name="userId">The ID of the user to activate.</param>
        /// <response code="200">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        /// <response code="404">"The requested resource was not found..</response>  
        /// <response code="500">Internal Server Error. Something has gone wrong on the Primates Wallet server.</response>
        [SwaggerOperation(Summary = "Activate an User.", Description = "Only admins have permission to perform this operation.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The requested resource was not found.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal Server Error")]
       [HttpPut("activate/{userId}")]
        public async Task<IActionResult>ActivateUser(int userId)
        {
            var user = await userService.ActivateUser(userId);
            return Ok(user);
        }

        /// <summary>
        /// Converts user points to a product.
        /// </summary>
        /// <param name="pointsDto">The object containing the user ID and the points to convert.</param>
        /// <response code="204">Successful operation</response>     
        /// <response code="401">Unauthorized user for this operation.</response>
        [SwaggerOperation(Summary = "Converts user points to a product.", Description = "Only authorized users can perform this operation.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Successful operation")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized user for this operation.")]
        [Authorize]
        [HttpPut("Product")]
        public async Task<IActionResult> PointsToProduct([FromBody] PointsDto pointsDto)
        {
            var userId = userContextService.GetCurrentUser();
            await userService.UpdatePoints(userId, pointsDto.points);
            return NoContent();

        }
    }
}
