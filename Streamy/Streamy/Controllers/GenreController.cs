using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Models;
using Streamy.Core.Services;

namespace Streamy.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreModel genreModel)
        {
            if (ModelState.IsValid)
            {
                await _genreService.CreateGenre(genreModel);
                return RedirectToAction(nameof(Index));
            }

            return View(genreModel);

        }
    }
}
