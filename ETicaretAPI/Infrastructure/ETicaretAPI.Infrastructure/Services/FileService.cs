using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
             {
                 string extention = Path.GetExtension(fileName);
                 string name = Path.GetFileNameWithoutExtension(fileName);
                 string newFileName = first
                    ? $"{NameOperation.CharacterRegulatory(name)}{extention}"
                    : $"{name}{extention}";

                 if (File.Exists($"{path}\\{newFileName}"))
                 {
                     string[] separated = name.Split("-");
                     if (separated.Length > 1)
                         return await FileRenameAsync(path, $"{separated[0]}-{int.Parse(separated[^1])+1}{extention}", false);
                     else
                         return await FileRenameAsync(path, $"{separated[0]}-2{extention}", false);
                 }
                 else
                     return newFileName;
             });
            return newFileName;
        }

        public async Task<List<(string path, string fileName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<bool> results = new();
            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);
                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add(($"{path}\\{fileNewName}", fileNewName));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
            //todo custom exceptions

        }
    }
}
