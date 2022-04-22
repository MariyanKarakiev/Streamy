using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

namespace Streamy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService;
        }

        public async Task<IActionResult> Index()
        {
            var songs = _songService.GetAllAsync();
            return View(songs);
        }

        public async Task<IActionResult> Detail(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToDetail = await _songService.GetSongWithDetails((Guid)id);

            return View(genreToDetail);
        }
        public async Task<IActionResult> Create()
        {
            var testSong = new SongViewModel()
            {
                Title = "Pesen",
                Duration = TimeSpan.Zero,
                ReleaseDate = DateTime.UtcNow,
                GenreId = 1,
            };


            await _songService.CreateSong(testSong);
            return RedirectToAction(nameof(Index));
        }
    }
}
