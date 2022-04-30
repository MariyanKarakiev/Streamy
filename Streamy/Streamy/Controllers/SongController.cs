
using CloudinaryDotNet;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Streamy.Core.Contracts;
using Streamy.Core.Models;
using Streamy.Core.Services;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
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
          //  await StreamAudioAsync(id);
            var genreToDetail = await _songService.GetForDetails(id);

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
                if (songModel.Song != null)
                {
                    var songUrl = await SongUploadService.UploadSongAsync(_cloudinary, songModel.Song);
                    songModel.SongUrl = songUrl;
                }

                if (songModel.Image != null)
                {
                    var imageUrl = await ImageUploadService.UploadImageAsync(_cloudinary, songModel.Image);
                    songModel.ImageUrl = imageUrl;
                }


                songModel.UserId = userId;

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

        //public async Task<IActionResult> StreamAudioAsync(string? id)
        //{
        //    var song = await _songService.GetForDetails(id);

        //    Stream stream = null;

        //    //This controls how many bytes to read at a time and send to the client
        //    int bytesToRead = 10000;

        //    // Buffer to read bytes in chunk size specified above
        //    byte[] buffer = new Byte[bytesToRead];

        //    var ms = new MemoryStream();
            
        //        // The number of bytes read
        //        try
        //        {
        //            //Create a WebRequest to get the file
        //            HttpWebRequest fileReq = (HttpWebRequest)HttpWebRequest.Create(song.SongUrl);

        //            //Create a response for this request
        //            HttpWebResponse fileResp = (HttpWebResponse)fileReq.GetResponse();

        //            if (fileReq.ContentLength > 0)
        //                fileResp.ContentLength = fileReq.ContentLength;


        //            fileReq.GetRequestStream().CopyTo(ms);

        //            // Use the memory stream.

        //            // prepare the response to the client. resp is the client Response
        //            var resp = HttpContext.Response;

        //            //Indicate the type of data being sent
        //            resp.ContentType = "application/octet-stream";

        //            //Name the file 
        //            resp.Headers.Add("Content-Disposition", "attachment; filename=test.zip");
        //            resp.Headers.Add("Content-Length", fileResp.ContentLength.ToString());

        //            int length;
        //            do
        //            {
        //                // Verify that the client is connected.
        //                if (!HttpContext.RequestAborted.IsCancellationRequested)
        //                {
        //                    // Read data into the buffer.
        //                    length = stream.Read(buffer, 0, bytesToRead);

        //                    // and write it out to the response's output stream
        //                    resp.Body.Write(buffer, 0, length);


        //                    //Clear the buffer
        //                    buffer = new Byte[bytesToRead];
        //                }
        //                else
        //                {
        //                    // cancel the download if client has disconnected
        //                    length = -1;
        //                }
        //            } while (length > 0); //Repeat until no data is read
        //        }
        //        finally
        //        {
        //            if (ms != null)
        //            {
        //            //Close the input stream
        //            ms.Close();
        //            }
        //        }

        //        return new FileStreamResult(stream, "audio/mpeg");
        //    }
        }
    }


