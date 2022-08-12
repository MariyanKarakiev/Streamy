using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Models;
using System.Diagnostics;

namespace Streamy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        public HomeController(
            ILogger<HomeController> logger,
            ISongService songService,
            IPlaylistService playlistService
            )
        {
            _songService = songService;
            _playlistService = playlistService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var first10Songs = await _songService.GetFirst10Async();

            return View(first10Songs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}