using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.ViewModels;
using server.Enteties;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using server.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.IdentityModel.Tokens;
using server.JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;

namespace server.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly weatherContext _wc;


        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, weatherContext wc)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _wc = wc;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {
                    var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (checkPassword.Succeeded)
                    {
                        var now = DateTime.UtcNow;

                        var roles = await _userManager.GetRolesAsync(user);

                        var encodedJwt = GenerateEncodedJwt(user, now, "user");

                        // await AuthenticateUser(model.Email); // аутентификация

                        return Ok(new { access_token = encodedJwt });
                    }

                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                return BadRequest(ModelState);
            }
            ModelState.AddModelError("UserName", "Пользователя с таким логином не существует");
            return BadRequest(ModelState);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //  SDas2dasD)
                Users user = new Users { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        private async Task<Users> AuthenticateUser(string email, string password)
        {
            return await _wc.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);
        }

        private string GenerateEncodedJwt(Users user, DateTime now, string role)
        {
            var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                            new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                        };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthTokenOptions.ISSUER,
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddSeconds(AuthTokenOptions.LIFETIME),
                signingCredentials: new SigningCredentials(AuthTokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}