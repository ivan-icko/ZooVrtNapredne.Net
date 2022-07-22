using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<Employee> manager;
        private readonly SignInManager<Employee> signIn;

        public AuthController(UserManager<Employee> manager,SignInManager<Employee> signIn)
        {
            this.manager = manager;
            this.signIn = signIn;
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

            var result = await manager.CreateAsync(e,register.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
                    ModelState.AddModelError("Username", result.Errors.FirstOrDefault(e => e.Code == "DuplicateUserName")?.Description);
                if (result.Errors.Any(e => e.Code.Contains("Password")))
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


    }
}
