using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamy.Common;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using System.Security.Claims;

namespace Streamy.Controllers
{
    [Authorize(Roles = RolesConstants.Roles.Administrator)]
    public class ArtistController : BaseController
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var allArtists = await _artistService.GetAll();

                return View(allArtists);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistModel artistModel)
        {
            try
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

                var artistToDetail = await _artistService.GetForDetails(id);

                return View(artistToDetail);
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
                if (id == null)
                {
                    return NotFound();
                }

                await _artistService.DeleteArtist(id);

                return RedirectToAction("Index");
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
                if (id == null)
                {
                    return NotFound();
                }

                var artistToEdit = await _artistService.GetByIdForUpdateAsync(id);

                return View(artistToEdit);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(ArtistModel artistViewModel)
        {
            try
            {
                if (artistViewModel == null)
                {
                    return NotFound();
                }
                await _artistService.UpdateArtist(artistViewModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }
    }
}
