using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Vehicles.API.Helpers
{
    public class ImageHelper : IImageHelper
    {
        public async Task<Guid> UploadImageAsync(IFormFile imageFile, string folder)
        {
            Guid name = Guid.NewGuid();
            string file = $"{name}.jpg";
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                $"wwwroot\\images\\{folder}",
                file);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            //return $"~/images/{folder}/{file}";
            return name;
        }

        public Task<Guid> UploadImageAsync(byte[] pictureArray, string folder)
        {
            MemoryStream stream = new MemoryStream(pictureArray);
            Guid imageId = Guid.NewGuid();
            string file = $"{imageId.ToString()}.jpg";

            try
            {
                stream.Position = 0;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                throw;
            }

            return Task.FromResult(imageId);
        }

        public Guid UploadImageAsync(string path, string folder, string imageName)
        {
            Guid imageId = Guid.NewGuid();
            byte[] data;
            using (WebClient webclient = new WebClient())
            {
                data = webclient.DownloadData(path + imageName + ".png");
                var ms = new MemoryStream(data);
                Image img = Image.FromStream(ms);
                img.Save(path + folder + "/" + imageId.ToString() + ".jpg", ImageFormat.Jpeg);
            }

            return imageId;
        }

        public Task<Guid> UploadImageAsync(Stream stream, string folder)
        {
            Guid imageId = Guid.NewGuid();
            string file = $"{imageId.ToString()}.jpg";
            stream.Position = 0;
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{folder}", file);

            try
            {
                Image img = Image.FromStream(stream);
                img.Save(path, ImageFormat.Jpeg);
            }
            catch
            {
                throw;
            }

            return Task.FromResult(imageId);
        }

        public void DeleteImageAsync(Guid id, string containerName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{containerName}", id.ToString()+".jpg");
            FileInfo fi1 = new FileInfo(path);
            try
            {
                fi1.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
