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
    public class AlbumController : BaseController
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

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var albums = await _albumService.GetAll();

            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var albumModel = new AlbumModel();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var artists = await _artistService.GetAll();
                var songs = await _songService.GetAll(userId);

                ViewData["Artists"] = new SelectList(artists, "Id", "Name");
                ViewData["Songs"] = new SelectList(songs, "Id", "Title");

                return View(albumModel);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumModel albumModel)
        {
            try
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
            catch (Exception ex)
            {
                return View("505");
            }
        }
        public async Task<IActionResult> Edit(string? id)
        {
            try
            {
                var albumToEdit = await _albumService.GetByIdForUpdateAsync(id);

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var artists = await _artistService.GetAll(userId);
                var songs = await _songService.GetAll();

                ViewData["Artists"] = new SelectList(artists, "Id", "Name");
                ViewData["Songs"] = new SelectList(songs, "Id", "Title");

                return View(albumToEdit);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(AlbumModel albumModel)
        {
            try
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
            catch (Exception ex)
            {
                return View("505");
            }
        }
        public async Task<IActionResult> Detail(string? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var songToDetail = await _songService.GetForDetails(id);

                return View(songToDetail);
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
                await _albumService.DeleteAlbum(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }
    }
}

