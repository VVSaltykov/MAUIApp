using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using File = MAUIAppCommon.Models.File;

namespace MAUI_API.Repositories
{
    public class FileRepository
    {
        private readonly ApplicationContext applicationContext;
        IWebHostEnvironment _appEnvironment;
        private static Random random = new Random();

        public FileRepository(ApplicationContext applicationContext, IWebHostEnvironment appEnvironment)
        {
            this.applicationContext = applicationContext;
            _appEnvironment = appEnvironment;
        }

        public async Task AddFileAsync(File file)
        {
            applicationContext.Files.Add(file);
            await applicationContext.SaveChangesAsync();
        }

        public async Task<File?> GetFileAsync(string path)
        {
            File file = await applicationContext.Files.FirstOrDefaultAsync(f => f.Path == path);
            return file;
        }
        public async Task<bool> FileInDataBase(string path)
        {
            File file = await applicationContext.Files.FirstOrDefaultAsync(f => f.Path == path);
            return true;
        }
        public async Task Upload(IFormFile uploadFile)
        {
            var type = GetContentType(uploadFile.FileName);
            var key = GetContentKey(uploadFile.FileName);
            if (Regex.IsMatch(type, @"image(\w*)"))
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[16];
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                finalString = string.Concat(finalString, key);

                if (!await FileInDataBase(finalString))
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    finalString = new String(stringChars);
                    finalString = string.Concat(finalString, key);
                }
                string path = "/Files/" + finalString;

                var image = Image.FromStream(uploadFile.OpenReadStream());
                if (image.Width > 1920 && image.Height > 1080)
                {
                    var imageBytes = await ResizeImage(image);
                    using (var stream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        stream.Write(imageBytes, 0, imageBytes.Length);
                    }
                }
                else
                {
                    using (var stream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        await uploadFile.CopyToAsync(stream);
                    }
                }

                File file = new File
                {
                    FileName = uploadFile.FileName,
                    Path = path
                };
                await AddFileAsync(file);
            }
            else
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var stringChars = new char[16];
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var finalString = new String(stringChars);
                finalString = string.Concat(finalString, key);

                if (!await FileInDataBase(finalString))
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[random.Next(chars.Length)];
                    }

                    finalString = new String(stringChars);
                    finalString = string.Concat(finalString, key);
                }
                string path = "/Files/" + finalString;

                using (var stream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                {
                    await uploadFile.CopyToAsync(stream);
                }

                File file = new File
                {
                    FileName = uploadFile.FileName,
                    Path = path
                };
                await AddFileAsync(file);
            }
        }
        public async Task<byte[]> ResizeImage(Image image)
        {
            var resized = new Bitmap(image, new Size(1920, 1080));
            using var imageStream = new MemoryStream();
            resized.Save(imageStream, ImageFormat.Jpeg);
            var imageBytes = imageStream.ToArray();
            return imageBytes;
        }
        // Get content type
        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        public string GetContentKey(string path)
        {
            var types = GetMimeTypes();
            var myKey = types.FirstOrDefault(x => x.Key == Path.GetExtension(path).ToLowerInvariant()).Key;
            return myKey;
        }

        // Get mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
