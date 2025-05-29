using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using UFF.Domain.Enum;

namespace UFF.Domain.Services
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, FileEnum fileType);
        Task DeleteFileAsync(string filePath);
    }
}