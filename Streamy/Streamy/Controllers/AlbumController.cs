using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

namespace Streamy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;

        public AlbumController(IAlbumService albumService, IArtistService artistService, ISongService songService)
        {
            _albumService = albumService;
            _artistService = artistService;
            _songService = songService;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAll();

            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            var albumModel = new AlbumModel();

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");


            // var songs = await _songService.GetAll();

            return View(albumModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumModel albumModel)
        {
            if (ModelState.IsValid)
            {
                await _albumService.CreateAlbum(albumModel);
                return RedirectToAction(nameof(Index));
            }

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(albumModel);
        }
        public async Task<IActionResult> Edit(string? id)
        {

            var genreToEdit = await _albumService.GetByIdForUpdateAsync(id);

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(genreToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AlbumModel albumModel)
        {
            if (ModelState.IsValid)
            {
                await _albumService.UpdateAlbum(albumModel);
                return RedirectToAction("Index");
            }
            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(albumModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            await _albumService.DeleteAlbum(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
