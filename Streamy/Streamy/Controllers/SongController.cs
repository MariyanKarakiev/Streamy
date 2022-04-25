using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

namespace Streamy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _songService;
        private readonly IGenreService _genreService;

        public SongController(ISongService songService, IGenreService genreService)
        {
            _songService = songService;
           _genreService = genreService;
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

        public IActionResult Create()
        {
            var songModel = new SongViewModel();

            var genres = _genreService.GetAllGenres();

            songModel.GenreListViewModel = genres;
            return View(songModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SongViewModel songModel)
        {
            if (songModel == null)
            {
                return NotFound();
            }

            songModel.ArtistList = new List<ArtistViewModel>()
                {
                    new ArtistViewModel()
                    {
                        Id = Guid.NewGuid().ToString(),
                    },
                    new ArtistViewModel()
                    {
                      Id = Guid.NewGuid().ToString(),
                    }
                };

            await _songService.CreateSong(songModel);
            return RedirectToAction(nameof(Index));
        }
    }
}
