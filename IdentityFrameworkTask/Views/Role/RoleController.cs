using IdentityFrameworkTask.DataModel;
using IdentityFrameworkTask.ViewModels;
using IdentityFrameworkTask.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityFrameworkTask.Views.Role
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> logger;
        private readonly RoleManager<AppRole> roleManager;
        private readonly appDbContext context;
        public RoleController(RoleManager<AppRole> roleManager, ILogger<RoleController> logger, appDbContext context)
        {
            this.roleManager = roleManager;
            this.logger = logger;
            this.context = context;
        } 
        [HttpGet]   
        public IActionResult Index()
        {
            List<RoleVM> model = new List<RoleVM>();

            model = roleManager.Roles.Select(r => new RoleVM
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                CreatedDate = r.CreatedDate,
                NumberOfUsers = context.UserRoles.Count(ur => ur.RoleId == r.Id)

            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
                var model = new RoleVM();
                model.Role = new AppRole();
                model.Role.CreatedDate = DateTime.Today;
                return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleVM model)
        {
            var roleExists = await roleManager.RoleExistsAsync(model.RoleName);
            if (ModelState.IsValid && !roleExists)
            {
                AppRole appRole = new AppRole();
                appRole.Name = model.RoleName;
                appRole.Description = model.Description;
                appRole.CreatedDate = model.Role.CreatedDate;
                IdentityResult roleRuslt = await roleManager.CreateAsync(appRole);

                if (roleRuslt.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}
