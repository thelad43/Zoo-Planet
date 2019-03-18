namespace ZooPlanet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AnimalsController : Controller
    {
        [HttpGet]
        public IActionResult Mammals() => View();

        [HttpGet]
        public IActionResult Reptiles() => View();
    }
}