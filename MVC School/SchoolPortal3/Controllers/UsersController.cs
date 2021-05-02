using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal3.Data;
using SchoolPortal3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPortal3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController( UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //Lista alla användare
        public async Task<IActionResult> Index()
        {
            ViewBag.Admins = await _userManager.GetUsersInRoleAsync("Admin");
            ViewBag.ProgramManagers = await _userManager.GetUsersInRoleAsync("Program Manager");
            //ViewBag.Teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            ViewBag.Students = await _userManager.GetUsersInRoleAsync("Student");

           

            return View();
        }

        // GET: SchoolClasses/Create
        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles;
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = CreateUser(model);
                var result = await _userManager.CreateAsync(user,"BytMig123!");

                if (result.Succeeded)
                {        
                        await _userManager.AddToRoleAsync(user, model.Role);
                        return RedirectToAction("Index", "Users");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            ViewBag.Roles = _roleManager.Roles;
            return View();
        }


        public ApplicationUser CreateUser(UserViewModel model)
        {
            return new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Role = model.Role
            };
        }











    }
}
