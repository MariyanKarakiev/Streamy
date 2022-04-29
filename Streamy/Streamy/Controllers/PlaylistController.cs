using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using System.Security.Claims;

namespace Streamy.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService _playlistService;
        private readonly ISongService _songService;
        private readonly Cloudinary _cloudinary;

        public PlaylistController(
            IPlaylistService playlistService,
            ISongService songService,
            Cloudinary cloudinary)
        {
            _playlistService = playlistService;
            _songService = songService;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _playlistService.GetAll();

            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            var playlistModel = new PlaylistModel();

            var songs = await _songService.GetAll();

            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(playlistModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaylistModel playlistModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                playlistModel.UserId = userId;

                await _playlistService.CreatePlaylist(playlistModel);
                return RedirectToAction(nameof(Index));
            }

            var songs = await _songService.GetAll();

            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(playlistModel);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            var playlistToEdit = await _playlistService.GetByIdForUpdateAsync(id);

            var songs = await _songService.GetAll();

            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(playlistToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PlaylistModel playlistModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                playlistModel.UserId = userId;

                await _playlistService.UpdatePlaylist(playlistModel);
                return RedirectToAction("Index");
            }

            var songs = await _songService.GetAll();

            ViewData["Songs"] = new SelectList(songs, "Id", "Title");

            return View(playlistModel);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            await _playlistService.DeletePlaylist(id);

            return RedirectToAction(nameof(Index));
        }
    }
}

