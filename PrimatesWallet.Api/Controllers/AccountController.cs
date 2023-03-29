using Microsoft.AspNetCore.Mvc;
using PrimatesWallet.Application.Interfaces;

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


    }
}
