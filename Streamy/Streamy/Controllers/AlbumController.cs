using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Services;
using System.Security.Claims;

namespace Streamy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;
        private readonly Cloudinary _cloudinary;


        public AlbumController(IAlbumService albumService, IArtistService artistService, ISongService songService, Cloudinary cloudinary)
        {
            _albumService = albumService;
            _artistService = artistService;
            _songService = songService;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAll();

            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            var albumModel = new AlbumModel();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll(userId);

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(albumModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumModel albumModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            if (ModelState.IsValid)
            {
                if (albumModel.Image != null)
                {

                    albumModel.UserId = userId;

                    var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, albumModel.Image);
                    albumModel.ImageUrl = imageUrl;
                }
                await _albumService.CreateAlbum(albumModel);
                return RedirectToAction(nameof(Index));
            }

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll(userId);

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(albumModel);
        }
        public async Task<IActionResult> Edit(string? id)
        {

            var albumToEdit = await _albumService.GetByIdForUpdateAsync(id);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var artists = await _artistService.GetAll(userId);
            var songs = await _songService.GetAll();

            ViewData["Artists"] = new SelectList(artists, "Id", "Name");
            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(albumToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AlbumModel albumModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                if (albumModel.Image != null)
                {
                    albumModel.UserId = userId;

                    var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, albumModel.Image); 
                    albumModel.ImageUrl = imageUrl;
                }
                await _albumService.UpdateAlbum(albumModel);
                return RedirectToAction(nameof(Index));
            }

            var artists = await _artistService.GetAll();
            var songs = await _songService.GetAll(userId);

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
