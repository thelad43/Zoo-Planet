namespace ZooPlanet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AnimalsController : Controller
    {
        public IActionResult Mammals() => View();
    }
}