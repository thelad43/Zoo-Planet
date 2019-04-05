namespace ZooPlanet.Web.Controllers
{
    using ZooPlanet.Services;
    using ZooPlanet.Web.Infrastructure.Extensions;
    using ZooPlanet.Web.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IContactService contacts;

        public HomeController(IContactService contacts)
        {
            this.contacts = contacts;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Privacy() => View();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Contacts() => View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Contacts(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            await this.contacts.AddAsync(model.Title, model.Message, model.ContactType, id);

            TempData.AddSuccessMessage("Thank you for the contact message. We will send you email answering your message.");

            return this.RedirectToCustomAction(
                nameof(HomeController.Index),
                nameof(HomeController),
                new { area = string.Empty });
        }
    }
}