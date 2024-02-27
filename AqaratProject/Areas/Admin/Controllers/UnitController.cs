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
    public class UnitController : Controller
    {
        UnitService unitService;
        OfferService offerService;
        RequestService requestService;
        Al3QaratContext ctx;
        public UnitController(RequestService RequestService,OfferService OfferService,UnitService UnitService, Al3QaratContext context)
        {
            requestService = RequestService;
            offerService = OfferService;
            unitService = UnitService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,الوحدات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsUnit = unitService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbUnit ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.UnitId == null)
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





                    var result = unitService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Unit Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Unit Profile  Creating.";
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






                    var result = unitService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Unit Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Unit Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsUnit = unitService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الوحدات")]
        public IActionResult Delete(Guid id)
        {

            TbUnit oldItem = ctx.TbUnits.Where(a => a.UnitId == id).FirstOrDefault();

            var resultOffer = true;
            foreach (var i in offerService.getAll())
            {
                if (i.UnitId == id)
                {
                    resultOffer = false;
                }

            }
            var resultRequest = true;
            foreach (var i in requestService.getAll())
            {
                if (i.UnitId == id)
                {
                    resultRequest = false;
                }

            }




            if (resultOffer == true && resultRequest == true)
            {
                var result = unitService.Delete(oldItem);
                if (result == true)
                {
                    TempData[SD.Success] = "Region Profile successfully Removed.";
                }
                else
                {
                    TempData[SD.Error] = "Error in Region Profile  Removing.";
                }
            }
            else
            {
                TempData[SD.Error] = "يوجد معلومات متصلة بالوحدة ف تقاير اخري و لا يمكن مسحها";
            }


           
            

            HomePageModel model = new HomePageModel();
            model.lsUnit = unitService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل الوحدات")]
        public IActionResult Form(Guid? id)
        {
            TbUnit oldItem = ctx.TbUnits.Where(a => a.UnitId == id).FirstOrDefault();
           

            return View(oldItem);
        }
    }
}
