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
            var songs = _songService.GetAll();
            return View(songs);
        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToDetail = await _songService.GetSongWithDetails((id));

            return View(genreToDetail);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _songService.DeleteSong(id);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
            var testSong = new SongViewModel()
            {
                Title = "Pesen",
                Duration = TimeSpan.Zero,
                ReleaseDate = DateTime.UtcNow,
                GenreId = 1, 
                ArtistList = new List<ArtistViewModel>()
                {
                    new ArtistViewModel()
                    {
                        Id = "c9c485cc-35f5-4541-aa57-4391c614161b",
                    },
                    new ArtistViewModel()
                    {
                        Id = "bccb344f-9aa2-4fcd-bcdb-56572a20d6ee",
                    }
                }
            };


            await _songService.CreateSong(testSong);
            return RedirectToAction(nameof(Index));
        }
    }
}
