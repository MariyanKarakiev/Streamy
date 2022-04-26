using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Models.Song;

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
            var songs =await _songService.GetAll();
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
            var songModel = new SongCreateModel();

            var genres =await _genreService.GetAllGenres();

            songModel.Genres = genres;
            return View(songModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SongCreateModel songModel)
        {
            if (songModel == null)
            {
                return NotFound();
            }

            //songModel.Artists = new List<ArtistViewModel>()
            //    {
            //        new ArtistViewModel()
            //        {
            //            Id = Guid.NewGuid().ToString(),
            //        },
            //        new ArtistViewModel()
            //        {
            //          Id = Guid.NewGuid().ToString(),
            //        }
            //    };

            await _songService.CreateSong(songModel);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songToEdit = await _songService.GetByIdForCreateAsync(id);

            return View(songToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SongCreateModel songModel)
        {
           await _songService.UpdateSong(songModel);

            return RedirectToAction("Index");
        }
    }
}
