using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UFF.Domain.Enum;
using UFF.Domain.Services;

namespace UFF.Service
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;
        private readonly string _supabaseKey;
        private readonly string _bucketName;

        public FileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _supabaseUrl = _configuration["Supabase:Url"] ?? throw new ArgumentNullException("Supabase:Url não configurado");
            _supabaseKey = _configuration["Supabase:ApiKey"] ?? throw new ArgumentNullException("Supabase:ApiKey não configurado");
            _bucketName = _configuration["Supabase:Bucket"] ?? throw new ArgumentNullException("Supabase:Bucket não configurado");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _supabaseKey);
        }

        public async Task<string> SaveFileAsync(IFormFile file, FileEnum fileType)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo inválido.");
                        
            var extension = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            var requestUrl = $"{_supabaseUrl}/object/{_bucketName}/{fileType.ToString().ToLower()}/{uniqueFileName}";

            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl)
            {
                Content = new StreamContent(file.OpenReadStream())
            };

            request.Headers.Add("Authorization", $"Bearer {_supabaseKey}");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro no upload: {response.StatusCode} - {content}");
            }

            var publicUrl = $"{_supabaseUrl}/object/public/{_bucketName}/{fileType.ToString().ToLower()}/{uniqueFileName}";
            return publicUrl;
        }
       
        public async Task DeleteFileAsync(string filePath)
        {
            var relativePath = filePath.Replace($"{_supabaseUrl}/storage/v1/object/public/{_bucketName}/", "");
            var response = await _httpClient.DeleteAsync(
                $"{_supabaseUrl}/storage/v1/object/{_bucketName}/{relativePath}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao deletar o arquivo: {response.StatusCode} - {error}");
            }
        }

        public async Task<byte[]> GetFileBytesAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public string CalculateHash(byte[] fileBytes)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(fileBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
