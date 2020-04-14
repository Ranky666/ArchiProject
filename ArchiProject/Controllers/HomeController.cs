using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using ArchiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArchiProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        

        [AllowAnonymous]
        public IActionResult Index()
        {


            List<string> files = Directory.GetFiles("D:/FilesforArchiving").Select(Name => Path.GetFileName(Name)).ToList();

          




            return View(files);



        }


    




        //[HttpPost]
        //public ActionResult Archi(List<ZipModel> files)
        //{
        //    // получаем выбранные файлы
        //    List<string> filenames = files.Where(m => m.Selected == true).Select(f => f.Name).ToList();

        //    // создаем папку Temp для хранения архива
        //    if (!Directory.Exists("~D/FilesforArchiving/Comprest"))
        //        Directory.CreateDirectory("~D/FilesforArchiving/Comprest");

        //    // создаем имя для архива
        //    string filename = Guid.NewGuid().ToString() + ".zip";
        //    string fullZipPath = ("~D/FilesforArchiving/Comprest" + filename);
        //    // определяем потоки для создания архива
        //    FileStream fsOut = System.IO.File.Create(fullZipPath);
        //    ZipOutputStream zipStream = new ZipOutputStream(fsOut);

        //    zipStream.SetLevel(3); // уровень сжатия от 0 до 9

        //    // перебираем выбранные файлы и добавляем в архив
        //    foreach (string file in filenames)
        //    {
        //        FileInfo fi = new FileInfo("~D/FilesforArchiving/" + file);

        //        if (!fi.Exists)
        //            continue;

        //        string entryName = ZipEntry.CleanName(fi.Name);
        //        ZipEntry newEntry = new ZipEntry(entryName);
        //        newEntry.DateTime = fi.LastWriteTime;
        //        newEntry.Size = fi.Length;
        //        zipStream.PutNextEntry(newEntry);

        //        byte[] buffer = new byte[4096];
        //        using (FileStream streamReader = System.IO.File.OpenRead(fi.FullName))
        //        {
        //            StreamUtils.Copy(streamReader, zipStream, buffer);
        //        }
        //        zipStream.CloseEntry();
        //    }

        //    zipStream.IsStreamOwner = true;
        //    zipStream.Close();

        //    string file_type = "application/zip";
        //    return File(fullZipPath, file_type, filename);
        //}


    }
} 
