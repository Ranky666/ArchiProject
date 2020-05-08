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

            List<ZipModel> files = Directory.GetFiles("D:/FilesforArchiving").Select(Name => new ZipModel { Name = Path.GetFileName(Name) }).ToList();


            return View(files);

        }


        [HttpPost]

        public ActionResult Index(List<ZipModel> files)
        {

            List<ZipModel> filename = files.Where(m => m.Selected == true).ToList();


            byte[] fileBytes = null;


            using (MemoryStream memoryStream = new MemoryStream())
            {
                // создание зип
                using (ZipArchive zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    //перебор файлов
                    foreach (ZipModel f in filename)
                    {
                        // 
                        ZipArchiveEntry zipItem = zip.CreateEntry(f.Name + f.Selected);
                                               
                        using MemoryStream originalFileMemoryStream = new MemoryStream(f.FileBytes);
                        using (Stream entryStream = zipItem.Open())
                        {
                            originalFileMemoryStream.CopyTo(entryStream);
                        }
                    }


                    fileBytes = memoryStream.ToArray();
                }

                //// скачивание 
                Response.Headers.Add("Content-Disposition", "attachment; filename=download.zip");

                return File(fileBytes, "application/zip");

            }

        }
    }
}             


