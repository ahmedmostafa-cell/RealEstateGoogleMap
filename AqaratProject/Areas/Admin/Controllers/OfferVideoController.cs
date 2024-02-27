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
    public class OfferVideoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        OfferVideoService offerVideoService;
        OfferService offerService;
        Al3QaratContext ctx;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public OfferVideoController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, OfferService OfferService, OfferVideoService OfferVideoService, Al3QaratContext context)
        {
            offerService = OfferService;
            offerVideoService = OfferVideoService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
            _userManager = userManager;

        }
        [Authorize(Roles = "Admin,فيديوهات العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsOfferVideo = offerVideoService.getAll();
            model.lstOffers = offerService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbOfferVideo ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.OfferVideoId == null)
            {


                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".mp4";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.OfferVideoPath = ImageName;
                        }
                    }





                    var result = offerVideoService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferVideo Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferVideoPath;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "اضافة عرض";
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
                        TempData[SD.Error] = "Error in OfferVideo Profile  Creating.";
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
                            string ImageName = Guid.NewGuid().ToString() + ".mp4";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.OfferVideoPath = ImageName;
                        }
                    }






                    var result = offerVideoService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferVideo Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferVideoPath;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "تعديل عرض";
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
                        TempData[SD.Error] = "Error in OfferVideo Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lsOfferVideo = offerVideoService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف فيديوهات العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOfferVideo oldItem = ctx.TbOfferVideos.Where(a => a.OfferVideoId == id).FirstOrDefault();



            var result = offerVideoService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferVideo Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in OfferVideo Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsOfferVideo = offerVideoService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل فيديوهات العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOfferVideo oldItem = ctx.TbOfferVideos.Where(a => a.OfferVideoId == id).FirstOrDefault();
            oldItem = ctx.TbOfferVideos.Where(a => a.OfferVideoId == id).FirstOrDefault();

            return View(oldItem);
        }

        [Authorize(Roles = "Admin, اضافة او تعديل فيديوهات العروض")]
        public IActionResult FormAdd(Guid? id)
        {
            TbOfferVideo oldItem = new TbOfferVideo(); ;
            oldItem.OfferId = id;

            return View(oldItem);
        }
    }
}
