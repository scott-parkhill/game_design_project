using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Chaos.Models.DbModels;

namespace Chaos.Business;

public class AccountController : Controller
{
    readonly UserManager<GameUser> _userManager;
    readonly SignInManager<GameUser> _signInManager;
    readonly IDataProtector _dataProtector;

    public AccountController(UserManager<GameUser> userManager, SignInManager<GameUser> signInManager, IDataProtectionProvider dataProtectionProvider)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dataProtector = dataProtectionProvider.CreateProtector("SignIn");
    }

    [HttpGet("account/signin")]
    public async Task<IActionResult> SignIn(string token)
    {
        var data = _dataProtector.Unprotect(token);
        var parts = data.Split('|');
        var user = await _userManager.FindByIdAsync(parts[0]);
        var isTokenValid = await _userManager.VerifyUserTokenAsync(user!, TokenOptions.DefaultProvider, "SignIn", parts[1]);

        if (isTokenValid)
        {
            await _signInManager.SignInAsync(user!, Convert.ToBoolean(parts[2]));
            return Redirect("/");
        }

        return Unauthorized("Are you hacking?");
    }

    [HttpGet("account/logout")]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }
}
