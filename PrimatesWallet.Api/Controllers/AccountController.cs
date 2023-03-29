using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Exceptions;
using PrimatesWallet.Application.Helpers;
using PrimatesWallet.Application.Interfaces;
using System.Net;

namespace PrimatesWallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _account;

        public AccountController(IAccountService account)
        {
            _account = account;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _account.GetAccountsList();
            if (accounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No accounts in database.");
            }

            return StatusCode(StatusCodes.Status200OK, accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountDetails(int id)
        {

            try
            {
                var account = await _account.GetAccountById(id);

                if (account is null) return NotFound($"The account with id: {id} does not exist");

                return Ok(account);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }


    }
}
