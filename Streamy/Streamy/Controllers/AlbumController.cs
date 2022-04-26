using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Core.Models;

namespace Streamy.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        private readonly IArtistService _artistService;

        public AlbumController(IAlbumService albumService, IArtistService artistService)
        {
            _albumService = albumService;
            _artistService = artistService;
        }

        public async Task<IActionResult> Index()
        {
            var albums =await  _albumService.GetAll();

            return View(albums);
        }

        public async Task<IActionResult> Create()
        {
            var albumModel = new AlbumCreateModel();

            var artists = await _artistService.GetAll();

            albumModel.Artists = artists;
            return View(albumModel);
        }
    }
}
