using System.Security.Claims;
using BlazorLearnWebApp.Entity;
using BlazorLearnWebApp.Vo;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorLearnWebApp.Controllers;

[ApiController]
[Route("/api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    [HttpPost]
    public async Task<object> Login([FromBody]LoginVo loginVo)
    {
        var user = UserEntity.Where(x => x.UserName == loginVo.UserName && x.Password == loginVo.Password).First();
        if (user == null)
        {
            return new { Code = 50000, Message = "用户名或密码错误" };
        }

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
        identity.AddClaim(new Claim(ClaimTypes.Role, user.RoleId.ToString()));

        await HttpContext.SignInAsync(new ClaimsPrincipal(identity), new AuthenticationProperties()
        {
            IsPersistent = true,
            ExpiresUtc = loginVo.IsKeep ? DateTimeOffset.Now.AddDays(5) : DateTimeOffset.Now.AddMinutes(30)
        });
        return new { Code = 20000, Message = "登录成功" };
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/Login");
    }
}