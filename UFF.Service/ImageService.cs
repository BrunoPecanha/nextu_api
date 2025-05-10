using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

public class ImageService
{
    private readonly IHostEnvironment _environment;

    public ImageService(IHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveImageAndGetPath(IFormFile imageFile, string saveDirectory)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new ArgumentException("Arquivo de imagem inválido");
        }

        var fullDirectoryPath = Path.Combine(_environment.ContentRootPath, saveDirectory);
        if (!Directory.Exists(fullDirectoryPath))
        {
            Directory.CreateDirectory(fullDirectoryPath);
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
        var fullPath = Path.Combine(fullDirectoryPath, fileName);

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return Path.Combine(saveDirectory, fileName);
    }


    public string GetFullPhysicalPath(string relativePath)
    {
        return Path.Combine(_environment.ContentRootPath, relativePath);
    }

    public string GetImageUrl(string relativePath)
    {
        return $"/{relativePath.Replace("\\", "/")}";
    }
}