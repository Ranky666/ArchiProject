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


            byte[] fileBytes = null;


            // начал придумывать цикл, по идее, надо чтобы он пробегал по списку и закидывал только нужны файлы , но чего то не хвататет тут
            foreach (var item in files)
            {
                files.AddRange(Directory.GetFiles(item,));
            }

          
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // создание зип
                using (ZipArchive zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    // перебор файлов
                    foreach (ZipModel f in files)
                    {
                        // 
                        ZipArchiveEntry zipItem = zip.CreateEntry(f.Name + "." + f.Selected);
                        // 
                        //using (MemoryStream originalFileMemoryStream = new MemoryStream(f.Selected))
                        //{
                        //    using (Stream entryStream = zipItem.Open())
                        //    {
                        //        originalFileMemoryStream.CopyTo(entryStream);
                        //    }
                        //}
                    }
                }
                fileBytes = memoryStream.ToArray();
            }

            // скачивание 
            Response.Headers.Add("Content-Disposition", "attachment; filename=download.zip");
            return File(fileBytes, "application/zip");

        }

    }
}              



            //string sourceFile = "D://test/book.pdf"; // исходный файл
            //string compressedFile = "D://test/book.gz"; // сжатый файл

            //using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            //{

            //    using (FileStream targetStream = File.Create(compressedFile))
            //    {
            //        // поток архивации
            //        using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
            //        {
            //            sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой

            //        } 
            //    }


            //}

            //    return View();
        
       

        //public ActionResult Index(List<ZipModel> files)
        //{
        

        //    //var filenames = Directory.GetFiles(@"D:/FilesforArchiving");
        //    //var zipFile = @"D:/FilesforArchiving/Comprest.zip";


        //    //using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        //    //{
        //    //    foreach (var fPath in filenames)
        //    //    {
        //    //        archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
        //    //    }
        //    //}


        //    string folderToZip = @"D:/FilesforArchiving";
        //    string zipFile = @"D:/FilesforArchiving/Comprest.zip";

        //    using (ZipArchive zipArchive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
        //    {

        //        DirectoryInfo di = new DirectoryInfo(folderToZip);

        //        FileInfo[] filesToArchive = di.GetFiles();


        //        if (filesToArchive != null && filesToArchive.Length > 0)
        //        {

        //            foreach (FileInfo fileToArchive in filesToArchive)
        //            {

        //                zipArchive.CreateEntryFromFile(fileToArchive.FullName, fileToArchive.Name, CompressionLevel.Optimal);
        //            }
        //        }
        //    }

        //    return View();

        //}

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






 
