
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Models.Song;
using Streamy.Core.Services;
using System.Security.Claims;

namespace Streamy.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService _songService;
        private readonly IGenreService _genreService;
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly Cloudinary _cloudinary;


        public SongController(
            ISongService songService,
            IGenreService genreService,
            IArtistService artistService,
            IAlbumService albumService,
            Cloudinary cloudinary)
        {
            _songService = songService;
            _genreService = genreService;
            _artistService = artistService;
            _albumService = albumService;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await _songService.GetAll();

            return View(songs);
        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genreToDetail = await _songService.GetForDetails((id));

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
            var songModel = new SongModel();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await _genreService.GetAll();
            var artists = await _artistService.GetAll();
            var albums = await _albumService.GetAll(userId);

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Albums"] = new SelectList(albums, "Id", "Title");
            ViewData["Genres"] = new SelectList(genres, "Id", "Name");

            return View(songModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SongModel songModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                if (songModel.Image != null)
                {

                    songModel.UserId = userId;

                    var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, songModel.Image);
                    songModel.ImageUrl = imageUrl;
                }
                await _songService.CreateSong(songModel);
                return RedirectToAction(nameof(Index));
            }


            var genres = await _genreService.GetAll();
            var artists = await _artistService.GetAll();
            var albums = await _albumService.GetAll(userId);

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Albums"] = new SelectList(albums, "Id", "Title");
            ViewData["Genres"] = new SelectList(genres, "Id", "Name");

            return View(songModel);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var genres = await _genreService.GetAll();
            var artists = await _artistService.GetAll(userId);
            var albums = await _albumService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Albums"] = new SelectList(albums, "Id", "Title");
            ViewData["Genres"] = new SelectList(genres, "Id", "Name");

            var songToEdit = await _songService.GetByIdForUpdateAsync(id);

            return View(songToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SongModel songModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                if (songModel.Image != null)
                {

                    songModel.UserId = userId;

                    var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, songModel.Image);
                    songModel.ImageUrl = imageUrl;
                }

                await _songService.UpdateSong(songModel);
                return RedirectToAction("Index");
            }

            var genres = await _genreService.GetAll();
            var artists = await _artistService.GetAll();
            var albums = await _albumService.GetAll(userId);

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Albums"] = new SelectList(albums, "Id", "Title");
            ViewData["Genres"] = new SelectList(genres, "Id", "Name");

            return View(songModel);
        }
    }
}
