using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class FileModel
    {
        public IFormFile File { get; set; }
    }
}
