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
using Microsoft.AspNetCore.Identity;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesRepPointController : Controller
    {
        SalesRepPointService _salesRepPointService;
        private readonly UserManager<ApplicationUser> _userManager;
        Al3QaratContext ctx;
        public SalesRepPointController(UserManager<ApplicationUser> userManager, SalesRepPointService salesRepPointService, Al3QaratContext context)
        {
            _userManager = userManager;
            _salesRepPointService = salesRepPointService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,نقاط ممثلي المبيعات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsSalesRepPoint = _salesRepPointService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbSalesRepPoint ITEM, int id, List<IFormFile> files)
        {
            if (_userManager.Users.Where(a => a.Id == ITEM.SalesRepId.ToString()).FirstOrDefault().FirstName != null) 
            {
                ITEM.SalesRepName = _userManager.Users.Where(a => a.Id == ITEM.SalesRepId.ToString()).FirstOrDefault().FirstName;
            }
            else 
            {
                ITEM.SalesRepName = _userManager.Users.Where(a => a.Id == ITEM.SalesRepId.ToString()).FirstOrDefault().UserName;
            }
            
            if (ITEM.SalesRepPointId == null)
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





                    var result = _salesRepPointService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Sales Rep Point Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Sales Rep Point Profile  Creating.";
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






                    var result = _salesRepPointService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Sales Rep Point Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Sales Rep Point Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsSalesRepPoint = _salesRepPointService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف نقاط ممثلي المبيعات")]
        public IActionResult Delete(Guid id)
        {

            TbSalesRepPoint oldItem = ctx.TbSalesRepPoints.Where(a => a.SalesRepPointId == id).FirstOrDefault();



            var result = _salesRepPointService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Sales Rep Point Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Sales Rep Point Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsSalesRepPoint = _salesRepPointService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل نقاط ممثلي المبيعات")]
        public IActionResult Form(Guid? id)
        {
            TbSalesRepPoint oldItem = ctx.TbSalesRepPoints.Where(a => a.SalesRepPointId == id).FirstOrDefault();

            ViewBag.SalesReps = _userManager.Users.Where(a => a.AccountType == "موظف مبيعات").ToList();
            return View(oldItem);
        }
    }
}
