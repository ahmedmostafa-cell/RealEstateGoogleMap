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
    public class RegionController : Controller
    {
        RequestService requestService;
        RegionService regionService;
        OfferService offerService;
        Al3QaratContext ctx;
        public RegionController(RequestService RequestService,OfferService OfferService,RegionService RegionService, Al3QaratContext context)
        {
            requestService = RequestService;
            offerService = OfferService;
            regionService = RegionService;
            ctx = context;

        }
        [Authorize(Roles = "Admin,المناطق")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsRegion = regionService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbRegion ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.RegionId == null)
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





                    var result = regionService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Region Profile successfully Created.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Region Profile  Creating.";
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






                    var result = regionService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Region Profile successfully Updated.";
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in Region Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsRegion = regionService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف المناطق")]
        public IActionResult Delete(Guid id)
        {

            TbRegion oldItem = ctx.TbRegions.Where(a => a.RegionId == id).FirstOrDefault();


           
            var resultOffer = true;
            foreach (var i in offerService.getAll())
            {
                if (i.RegionId == id )
                {
                    resultOffer = false;
                }

            }
            var resultRequest = true;
            foreach (var i in requestService.getAll())
            {
                if (i.RegionId == id)
                {
                    resultRequest = false;
                }

            }
           



            if (resultOffer == true && resultRequest == true )
            {
                var result = regionService.Delete(oldItem);
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
                TempData[SD.Error] = "يوجد معلومات متصلة بالمنطقة ف تقاير اخري و لا يمكن مسحها";
            }


          

            HomePageModel model = new HomePageModel();
            model.lsRegion = regionService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل المناطق")]
        public IActionResult Form(Guid? id)
        {
            TbRegion oldItem = ctx.TbRegions.Where(a => a.RegionId == id).FirstOrDefault();
            
            

            return View(oldItem);
        }
    }
}
