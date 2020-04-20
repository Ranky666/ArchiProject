using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchiProject.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.IO.Compression;

namespace ArchiProject.Controllers
{
    //[Authorize]
    [AllowAnonymous]
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public IActionResult Index()
        {

            List<string> files = Directory.GetFiles("D:/FilesforArchiving").Select(Name => Path.GetFileName(Name)).ToList();

            return View(files);

        }


        [HttpPost]

        public ActionResult Index(List<ZipModel> files)
        {
           List<string> filename = files.Where(m => m.Selected == true).Select(f => f.Name).ToList();

         // я знаю, что этого не достаточно

            var filenames = Directory.GetFiles(@"D:/FilesforArchiving");
            var zipFile = @"D:/FilesforArchiving/Comprest.zip";

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {
                foreach (var fPath in filenames)
                {
                    archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
                }
            }



            return View();

        }

//        // отростированный список 
//        List<string> filenames = files.Where(m => m.Selected == true).Select(f => f.Name).ToList();

//        // дергаетс помощью Guid 
//        string filename = Guid.NewGuid().ToString() + ".zip";

//        // так, как я понял, выделяется память потока и второй строкой туда запиливается  сжатый файлик
//        MemoryStream outputMemStream = new MemoryStream();
//        ZipOutputStream zipStream = new ZipOutputStream(outputMemStream);

//        zipStream.SetLevel(3); // уровень сжатия от 0 до 9

//            // в цикле все тоже самое, что и в закоменчином коде, но тут работает, ну почти(
//            foreach (string file in filenames)
//            {
//                // я понял, что проблема с загрузкой файлов в этой строке, ибо она не видит их в папке

//                FileInfo fi = new FileInfo("~D/FilesforArchiving/Comprest" + file);

//        string entryName = ZipEntry.CleanName(fi.Name);
//        ZipEntry newEntry = new ZipEntry(entryName);
//        newEntry.DateTime = fi.LastWriteTime;
//                newEntry.Size = fi.Length;
//                zipStream.PutNextEntry(newEntry);

//                byte[] buffer = new byte[4096];
//                using (FileStream streamReader = System.IO.File.OpenRead(fi.FullName))
//                {
//                    StreamUtils.Copy(streamReader, zipStream, buffer);
//                }
//zipStream.CloseEntry();
//            }
//            zipStream.IsStreamOwner = false;
//            zipStream.Close();

//            outputMemStream.Position = 0;

//            string file_type = "application/zip";
//            return File(outputMemStream, file_type, filename);




             
    }

} 
