using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streamy.Common;
using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Services;
using System.Security.Claims;

namespace Streamy.Controllers
{

    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;
        private readonly Cloudinary _cloudinary;

        public GenreController(IGenreService genreService, Cloudinary cloudinary)
        {
            _genreService = genreService;
            _cloudinary = cloudinary;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var allGenres = await _genreService.GetAll();

                return View(allGenres);
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
        public async Task<IActionResult> Create(GenreModel genreModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    genreModel.UserId = userId;

                    await _genreService.CreateGenre(genreModel);
                    return RedirectToAction(nameof(Index));
                }

                return View(genreModel);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public async Task<IActionResult> Detail(short? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var genreToDetail = await _genreService.GetForDetails((short)id);

                return View(genreToDetail);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public async Task<IActionResult> Delete(short? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                await _genreService.DeleteGenre((short)id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        public async Task<IActionResult> Edit(short? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var genreToEdit = await _genreService.GetByIdForUpdateAsync((short)id);

                return View(genreToEdit);
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(GenreModel genreViewModel)
        {
            try
            {
                await _genreService.UpdateGenre(genreViewModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("505");
            }
        }
    }
}
