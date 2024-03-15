using Microsoft.AspNetCore.Mvc;
using SubirArchivos.Models;
using System.Globalization;

namespace SubirArchivos.Controllers
{
    public class ArchivosController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        List<ArchivoModel> archivos = new List<ArchivoModel>();
        ListFilesModel files = new ListFilesModel();

        public ArchivosController(IWebHostEnvironment webHost)
        {
            _webHostEnvironment = webHost;
        }


        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Upload(ArchivoModel archivo)
        {
            try
            {
                string fileName = archivo.File.FileName;


                if (fileName.Length == 0)
                {
                    return BadRequest();
                }
                else
                {
                    //var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files");
                    var path = Path.Combine(@"C:/", "Files");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fullPath = Path.Combine(path, fileName); 

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        archivo.File.CopyTo(stream);
                    }

                        files.Archivos = new List<ArchivoModel> { new ArchivoModel { Name = fileName, Fecha = archivo.Fecha, File = archivo.File } }; 

                    return View("Index", files.ObternerArchivos());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
