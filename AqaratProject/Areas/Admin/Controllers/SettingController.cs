using AqaratProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        SettingService settingService;

        Al3QaratContext ctx;
        public SettingController(SettingService SettingService, Al3QaratContext context)
        {

            settingService = SettingService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الاعدادات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsSetting = settingService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbSetting ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.SettingId == null)
            {


                if (ModelState.IsValid)
                {
                    //foreach (var file in files)
                    //{
                    //    if (file.Length > 0)
                    //    {
                    //        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    //        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    //        using (var stream = System.IO.File.Create(filePaths))
                    //        {
                    //            await file.CopyToAsync(stream);
                    //        }
                    //        ITEM.ab = ImageName;
                    //    }
                    //}





                    var result = settingService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Setting Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Setting Profile  Creating.";
                    }


                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    //foreach (var file in files)
                    //{
                    //    if (file.Length > 0)
                    //    {
                    //        string ImageName = Guid.NewGuid().ToString() + ".jpg";
                    //        var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    //        using (var stream = System.IO.File.Create(filePaths))
                    //        {
                    //            await file.CopyToAsync(stream);
                    //        }
                    //        ITEM.MainConsultingImage = ImageName;
                    //    }
                    //}






                    var result = settingService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Setting Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Setting Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsSetting = settingService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الاعدادات")]
        public IActionResult Delete(Guid id)
        {

            TbSetting oldItem = ctx.TbSettings.Where(a => a.SettingId == id).FirstOrDefault();



            var result = settingService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Setting Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Setting Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsSetting = settingService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل الاعدادات")]
        public IActionResult Form(Guid? id)
        {
            TbSetting oldItem = ctx.TbSettings.Where(a => a.SettingId == id).FirstOrDefault();
          

            return View(oldItem);
        }
    }
}
