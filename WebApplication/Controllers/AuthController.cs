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

        public AuthController(UserManager<Employee> manager)
        {
            this.manager = manager;
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel register)
        {
            Employee e = new Employee()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,

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
        public IActionResult Login([FromForm] RegisterViewModel register)
        {
            return View();
        }


    }
}
