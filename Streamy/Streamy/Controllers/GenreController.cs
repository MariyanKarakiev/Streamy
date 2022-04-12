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
            var allGenres = _genreService.GetAllGenres();

            return View(allGenres);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GenreViewModel genreModel)
        {
            if (ModelState.IsValid)
            {
                await _genreService.CreateGenre(genreModel);
                return RedirectToAction(nameof(Index));
            }

            return View(genreModel);

        }

        public async Task<IActionResult> Detail(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToDetail = await _genreService.GetGenreWithDetails((short)id);

            return View(genreToDetail);
        }

        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _genreService.DeleteGenre((short)id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToEdit = await _genreService.GetByIdAsync((short)id);

            return View(genreToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(GenreViewModel genreViewModel)
        {
            _genreService.UpdateGenre(genreViewModel);

            return RedirectToAction("Index");
        }
    }
}
