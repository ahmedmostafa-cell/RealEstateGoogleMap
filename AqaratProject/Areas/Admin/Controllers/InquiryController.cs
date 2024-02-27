using AqaratProject.Models;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Domains;
using System.Linq;
using AqaratProject.Hubs;
using static AqaratProject.Hubs.NotificationHub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InquiryController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        InquiryService inquiryService;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        Al3QaratContext ctx;
        public InquiryController(UserManager<ApplicationUser> userManager, IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, InquiryService InquiryService, Al3QaratContext context)
        {

            inquiryService = InquiryService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin,استفسارات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstInquiries = inquiryService.getAll();

            return View(model);


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbInquiry ITEM, int id, List<IFormFile> files)
        {
            ITEM.CreatedBy = _userManager.GetUserAsync(User).Result.Id;
            ITEM.UpdatedBy = _userManager.GetUserAsync(User).Result.FirstName;
            if (ITEM.InquiryId == null)
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





                    var result = inquiryService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Inquiry Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة استفسار  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.InquirySyntax;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.InquiryId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "اضافة استفسار";
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
                        TempData[SD.Error] = "Error in Inquiry Profile  Creating.";
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






                    var result = inquiryService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Inquiry Profile successfully Updated.";
                        TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                        oTbRealTimeNotifcation.NotificationType = "تم تعديل استفسار  ";
                        oTbRealTimeNotifcation.NotificationSyntax = ITEM.InquirySyntax;


                        oTbRealTimeNotifcation.CreatedBy = ITEM.InquiryId.ToString();
                        oTbRealTimeNotifcation.UpdatedBy = "تعديل استفسار";
                        realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                        notificationCounter = ctx.TbRealTimeNotifcations.Count();

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
                        TempData[SD.Error] = "Error in Inquiry Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstInquiries = inquiryService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف الاستفسارات")]
        public IActionResult Delete(Guid id)
        {

            TbInquiry oldItem = ctx.TbInquiries.Where(a => a.InquiryId == id).FirstOrDefault();



            var result = inquiryService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Inquiry Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Inquiry Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lstInquiries = inquiryService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل الاستفسارات")]
        public IActionResult Form(Guid? id)
        {
            TbInquiry oldItem = ctx.TbInquiries.Where(a => a.InquiryId == id).FirstOrDefault();
            oldItem = ctx.TbInquiries.Where(a => a.InquiryId == id).FirstOrDefault();

            return View(oldItem);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Form2(string id)
        {
            TbInquiry oldItem = ctx.TbInquiries.Where(a => a.InquiryId == Guid.Parse(id)).FirstOrDefault();


            return View(oldItem);
        }
    }
}
