using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Entity
{
    public class FileEntity:BaseEntity
    {
        
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
    }
}
