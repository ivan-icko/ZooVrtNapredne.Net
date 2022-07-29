using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Helpers;


namespace WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<Employee> manager;
        private readonly SignInManager<Employee> signIn;
        private readonly RoleManager<IdentityRole<int>> roleManager;

        public AuthController(UserManager<Employee> manager,SignInManager<Employee> signIn,RoleManager<IdentityRole<int>> roleManager)
        {
            this.manager = manager;
            this.signIn = signIn;
            this.roleManager = roleManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel register)
        {
            Employee e = new Employee()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName= register.Username

            };

            var res = await manager.CreateAsync(e,register.Password);
           // roleManager.CreateAsync(new IdentityRole<int>() { Name = "Neko" });

            if (res.Succeeded)
            {
                if(!await roleManager.RoleExistsAsync(Roles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(Roles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(Roles.Executive))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(Roles.Executive));
                }

                if (register.Admin)
                {
                    await manager.AddToRoleAsync(e, Roles.Admin);
                }
                else
                {
                    await manager.AddToRoleAsync(e, Roles.Executive);
                }
                
                return RedirectToAction("Login");
            }
            else
            {
                if (res.Errors.Any(e => e.Code == "DuplicateUserName"))
                    ModelState.AddModelError("Username", res.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName")?.Description);
                if (res.Errors.Any(e => e.Code.Contains("Password")))
                    ModelState.AddModelError("Password", "Error in credentials");
                return View();
            }

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] RegisterViewModel login)
        {
           var result =  await signIn.PasswordSignInAsync(login.Username, login.Password,false,false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Model");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signIn.SignOutAsync();
            return RedirectToAction("Login");
        }
       


    }
}
