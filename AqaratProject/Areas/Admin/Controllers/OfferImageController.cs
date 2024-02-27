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
using System.IO;
using Microsoft.AspNetCore.Identity;
using AqaratProject.Hubs;
using Microsoft.AspNetCore.SignalR;
using static AqaratProject.Hubs.NotificationHub;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferImageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        OfferImageService offerImageService;
        OfferService offerService;
        Al3QaratContext ctx;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public OfferImageController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, OfferService OfferService,OfferImageService OfferImageService, Al3QaratContext context)
        {
            offerService = OfferService;
            offerImageService = OfferImageService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,صور العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstOfferImages = offerImageService.getAll();
            model.lstOffers = offerService.getAll();
            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbOfferImage ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.OfferImageId == null)
            {


                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".jpg";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.OfferImagePath = ImageName;
                        }
                    }





                    var result = offerImageService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferImage Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة صورة  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferImagePath;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "اضافة صورة";
                            oTbRealTimeNotifcation.Notes = i.Id;
                            realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                            notificationCounter = ctx.TbRealTimeNotifcations.Count();

                        }
                        List<MessageObject> messages = new List<MessageObject>();
                        foreach (var i in ctx.TbRealTimeNotifcations.ToList())
                        {
                            messages.Add(new MessageObject { id = i.RealTimeNotifcationId.ToString(), mesage = i.NotificationType });


                        }
                        _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
                        _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in OfferImage Profile  Creating.";
                    }


                }


            }
            else
            {
                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".jpg";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.OfferImagePath = ImageName;
                        }
                    }






                    var result = offerImageService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferImage Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل صورة  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferImagePath;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "تعديل صورة";
                            oTbRealTimeNotifcation.Notes = i.Id;
                            realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                            notificationCounter = ctx.TbRealTimeNotifcations.Count();

                        }
                        List<MessageObject> messages = new List<MessageObject>();
                        foreach (var i in ctx.TbRealTimeNotifcations.ToList())
                        {
                            messages.Add(new MessageObject { id = i.RealTimeNotifcationId.ToString(), mesage = i.NotificationType });


                        }
                        _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
                        _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in OfferImage Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lstOfferImages = offerImageService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف صور العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOfferImage oldItem = ctx.TbOfferImages.Where(a => a.OfferImageId == id).FirstOrDefault();



            var result = offerImageService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferImage Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in OfferImage Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstOfferImages = offerImageService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin,اضافة او تعديل صور العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOfferImage oldItem = ctx.TbOfferImages.Where(a => a.OfferImageId == id).FirstOrDefault();
            oldItem = ctx.TbOfferImages.Where(a => a.OfferImageId == id).FirstOrDefault();

            return View(oldItem);
        }
        [Authorize(Roles = "Admin,اضافة او تعديل صور العروض")]
        public IActionResult FormAdd(Guid? id)
        {
            TbOfferImage oldItem = new TbOfferImage();
            oldItem.OfferId = id;

            return View(oldItem);
        }




        
    }
}
