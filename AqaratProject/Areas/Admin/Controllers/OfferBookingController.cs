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
    public class OfferBookingController : Controller
    {
        OfferService offerService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferBookingService offerBookingService;
        OfferNoteService offerNoteService;
        Al3QaratContext ctx;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public OfferBookingController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, OfferNoteService OfferNoteService,OfferService OfferService,UserManager<ApplicationUser> userManager, OfferBookingService OfferBookingService, Al3QaratContext context)
        {
            offerService = OfferService;
            _userManager = userManager;
            offerBookingService = OfferBookingService;
            ctx = context;
            offerNoteService = OfferNoteService;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;

        }
        [Authorize(Roles = "Admin,حجز العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsOfferBooking = offerBookingService.getAll();
            model.lstOffers = offerService.getAll();
            return View(model);


        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(TbOfferBooking ITEM, int id, List<IFormFile> files)
        {
            ITEM.SalesRepId = _userManager.GetUserAsync(User).Result.Id;
            ITEM.SalesRepName = _userManager.GetUserAsync(User).Result.FirstName;
            ITEM.CreatedBy = "الحجز فعال";
            if (ITEM.OfferBookingId == null)
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





                    var result = offerBookingService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferBooking Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم حجز عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.ValueToBePaid;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferBookingId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "حجز عرض";
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
                        TbOfferNote oTbOfferNote = new TbOfferNote();
                        oTbOfferNote.OfferId = ITEM.OfferId;
                        oTbOfferNote.SenderId = _userManager.GetUserAsync(User).Result.Id;
                        oTbOfferNote.SenderName = _userManager.GetUserAsync(User).Result.FirstName;
                        oTbOfferNote.SenderDepartment = _userManager.GetUserAsync(User).Result.Department;
                        oTbOfferNote.NoteSyntax = "قام بحجز الفرع";
                        oTbOfferNote.Visibility = "true";
                        offerNoteService.Add(oTbOfferNote);
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in OfferBooking Profile  Creating.";
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






                    var result = offerBookingService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "OfferBooking Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل حجز عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.ValueToBePaid;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.OfferBookingId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "تعديل حجز عرض";
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
                        TbOfferNote oTbOfferNote = new TbOfferNote();
                        oTbOfferNote.OfferId = ITEM.OfferId;
                        oTbOfferNote.SenderId = _userManager.GetUserAsync(User).Result.Id;
                        oTbOfferNote.SenderName = _userManager.GetUserAsync(User).Result.FirstName;
                        oTbOfferNote.SenderDepartment = _userManager.GetUserAsync(User).Result.Department;
                        oTbOfferNote.NoteSyntax = "قام بحجز الفرع";
                        oTbOfferNote.Visibility = "true";
                        offerNoteService.Add(oTbOfferNote);
                    }
                    else
                    {
                        TempData[SD.Error] = "Error in OfferBooking Profile  Updating.";
                    }
                }
            }




            HomePageModel model = new HomePageModel();
            model.lsOfferBooking = offerBookingService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف حجز العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOfferBooking oldItem = ctx.TbOfferBookings.Where(a => a.OfferBookingId == id).FirstOrDefault();
            TbOffer oTbOffer = offerService.getAll().Where(a => a.OfferId == oldItem.OfferId).FirstOrDefault();

            oTbOffer.contract_type = "غير محجوز";


            var result = offerBookingService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "OfferBooking Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in OfferBooking Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsOfferBooking = offerBookingService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل حجز العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOfferBooking oldItem = ctx.TbOfferBookings.Where(a => a.OfferBookingId == id).FirstOrDefault();
            oldItem = ctx.TbOfferBookings.Where(a => a.OfferBookingId == id).FirstOrDefault();

            return View(oldItem);
        }



        [Authorize(Roles = "Admin, اضافة او تعديل حجز العروض")]
        public IActionResult Form3(Guid? id)
        {
            TbOfferBooking oldItem = ctx.TbOfferBookings.Where(a => a.OfferBookingId == id).FirstOrDefault();
            oldItem.CurrentState = 1;
            oldItem.CreatedDate = DateTime.Now;
            oldItem.CreatedBy = "الحجز فعال";
            offerBookingService.Edit(oldItem);
            TbOffer oTbOffer = offerService.getAll().Where(a => a.OfferId == oldItem.OfferId).FirstOrDefault();

            oTbOffer.contract_type = "محجوز";
            offerService.Edit(oTbOffer);
            HomePageModel model = new HomePageModel();
            model.lsOfferBooking = offerBookingService.getAll();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
          
        }

        [Authorize(Roles = "Admin, اضافة او تعديل حجز العروض")]
        public IActionResult Form2(Guid? id)
        {
            HomePageModel model = new HomePageModel();
            model.lsOfferBooking = offerBookingService.getAll().Where(a=> a.CurrentState ==0).ToList();
            foreach(var i in model.lsOfferBooking) 
            {
                if(i.OfferId == id && i.SalesRepId == _userManager.GetUserAsync(User).Result.Id ) 
                {
                    return View("AccessDenied");
                }

            }



            TbOfferBooking oldItem = new TbOfferBooking();
            oldItem.OfferId = id;
            TbOffer otTbOffer = offerService.getAll().Where(a=> a.OfferId == id).FirstOrDefault();
            otTbOffer.contract_type = "محجوز";
            offerService.Edit(otTbOffer);
            return View(oldItem);
        }
    }
}
