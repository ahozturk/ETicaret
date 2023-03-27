using ETicaretAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storages
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extention = Path.GetExtension(fileName);
                string name = Path.GetFileNameWithoutExtension(fileName);
                string newFileName = first
                   ? $"{NameOperation.CharacterRegulatory(name)}{extention}"
                   : $"{name}{extention}";

                if (hasFileMethod(pathOrContainerName, newFileName))
                {
                    string[] separated = name.Split("-");
                    if (separated.Length > 1)
                        return await FileRenameAsync(pathOrContainerName, $"{separated[0]}-{int.Parse(separated[^1]) + 1}{extention}", hasFileMethod, false);
                    else
                        return await FileRenameAsync(pathOrContainerName, $"{separated[0]}-2{extention}", hasFileMethod, false);
                }
                else
                    return newFileName;
            });
            return newFileName;
        }
    }
}
