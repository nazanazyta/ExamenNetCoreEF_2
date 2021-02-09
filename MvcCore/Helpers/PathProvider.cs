using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Helpers
{
    public enum Folders
    {
        Images = 0, Documents = 1, Temporal = 2
    }

    public class PathProvider
    {
        IWebHostEnvironment environment;

        public PathProvider(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        //MÉTODO PARA DEVOLVER LAS RUTAS A FICHEROS
        public String MapPath(String filename, Folders folder)
        {
            //String carpeta = folder.ToString(); //Documents, Images
            String carpeta = "";
            if(folder == Folders.Documents)
            {
                carpeta = "documents";
            }else if(folder == Folders.Images)
            {
                carpeta = "images";
            }else if(folder == Folders.Temporal)
            {
                carpeta = "temporal";
            }
            String path = Path.Combine(this.environment.WebRootPath, carpeta, filename);
            return path;
        }
    }
}
