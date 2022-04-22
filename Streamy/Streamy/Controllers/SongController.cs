using Microsoft.AspNetCore.Mvc;

namespace Streamy.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
