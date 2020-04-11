using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArchiProject.Models;
using AutoMapper;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace ArchiProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

       


        private readonly IAuthorizationService _noteService;

        private readonly IMapper _mapper;



        public HomeController(IAuthorizationService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }


           //[AllowAnonymous]
        public IActionResult Index()
        {

            //return Content(User.Identity.Name);

            return View(_mapper.Map<List<ZipModel>>(_noteService.Archi(SearchString)));

        }



        public ActionResult Archi()
        {
            string path = ("~D/FilesforArchiving/");
            List<string> files = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(path);

            files.AddRange(dir.GetFiles().Select(f => f.Name));

            return View(files);
        }




        [HttpPost]
        public ActionResult Archi(List<ZipModel> files)
        {
            // получаем выбранные файлы
            List<string> filenames = files.Where(m => m.Selected == true).Select(f => f.Name).ToList();

            // создаем папку Temp для хранения архива
            if (!Directory.Exists("~D/FilesforArchiving/Comprest"))
                Directory.CreateDirectory("~D/FilesforArchiving/Comprest");

            // создаем имя для архива
            string filename = Guid.NewGuid().ToString() + ".zip";
            string fullZipPath = ("~D/FilesforArchiving/Comprest" + filename);
            // определяем потоки для создания архива
            FileStream fsOut = System.IO.File.Create(fullZipPath);
            ZipOutputStream zipStream = new ZipOutputStream(fsOut);

            zipStream.SetLevel(3); // уровень сжатия от 0 до 9

            // перебираем выбранные файлы и добавляем в архив
            foreach (string file in filenames)
            {
                FileInfo fi = new FileInfo("~D/FilesforArchiving/" + file);

                if (!fi.Exists)
                    continue;

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

            string file_type = "application/zip";
            return File(fullZipPath, file_type, filename);
        }





    }
} 
