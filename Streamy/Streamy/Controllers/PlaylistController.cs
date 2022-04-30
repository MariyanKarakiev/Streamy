using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Services;
using System.Security.Claims;

namespace Streamy.Controllers
{
    public class PlaylistController : BaseController
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                var albums = await _playlistService.GetAll();

                return View(albums);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var playlistModel = new PlaylistModel();

                var songs = await _songService.GetAll();

                ViewData["Songs"] = new SelectList(songs, "Id", "Title");

                return View(playlistModel);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlaylistModel playlistModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (playlistModel.Image != null)
                    {
                        var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, playlistModel.Image);
                        playlistModel.ImageUrl = imageUrl;
                    }

                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    playlistModel.UserId = userId;

                    await _playlistService.CreatePlaylist(playlistModel);
                    return RedirectToAction(nameof(Index));
                }

                var songs = await _songService.GetAll();

                ViewData["Songs"] = new SelectList(songs, "Id", "Title");

                return View(playlistModel);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                var playlistToEdit = await _playlistService.GetByIdForUpdateAsync(id);

                var songs = await _songService.GetAll();

                ViewData["Songs"] = new SelectList(songs, "Id", "Title");

                return View(playlistToEdit);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(PlaylistModel playlistModel)
        {
            try
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
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public async Task<IActionResult> Delete(string? id)
        {
            try
            {
                await _playlistService.DeletePlaylist(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }
    }
}

