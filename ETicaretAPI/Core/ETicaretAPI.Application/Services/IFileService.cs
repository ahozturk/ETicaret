using Microsoft.AspNetCore.Http;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string path, string fileName)>> UploadAsync(string path, IFormFileCollection files);
        Task<bool> CopyFileAsync(string path, IFormFile file);
    }
}
