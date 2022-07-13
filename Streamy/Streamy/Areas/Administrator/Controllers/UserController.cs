
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Common;
using System.Linq;

namespace Streamy.Controllers
{
    [Authorize]
    [Area("Administrator")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, IUserService userService, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userService = userService;
            _userManager = userManager;
        }


        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = Roles.Creator
            });

            return Ok();
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService
                .GetAll();


            var userModels = new List<UserModel>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);

                userModels.Add(new UserModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    Roles = roles.ToArray()
                });
            }

            return View(userModels);
        }

        public async Task<IActionResult> SetRole(string id)
        {
            var user = await _userService.GetById(id);


            var roles = _roleManager
                .Roles
                .ToList();

            ViewData["Roles"] = new SelectList(roles, "Name", "Name");

            var userModel = new UserModel()
            {
                Id = id,
                UserName = user.UserName,

            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> SetRole(UserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "No user to set role to.");
            }

            var selectedRoles = model.Roles.ToList();

            if (selectedRoles.Count > 0)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in userRoles)
                {
                    if (!selectedRoles.Contains(role))
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }
                    else
                    {
                        selectedRoles.Remove(role);
                    }
                }

                await _userManager.AddToRolesAsync(user, selectedRoles);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var userToEdit = await _userService.GetById(id);

            return View(userToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(UserModel userModel)
        {
            await _userService.UpdateUser(userModel);

            return RedirectToAction("Index");
        }
    }
}
