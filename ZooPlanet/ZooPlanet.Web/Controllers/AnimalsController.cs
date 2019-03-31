namespace ZooPlanet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Services;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Infrastructure.Filters;
    using ZooPlanet.Web.Models.Animals;

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
        public async Task<IActionResult> Index(int id = 1)
        {
            var animals = await this.animals.All(id);

            var animalsCount = await this.animals.CountAsync();

            var model = new AnimalsListingViewModel
            {
                Animals = animals,
                CurrentPage = id,
                AnimalsCount = animalsCount,
                PagesCount = (int)Math.Ceiling(animalsCount / (decimal)WebConstants.AnimalsPerPage)
            };

            return View(model);
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
        [Log]
        [Authorize(Roles = WebConstants.AdministratorRole + "," + WebConstants.ZooEmployeeRole)]
        public async Task<IActionResult> Add(AnimalFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(User);

            await this.animals
                .Add(model.Name, model.Age, model.ImageUrl, model.AnimalClass, user.Id);
            
            TempData.AddSuccessMessage("Successfully added animal to zoo.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }

        [HttpGet]
        [Authorize(Roles = WebConstants.ZooEmployeeRole + "," + WebConstants.AdministratorRole)]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            var animal = await this.animals.ByUser(id, user);

            var isInAdminRole = await this.userManager.IsInRoleAsync(user, WebConstants.AdministratorRole);

            if (isInAdminRole)
            {
                animal = await this.animals.ById(id);
            }

            if (animal == null)
            {
                return this.AccessDenied();
            }

            var model = new AnimalFormViewModel
            {
                Name = animal.Name,
                Age = animal.Age,
                ImageUrl = animal.ImageUrl,
                AnimalClass = animal.AnimalClass,
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        [Authorize(Roles = WebConstants.ZooEmployeeRole + "," + WebConstants.AdministratorRole)]
        public async Task<IActionResult> Edit(AnimalFormViewModel model)
        {
            var user = await this.userManager.GetUserAsync(User);

            var animal = await this.animals.ByUser(model.Id, user);

            var isInAdminRole = await this.userManager.IsInRoleAsync(user, WebConstants.AdministratorRole);

            if (isInAdminRole)
            {
                animal = await this.animals.ById(model.Id);
            }

            if (animal == null)
            {
                return this.AccessDenied();
            }
            
            await this.animals.Edit(
                animal.Id,
                model.Name,
                model.Age,
                model.AnimalClass,
                model.ImageUrl);

            TempData.AddSuccessMessage($"Successfully edited animal {animal.Name}.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }

        [HttpGet]
        [Authorize(Roles = WebConstants.ZooEmployeeRole + "," + WebConstants.AdministratorRole)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            var animal = await this.animals.ByUser(id, user);

            var isInAdminRole = await this.userManager.IsInRoleAsync(user, WebConstants.AdministratorRole);

            if (isInAdminRole)
            {
                animal = await this.animals.ById(id);
            }

            if (animal == null)
            {
                return this.AccessDenied();
            }

            var model = new DeleteAnimalViewModel
            {
                Id = animal.Id,
                Name = animal.Name
            };

            return View(model);
        }

        [HttpPost]
        [Log]
        [Authorize(Roles = WebConstants.ZooEmployeeRole + "," + WebConstants.AdministratorRole)]
        public async Task<IActionResult> Destroy(int id)
        {
            var user = await this.userManager.GetUserAsync(User);

            var animal = await this.animals.ByUser(id, user);

            var isInAdminRole = await this.userManager.IsInRoleAsync(user, WebConstants.AdministratorRole);

            if (isInAdminRole)
            {
                animal = await this.animals.ById(id);
            }

            if (animal == null)
            {
                return this.AccessDenied();
            }

            await this.animals.Delete(id);

            TempData.AddSuccessMessage("Successfully deleted an animal.");

            return this.RedirectToCustomAction(
               nameof(HomeController.Index),
               nameof(HomeController),
               new { area = string.Empty });
        }
    }
}