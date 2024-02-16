using IdentityFrameworkTask.DataModel;
using IdentityFrameworkTask.ViewModels;
using IdentityFrameworkTask;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Rewrite;

namespace IdentityFrameworkTask.Views.Users
{
    public class UserController : Controller
    {
        private readonly appDbContext context;
        //private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;

        public UserController(appDbContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public IActionResult Register()
        {
            UserVM model = new UserVM();
            model.AppRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString()

            }).ToList();
            return View("Register", model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserVM vm)
        {
            if (ModelState.IsValid)
            {

                var model = new AppUser()
                {
                    FirstName = vm.User.FirstName,
                    LastName = vm.User.LastName,
                    Email = vm.User.Email,
                    PhoneNumber = vm.User.PhoneNumber,
                    Address = vm.User.Address,
                    UserName = vm.User.UserName,

                };

                IdentityResult result = await userManager.CreateAsync(model, vm.User.PasswordHash);
                if(result.Succeeded)
                {
                    AppRole appRole = await roleManager.FindByIdAsync(vm.AppRoleId);

                    if(appRole != null)
                    {

                        IdentityResult roleResult = await userManager.AddToRoleAsync(model, appRole.Name);

                        if(appRole.Name == "Admin")
                        {
                            await userManager.AddClaimAsync(model, new Claim("Admin", "True"));
                        }
                        else if(appRole.Name == "User")
                        {
                            await userManager.AddClaimAsync(model, new Claim("User", "True"));
                        }
                        else { await userManager.AddClaimAsync(model, new Claim("Guest", "True")); }

                        if (roleResult.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        await userManager.AddClaimAsync(model, new Claim("Guest", "True"));
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(vm);
        }

        public IActionResult Index()
        {
            var model = context.Users.ToList();
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            UserVM result = new UserVM();
            result.AppRoles = roleManager.Roles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id

            }).ToList();

            var model = context.AppUsers.SingleOrDefault(u => u.Id == id);
            AppUser roles = await userManager.FindByIdAsync(id);

            //var result = new UserVM();
            result.User = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                UserName = model.UserName,

            };

            var userRole = userManager.GetRolesAsync(roles).Result.FirstOrDefault();

            var selectedRole = result.AppRoles.FirstOrDefault(r => r.Text.Equals(userRole));
            if (selectedRole != null)
            {
                selectedRole.Selected = true;
            }

            return View("Edit", result);
            }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserVM model) 
        {
            if(ModelState.IsValid)
            {
                AppUser user = await userManager.FindByIdAsync(id);
                model.AppRoles = roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id

                }).ToList();

                if (user != null)
                {
                    user.FirstName = model.User.FirstName;
                    user.LastName = model.User.LastName;
                    user.Email = model.User.Email;
                    user.PhoneNumber = model.User.PhoneNumber;
                    user.Address = model.User.Address;
                    user.UserName = model.User.UserName;

                    if (!string.IsNullOrEmpty(model.OldPassword))
                    {
                        var changePass = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                        if(changePass.Succeeded)
                        {
                            string existingRole = userManager.GetRolesAsync(user).Result.Single();
                            string existingRoleId = roleManager.Roles.Single(r => r.Name == existingRole).Id;
                            IdentityResult result = await userManager.UpdateAsync(user);

                            if (result.Succeeded)
                            {
                                if (existingRoleId != model.AppRoleId)
                                {
                                    IdentityResult roleResult = await userManager.RemoveFromRoleAsync(user, existingRole);
                                    if (roleResult.Succeeded)
                                    {
                                        AppRole appRole = await roleManager.FindByIdAsync(model.AppRoleId);
                                        if (appRole != null)
                                        {
                                            if (appRole.Name == "Admin")
                                            {
                                                await userManager.AddClaimAsync(user, new Claim("Admin", "True"));
                                            }
                                            else if (appRole.Name == "User")
                                            {
                                                await userManager.AddClaimAsync(user, new Claim("User", "True"));
                                            }
                                            else { await userManager.AddClaimAsync(user, new Claim("Guest", "True")); }

                                            IdentityResult newRoleResult = await userManager.AddToRoleAsync(user, appRole.Name);
                                            if (newRoleResult.Succeeded)
                                            {
                                                return RedirectToAction("Index");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Password is incorrect.");
                            return View(model);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            
            var user = context.AppUsers.FirstOrDefault(x => x.Id == id);
            var roles = await userManager.GetRolesAsync(user);
            string role =  (await userManager.GetRolesAsync(user)).Single();

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
}
