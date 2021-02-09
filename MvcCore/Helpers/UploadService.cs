using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public class UploadService
    {
        PathProvider PathProvider;

        public UploadService(PathProvider pathprovider)
        {
            this.PathProvider = pathprovider;
        }

        public async Task<String> UploadFileAsync(IFormFile fichero, Folders folder)
        {
            String filename = fichero.FileName;
            String path = this.PathProvider.MapPath(filename, folder);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            return path;
        }
    }
}
