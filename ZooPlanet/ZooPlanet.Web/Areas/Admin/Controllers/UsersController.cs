namespace ZooPlanet.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Services.Admin;
    using ZooPlanet.Web.Areas.Admin.Models.Users;
    using ZooPlanet.Web.Controllers;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Infrastructure.Filters;

    public class UsersController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IUserAdminService users;

        public UsersController(
            UserManager<User> userManager,
            IUserAdminService users)
        {
            this.userManager = userManager;
            this.users = users;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult AddUserToRole() => View();

        [HttpPost]
        [Log]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel model)
        {
            var user = await this.users.GetUserByNameAsync(model.UserName);

            if (user == null)
            {
                TempData.AddErrorMessage($"User {user.UserName} could not be found.");
                return NotFound();
            }
            
            await this.userManager.AddToRoleAsync(user, WebConstants.ZooEmployeeRole);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to role '{WebConstants.ZooEmployeeRole}'.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }

        [HttpGet]
        public IActionResult RemoveUserFromRole() => View();

        [HttpPost]
        [Log]
        public async Task<IActionResult> RemoveUserFromRole(UserRoleViewModel model)
        {
            var user = await this.users.GetUserByNameAsync(model.UserName);

            if (user == null)
            {
                TempData.AddErrorMessage($"User {user.UserName} could not be found.");
                return NotFound();
            }
            
            await this.userManager.RemoveFromRoleAsync(user, WebConstants.ZooEmployeeRole);

            TempData.AddSuccessMessage($"User {user.UserName} successfully removed from role '{WebConstants.ZooEmployeeRole}'.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }
    }
}