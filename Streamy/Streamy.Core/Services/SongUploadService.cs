using System;
using System.IO;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;

namespace Streamy.Core.Services
{
    public static class SongUploadService
    {
        public static async Task<string> UploadSongAsync(Cloudinary cloudinary, IFormFile song)
        {
            byte[] songBytes;
            string songUrl;

            using (var memoryStream = new MemoryStream())
            {
                await song.CopyToAsync(memoryStream);
                songBytes = memoryStream.ToArray();

            }

            using (var memoryStream = new MemoryStream(songBytes))
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), memoryStream)
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                songUrl = result.Url.AbsoluteUri;
            }

            return songUrl;
        }
    }
}