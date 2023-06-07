//using CargoManagerSystem.DatabaseContext;
using CargoManagerSystem.Models;
using CargoManagerSystem.Models.AuthFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CargoManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> loginManager;
        private readonly RoleManager<MyIdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        MyIdentityDbContext context = new MyIdentityDbContext();
        

       
        public UserManager<MyIdentityUser> UserManager { get; private set; }

        public AccountController(UserManager<MyIdentityUser> userManager,
           SignInManager<MyIdentityUser> loginManager,
           RoleManager<MyIdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
            _configuration = configuration;

        }

      

        

        [HttpPost]      
        [Route("register")]
        public IActionResult Register(RegisterViewModel obj)
        {
            IdentityResult result=null;
            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                //user.FullName = obj.FullName;
                //user.BirthDate = obj.BirthDate;

               result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("NormalUser").Result)
                    {
                        MyIdentityRole role = new MyIdentityRole();
                        role.Name = "NormalUser";
                        role.Description = "Perform normal operations.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Role creation failed! Please check user details and try again.");
                             return StatusCode(500, ModelState);
                           
                        }
                    }                

                }
                //Adding User To Role
                if (roleManager.RoleExistsAsync("NormalUser").Result)
                {
                    userManager.AddToRoleAsync(user, "NormalUser");
                }
            }
            //  return Ok("User created successfully!" );
            return Ok(result);
        }

        [HttpPost]
        [Route("register-admin")]
        public IActionResult RegisterAdmin(RegisterViewModel obj)
        {
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                MyIdentityUser user = new MyIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                //user.FullName = obj.FullName;
                //user.BirthDate = obj.BirthDate;

                result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded)
                {
                    if (!roleManager.RoleExistsAsync("AdminUser").Result)
                    {
                        MyIdentityRole role = new MyIdentityRole();
                        role.Name = "AdminUser";
                        role.Description = "Perform Admin operations.";
                        IdentityResult roleResult = roleManager.
                        CreateAsync(role).Result;
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "Role creation failed! Please check user details and try again.");
                            return StatusCode(500, ModelState);

                        }
                    }

                }
                //Adding User To Role
                if (roleManager.RoleExistsAsync("AdminUser").Result)
                {
                    userManager.AddToRoleAsync(user, "AdminUser");
                }
            }
            // return Ok("Admin User created successfully!");
            return Ok(result);
        }

        //public IActionResult Login()
        //{
        //    return View();
        //}


        [HttpPost]      
        [Route("login")]
        public IActionResult Login(LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var result = loginManager.PasswordSignInAsync(obj.UserName, obj.Password, obj.RememberMe, false).Result;
                var role = GetUserRole(obj.UserName);

               

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,obj.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );



                if (result.Succeeded)
                {
                   
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        username=obj.UserName,
                        Role=role.Result
                    });
                }
            }
            return Unauthorized();
        }
        [HttpGet]
        public async Task<string> GetUserRole(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var role = "";
            if (userManager.IsInRoleAsync(user, "NormalUser").Result)
            {
                role = "NormalUser";
            }
            else
            {
                role = "AdminUser";
            }

            return role;
        }



        [HttpPost]       
        public IActionResult LogOff()
        {
            loginManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }
    }
}
