
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Common;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

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
            _roleManager = roleManager;
        }


        public async Task<IActionResult> CreateRole()
        {
            //await _roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = Roles.Administrator
            //}); ;

            return Ok();
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _userService.GetAll();
            return View(roles);
        }
       
        public async Task<IActionResult> Roles(string id)
        {
            var user = await _userService.GetById(id);


            var roles = _roleManager.Roles
                .ToList();

            ViewData["Roles"] = new SelectList(roles, "Name", "Name");

            var model = new UserModel()
            {
                Id = id,
                UserName = user.UserName,
                Email = user.Email,
            };

            return View(model);
        }
      
        [HttpPost]
        public async Task<IActionResult> Roles(UserModel model)
        {
            var user = await _userService.GetById(model.Id);

            var userRoles = await _userManager.GetRolesAsync(user);
            
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (model.Roles?.Length > 0)
            {
                await _userManager.AddToRolesAsync(user, model.Roles);
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
