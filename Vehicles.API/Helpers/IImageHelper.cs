using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Vehicles.API.Helpers
{
    public interface IImageHelper
    {
        Task<Guid> UploadImageAsync(IFormFile imageFile, string folder);

        Guid UploadImageAsync(string path, string folder, string name);

        Task<Guid> UploadImageAsync(byte[] pictureArray, string folder);

        Task<Guid> UploadImageAsync(Stream stream, string folder);

        void DeleteImageAsync(Guid id, string containerName);
    }
}
