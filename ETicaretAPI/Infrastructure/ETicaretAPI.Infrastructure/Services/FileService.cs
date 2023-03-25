using ETicaretAPI.Infrastructure.Operations;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService 
    {
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
    }
}
