﻿using System;
using System.IO;
using System.Threading.Tasks;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Microsoft.AspNetCore.Http;

namespace Streamy.Core.Services
{
    public static class ImageUploadService
    {
        public static async Task<string> UploadImageAsync(Cloudinary cloudinary, IFormFile image)
        {
            byte[] imageBytes;
            string imageUrl;

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                imageBytes = memoryStream.ToArray();


            }

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var uploadParams = new RawUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), memoryStream)
                };

                var result = await cloudinary.UploadAsync(uploadParams);

                imageUrl = result.Url.AbsoluteUri;
            }

            return imageUrl;
        }
    }
}