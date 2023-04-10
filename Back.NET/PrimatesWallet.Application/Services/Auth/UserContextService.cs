using Microsoft.AspNetCore.Http;
using PrimatesWallet.Application.Interfaces;
using System.Security.Claims;


namespace PrimatesWallet.Application.Services.Auth
{
    public class UserContextService : IUserContextService
    {
        public readonly IHttpContextAccessor HttpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }
        public int GetCurrentUser()
        {
            string userId = HttpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(userId);
        }
    }
}
