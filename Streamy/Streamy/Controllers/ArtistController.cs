using Microsoft.AspNetCore.Mvc;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using System.Security.Claims;

namespace Streamy.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            var allArtists = await _artistService.GetAll(userId);

            return View(allArtists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistModel artistModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                artistModel.UserId = userId;

                await _artistService.CreateArtist(artistModel);
                return RedirectToAction(nameof(Index));
            }

            return View(artistModel);

        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistToDetail = await _artistService.GetForDetails(id);

            return View(artistToDetail);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _artistService.DeleteArtist(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artistToEdit = await _artistService.GetByIdForEditAsync(id);

            return View(artistToEdit);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ArtistModel artistViewModel)
        {
            await _artistService.UpdateArtist(artistViewModel);

            return RedirectToAction("Index");
        }
    }
}
