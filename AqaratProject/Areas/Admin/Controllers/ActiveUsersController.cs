using AqaratProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActiveUsersController : Controller
    {
        ActiveUsersService activeUsersService;
        private readonly UserManager<ApplicationUser> _userManager;
        Al3QaratContext ctx;
        public ActiveUsersController(UserManager<ApplicationUser> userManager, ActiveUsersService ActiveUsersService, Al3QaratContext context)
        {
            _userManager = userManager;
            activeUsersService = ActiveUsersService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,المستخدمين النشطين الان")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsActiveUsers = activeUsersService.getAll();
            ViewBag.Regions = _userManager.Users.ToList();
            return View(model);


        }
        [Authorize(Roles = "Admin")]
        public IActionResult EmplyeePerformance()
        {

            HomePageModel model = new HomePageModel();
            model.lsActiveUsers = activeUsersService.getAll();
            ViewBag.Regions = _userManager.Users.ToList();
            return View(model);


        }



        [HttpPost]
        public async Task<IActionResult> Save(TbActiveUsers ITEM, int id, List<IFormFile> files)
        {
            ITEM.LoggedUserName = _userManager.Users.Where(a => a.Id == ITEM.LoggedUserId.ToString()).FirstOrDefault().FirstName;
            if (ITEM.LoggingId == null)
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





                    var result = activeUsersService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Active Users Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Active Users Profile  Creating.";
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






                    var result = activeUsersService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Active Users Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Active Users Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsActiveUsers = activeUsersService.getAll();
            ViewBag.Regions = _userManager.Users.ToList();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف المستخدمين النشطين الان")]
        public IActionResult Delete(Guid id)
        {

            TbActiveUsers oldItem = ctx.TbActiveUserss.Where(a => a.LoggingId == id).FirstOrDefault();



            var result = activeUsersService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Active Users Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Active Users Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsActiveUsers = activeUsersService.getAll();
            ViewBag.Regions = _userManager.Users.ToList();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل المستخدمين النشطين الان")]
        public IActionResult Form(Guid? id)
        {
            TbActiveUsers oldItem = ctx.TbActiveUserss.Where(a => a.LoggingId == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
