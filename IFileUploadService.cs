using Microsoft.AspNetCore.Http;

namespace Sahred.Application.Services.FileServices
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(string file, Stream stream, string folder);
        Task<string> UploadImages(string fileName, Stream stream, string folder, IFormFile file, int width, int height);
        Task<string> Upload(IFormFile file, string folder);
        Task<bool> CopyImage(string filePath, string rootFolderName, string destinationFolder, string fileName);
        bool IsDirectoryNotExistThenCreate(string rootFolderName, string directoryFolderPath);
        void ClearFolder(string FolderName);
        void DeleteFile(string FileURL);
        void CopyFile(string sourcePath, string targetFileName, string targetFolderName, string sourcBasePath = null);
        string Write64BaseInFolder(string folderInSave, string base64Str, string fileName, string fileExtention);
        byte[] StringToBase64Bytes(string inputString);
        string Base64BytesToString(byte[] inputBase64);
        string ConvertFileToBase64(string filePath);
        void ConvertAndSaveBase64ToFile(string base64String, string outputPath);
    }
}
