namespace ZooPlanet.Web.Controllers
{
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Services;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Models.Animals;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AnimalsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAnimalService animals;

        public AnimalsController(UserManager<User> userManager, IAnimalService animals)
        {
            this.userManager = userManager;
            this.animals = animals;
        }

        [HttpGet]
        public IActionResult Mammals() => View();

        [HttpGet]
        public IActionResult Reptiles() => View();

        [HttpGet]
        public IActionResult Birds() => View();

        [HttpGet]
        public IActionResult Amphibians() => View();

        [HttpGet]
        public IActionResult Arthropods() => View();

        [HttpGet]
        public IActionResult Fish() => View();

        [HttpGet]
        [Authorize(Roles = WebConstants.AdministratorRole + "," + WebConstants.ZooEmployeeRole)]
        public IActionResult Add() => View();

        [HttpPost]
        [Authorize(Roles = WebConstants.AdministratorRole + "," + WebConstants.ZooEmployeeRole)]
        public async Task<IActionResult> Add(AddAnimalFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.animals
                .Add(model.Name, model.Age, model.ImageUrl, model.AnimalClass, user.Id);

            // TODO: Log

            TempData.AddSuccessMessage("Successfully added animal to zoo.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }
    }
}