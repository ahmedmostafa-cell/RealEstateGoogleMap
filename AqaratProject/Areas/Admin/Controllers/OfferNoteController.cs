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
using AqaratProject.Hubs;
using static AqaratProject.Hubs.NotificationHub;
using Microsoft.AspNetCore.SignalR;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferNoteController : Controller
    {
        RealTimeNotifcationService realTimeNotifcationService;
        OfferNoteService offerNoteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        Al3QaratContext ctx;
        public OfferNoteController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, OfferNoteService OfferNoteService, Al3QaratContext context)
        {
            _userManager = userManager;

            _signInManager = signInManager;

            offerNoteService = OfferNoteService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;

        }
        [Authorize(Roles = "Admin,ملاحظات العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsOfferNote = offerNoteService.getAll();

            return View(model);


        }
        [Authorize(Roles = "Admin,ملاحظات العروض")]
        public IActionResult Index2(Guid? id)
        {

            HomePageModel model = new HomePageModel();
            model.lsOfferNote = offerNoteService.getAll().Where(a=> a.OfferId == id).OrderBy(a=> a.CreatedDate);
            ViewBag.Visible = "اظهار";
            ViewBag.InVisible = "اخفاء";
            return View(model);


        }
        [Authorize(Roles = "Admin, اظهار او اخفاء ملاحظات العروض ")]
        public IActionResult Visibility(Guid? id)
        {

            TbOfferNote oldItem = ctx.TbOfferNotes.Where(a => a.OfferNotesId == id).FirstOrDefault();
            oldItem.Visibility = "false";
            var result = offerNoteService.Edit(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferNote Profile successfully Updated.";
               
            }
            else
            {
                TempData[SD.Error] = "Error in OfferNote Profile  Updating.";
            }
            return RedirectToAction("Details", "Offer", new { id = oldItem.OfferId });


        }


        [Authorize(Roles = "Admin, اظهار او اخفاء ملاحظات العروض ")]
        public IActionResult Visibility2(Guid? id)
        {

            TbOfferNote oldItem = ctx.TbOfferNotes.Where(a => a.OfferNotesId == id).FirstOrDefault();
            oldItem.Visibility = "true";
            var result = offerNoteService.Edit(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferNote Profile successfully Updated.";
            }
            else
            {
                TempData[SD.Error] = "Error in OfferNote Profile  Updating.";
            }
            return RedirectToAction("Details", "Offer", new { id = oldItem.OfferId });


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbOfferNote ITEM, int id, List<IFormFile> files)
        {

            if (ITEM.OfferNotesId == null)
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




                    ITEM.Visibility = "true";
                    var result = offerNoteService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferNote Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة ملاحظة  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.NoteSyntax;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "اضافة ملاحظة";
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
                        TempData[SD.Error] = "Error in OfferNote Profile  Creating.";
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






                    var result = offerNoteService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferNote Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل ملاحظة  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.NoteSyntax;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "تعديل ملاحظة";
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
                        TempData[SD.Error] = "Error in OfferNote Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lsOfferNote = offerNoteService.getAll();
            return RedirectToAction("Details", "Offer" , new {id = ITEM.OfferId }  );
        }




        [Authorize(Roles = "Admin,حذف ملاحظات العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOfferNote oldItem = ctx.TbOfferNotes.Where(a => a.OfferNotesId == id).FirstOrDefault();

            Guid? id2 = oldItem.OfferId;

            var result = offerNoteService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferNote Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in OfferNote Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsOfferNote = offerNoteService.getAll();
            return RedirectToAction("Details", "Offer", new { id = id2 });



        }



        [Authorize(Roles = "Admin, اضافة او تعديل ملاحظات العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOfferNote oldItem = ctx.TbOfferNotes.Where(a => a.OfferNotesId == id).FirstOrDefault();
            oldItem = ctx.TbOfferNotes.Where(a => a.OfferNotesId == id).FirstOrDefault();

            return View(oldItem);
        }
        [Authorize(Roles = "Admin, اضافة او تعديل ملاحظات العروض")]
        public IActionResult Form2(Guid? id)
        {

           
            TbOfferNote newItem = new TbOfferNote();
            newItem.OfferId = id;
            newItem.SenderId = _userManager.GetUserAsync(User).Result.Id;
            newItem.SenderName = _userManager.GetUserAsync(User).Result.FirstName;
            newItem.SenderDepartment = _userManager.GetUserAsync(User).Result.AccountType;

            return View(newItem);
        }
    }
}
