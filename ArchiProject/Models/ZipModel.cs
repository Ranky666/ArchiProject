using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiProject.Models
{
    public class ZipModel
    {

        public string Name { get; set; } // имя файла
        public bool? Selected { get; set; } // выбран ли файл для загрузки
    //    public Byte[] FileBytes { get; set; }

    }
}
