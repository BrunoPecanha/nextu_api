using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UFF.Domain.Enum;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly string _basePath;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _basePath = _configuration["FileDirectory:Profile"]
                ?? throw new ArgumentNullException("FileDirectory:Profile não configurado");

            _basePath = _basePath.Replace("\\uploads\\profile-images", "");
        }

        public async Task<string> SaveFileAsync(IFormFile file, FileEnum fileType)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");

            var folderPath = GetFolderPath(fileType);
            Directory.CreateDirectory(folderPath);

            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var absolutePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
                        
            var relativePath = GetRelativePathWithAssets(absolutePath);

            return relativePath;
        }

        public async Task DeleteFileAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                await Task.CompletedTask;
            }
        }

        private string GetRelativePathWithAssets(string absolutePath)
        {            
            int uploadsIndex = absolutePath.IndexOf("uploads\\", StringComparison.OrdinalIgnoreCase);

            if (uploadsIndex == -1)
            {                
                return Path.GetRelativePath(_basePath, absolutePath).Replace("\\", "/");
            }
                        
            string uploadsPath = absolutePath.Substring(uploadsIndex).Replace("\\", "/");
            return $"assets/images/{uploadsPath}";
        }       

        private string GetFolderPath(FileEnum fileType)
        {
            var fieldInfo = fileType.GetType().GetField(fileType.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttributes(
                typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            var description = descriptionAttribute?.FirstOrDefault()?.Description ?? fileType.ToString();

            var path = _configuration[$"FileDirectory:{description}"]
                ?? throw new ArgumentException($"Caminho não configurado para {fileType}");

            return path;
        }
    }
}