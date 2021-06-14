using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using WebApiDemo.Models;
using WebApiDemo.Entity;
using WebApiDemo.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApiDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IDataService<FileEntity> _filecontext;
        private static IWebHostEnvironment _webHostEnvoirnment;
        public FileController(IWebHostEnvironment webHostEnvoirnment,
            IDataService<FileEntity> filecontext)
        {
            _webHostEnvoirnment = webHostEnvoirnment;
            _filecontext = filecontext;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm]FileModel model)
        {
            var entity = new FileEntity();
            var path = Directory.GetCurrentDirectory() + "\\FilesData\\";
            var fileType = Path.GetExtension(model.File.FileName);

            if (model.File.Length <= 0 )
                return Content("file not selected");
           
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
           if(fileType==".pdf")
            {
                entity.Name = model.File.FileName;
                entity.Type = fileType;
                entity.Path = path;
                using (FileStream fileStream = System.IO.File.Create(path + model.File.FileName))
                {
                    await model.File.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
               await  _filecontext.Create(entity);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
            
        }
        [HttpGet("Download")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            if (filename == null)
                return Content("file not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "FilesData", 
                           filename);
            if (!System.IO.File.Exists(path))
                return Content("not found");

                var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf").ToString();
            return File(memory, ext, Path.GetFileName(path));
        }

    }
}
