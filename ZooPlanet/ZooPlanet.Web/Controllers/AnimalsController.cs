namespace ZooPlanet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AnimalsController : Controller
    {
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
    }
}