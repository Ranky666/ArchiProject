using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArchiProject.Models;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
        // метод индекс с парметрами 
        public IActionResult Index(List<ZipModel> files)
        {
            //    // получаем выбранные файлы из списка , затем сортируем по имени в список
            List<string> filenames = files.Where(m => m.Selected == true).Select(f => f.Name).ToList();

            // создается папка для хранения архивая
            if (!Directory.Exists("~D/FilesforArchiving/Comprest"))
                Directory.CreateDirectory("~D/FilesforArchiving/Comprest");

            // создаем имя для архива (на самом делел не понял, что делает эта штука Guid.NewGuid() Вики говорит, что это  
            //статистически уникальный 128-битный идентификатор, но суть его применения не ясна.
            string filename = Guid.NewGuid().ToString() + ".zip";
            string fullZipPath = ("~D/FilesforArchiving/Comprest" + filename);
            // определяем потоки для создания архива , в классе потока создает переменная, которая присваивается к System.IO
            FileStream fsOut = System.IO.File.Create(fullZipPath);
            // как я понял, данная страка архивирует с помощью класса ZipOutputStream
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); // уровень сжатия от 0 до 9

            //// перебираем выбранные файлы и добавляем в архив
            foreach (string file in filenames)
            {
                // с помощью класса FileInfo перебираются файлы в папке
                FileInfo fi = new FileInfo("~D/FilesforArchiving/Comprest" + file);
                // если файлы сществуют то продолжаем
                if (!fi.Exists)
                    continue;

                // происходит запись файла в каталог с помощью класса ZipEntry

                string entryName = ZipEntry.CleanName(fi.Name);
                ZipEntry newEntry = new ZipEntry(entryName);
                newEntry.DateTime = fi.LastWriteTime;
                newEntry.Size = fi.Length;
                zipStream.PutNextEntry(newEntry);

                byte[] buffer = new byte[4096];
                using (FileStream streamReader = System.IO.File.OpenRead(fi.FullName))
                {
                    StreamUtils.Copy(streamReader, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }

            zipStream.IsStreamOwner = true;
            zipStream.Close();

            string file_type = "Comprest/zip";
            return File(fullZipPath, file_type, filename);
        }


    }
} 
