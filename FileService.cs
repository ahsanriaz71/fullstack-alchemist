using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Text;

namespace Sahred.Application.Services.FileServices
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileUploadService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }
        public async Task<string> UploadFile(string file, Stream stream, string folder)
        {


            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            var exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);

            }
            var fileName = GetFileName(file);
            var filePath = Path.Combine(uploads, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }
            string savePathToDatabase = Path.Combine(folder, fileName);
            return savePathToDatabase;//filePath
        }
        public async Task<string> Upload(IFormFile file, string folder)
        {

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folder);
            var exists = Directory.Exists(uploads);
            if (!exists)
            {
                Directory.CreateDirectory(uploads);
            }
            var filePath = await CopyFile(file, folder, uploads);
            return filePath;
        }
        public async Task<string> UploadImages(string fileName, Stream stream, string folder, IFormFile file, int width, int height)
        {
            try
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, folder);
                var exists = Directory.Exists(uploads);
                if (!exists)
                {
                    Directory.CreateDirectory(uploads);

                }
                var fileNameNew = GetImageName(fileName);
                var filePath = Path.Combine(uploads, fileNameNew);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    //var img = await file.ResizeImage(width, height);
                    //img.Save(stream, ImageFormat.Jpeg);//SomeFormat
                    await stream.CopyToAsync(fileStream);
                }
                string savePathToDatabase = Path.Combine(folder, fileNameNew);
                return savePathToDatabase;//filePath
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        private async Task<string> CopyFile(IFormFile file, string folder, string uploads)
        {
            var fileName = GetFileName(file);
            var filePath = Path.Combine(uploads, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return "/" + folder + "/" + fileName;
        }
        private string GetFileName(IFormFile file)
        {
            return DateTime.Now.Ticks + "_" + file.FileName.Replace(" ", "_");
        }
        public string GetFileName(string file)
        {
            return file + DateTime.Now.Ticks + ".pdf";
        }
        public string GetImageName(string fileName)
        {
            return DateTime.Now.Ticks + "_" + fileName;
        }

        public async Task<bool> CopyImage(string filePath, string rootFolderName, string destinationFolder, string fileName)
        {
            try
            {
                var fileFolder = Path.Combine(_hostingEnvironment.WebRootPath, filePath);
                if (System.IO.File.Exists(fileFolder))
                {
                    var fileFolderUpload = Path.Combine(_hostingEnvironment.WebRootPath, rootFolderName, destinationFolder);
                    var exists = Directory.Exists(fileFolderUpload);
                    if (!exists)
                    {
                        Directory.CreateDirectory(fileFolderUpload);

                    }
                    var fileFolderUploadWithFile = Path.Combine(fileFolderUpload, fileName);
                    byte[] bytes = System.IO.File.ReadAllBytes(fileFolder);
                    Stream stream = new MemoryStream(bytes);
                    //await UploadImages(fileName, stream, fileFolderUploadWithFile);
                    using (FileStream fileStream = new FileStream(fileFolderUploadWithFile, FileMode.Create, FileAccess.Write))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        await stream.CopyToAsync(fileStream);
                    }
                    // System.IO.File.Move(fileFolder, fileFolderUploadWithFile);
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return false;
        }

        public bool IsDirectoryNotExistThenCreate(string rootFolderName, string directoryFolderPath)
        {
            try
            {
                var directoryFolder = Path.Combine(_hostingEnvironment.WebRootPath, rootFolderName, directoryFolderPath);
                if (!System.IO.Directory.Exists(directoryFolder))
                {
                    System.IO.Directory.CreateDirectory(directoryFolder);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void ClearFolder(string FolderName)
        {
            var directoryFolder = Path.Combine(_hostingEnvironment.WebRootPath, FolderName);
            if (System.IO.Directory.Exists(directoryFolder))
            {
                DirectoryInfo dir = new DirectoryInfo(directoryFolder);
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
            }

        }

        public void DeleteFile(string FileURL)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, FileURL);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        public string Write64BaseInFolder(string folderInSave, string base64Str, string fileName, string fileExtention)
        {
            try
            {
                string returnFilePath = string.Empty;
                var random = DateTime.Now.ToString("ddMMyyyyHHmmss");
                if (fileName.Length > 100)
                    fileName = fileName.Substring(0, 100);
                string newFileName = fileName.Replace(' ', '_').Replace('/', '_') + "_" + random + fileExtention;
                var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, folderInSave);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                byte[] fileBytes = Convert.FromBase64String(base64Str);
                File.WriteAllBytes(Path.Combine(dirPath, newFileName), fileBytes);
                return newFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CopyFile(string sourcePath, string targetFileName, string targetFolderName, string sourcBasePath = null)
        {
            var dirPath = Path.Combine(_hostingEnvironment.WebRootPath, targetFolderName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var sourceFullPath = string.IsNullOrEmpty(sourcBasePath) ? Path.Combine(_hostingEnvironment.WebRootPath, sourcePath) : Path.Combine(sourcBasePath, sourcePath);
            if (File.Exists(sourceFullPath))
            {
                var targetFullPath = Path.Combine(dirPath, targetFileName);
                System.IO.File.Copy(sourceFullPath, targetFullPath, true);
            }

        }
        public byte[] StringToBase64Bytes(string inputString)
        {
            byte[] base64Bytes = new byte[64];
            if (inputString != null)
            {
                base64Bytes = Convert.FromBase64String(inputString);
            }
            return base64Bytes;
        }

        public string Base64BytesToString(byte[] inputBase64)
        {
            string base64String = Convert.ToBase64String(inputBase64, 0, inputBase64.Length);
            return base64String;
        }
        public string ConvertFileToBase64(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(fileBytes);
            return "base64," + base64String;
        }
        public void ConvertAndSaveBase64ToFile(string base64String, string outputPath)
        {
            byte[] fileBytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(outputPath, fileBytes);
        }

    }
}
