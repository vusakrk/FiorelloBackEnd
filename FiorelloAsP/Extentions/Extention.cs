using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Extentions
{
    public static class Extention
    {
        public static bool IsImage (this IFormFile file,string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool MaxLength(this IFormFile file,int kb)
        {
            return file.Length / 1024 <= kb;
        }
        public static async Task<string> SaveFile(this IFormFile file,string root,string folder)
        {
            try
            {
                string path = root;
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                string resultPath = Path.Combine(path, folder, fileName);
                using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileName;
            }
            catch (Exception)
            {

                return null;
            }

            
        }
    }
}
