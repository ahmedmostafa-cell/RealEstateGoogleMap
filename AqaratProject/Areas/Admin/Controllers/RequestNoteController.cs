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
    public class RequestNoteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        RequestNoteService requestNoteService;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;

        Al3QaratContext ctx;
        public RequestNoteController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, RequestNoteService RequestNoteService, Al3QaratContext context)
        {
            _userManager = userManager;
            requestNoteService = RequestNoteService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
        }
        [Authorize(Roles = "Admin,ملاحظات الطلب ")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsRequestNote = requestNoteService.getAll();

            return View(model);


        }
        [Authorize(Roles = "Admin,ملاحظات الطلب")]
        public IActionResult Index2(Guid? id)
        {

            HomePageModel model = new HomePageModel();
            model.lsRequestNote = requestNoteService.getAll().Where(a => a.RequestId == id).OrderBy(a => a.CreatedDate);
            ViewBag.Visible = "اظهار";
            ViewBag.InVisible = "اخفاء";
            return View(model);


        }


        [Authorize(Roles = "Admin, اظهار او اخفاء ملاحظات الطلب ")]
        public IActionResult Visibility(Guid? id)
        {

            TbRequestNote oldItem = ctx.TbRequestNotes.Where(a => a.RequestNotesId == id).FirstOrDefault();
            oldItem.Visibility = "false";
            var result = requestNoteService.Edit(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "RequestNote Profile successfully Updated.";
            }
            else
            {
                TempData[SD.Error] = "Error in RequestNote Profile  Updating.";
            }
            return RedirectToAction("Details", "Request", new { id = oldItem.RequestId });


        }


        [Authorize(Roles = "Admin, اظهار او اخفاء ملاحظات الطلب ")]
        public IActionResult Visibility2(Guid? id)
        {

            TbRequestNote oldItem = ctx.TbRequestNotes.Where(a => a.RequestNotesId == id).FirstOrDefault();
            oldItem.Visibility = "true";
            var result = requestNoteService.Edit(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "RequestNote Profile successfully Updated.";
            }
            else
            {
                TempData[SD.Error] = "Error in RequestNote Profile  Updating.";
            }
            return RedirectToAction("Details", "Request", new { id = oldItem.RequestId });


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbRequestNote ITEM, int id, List<IFormFile> files)
        {
            if (ITEM.RequestNotesId == null)
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

                    var result = requestNoteService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Request Note Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة طلب";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.NoteSyntax;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.RequestId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "اضافة طلب";
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
                        TempData[SD.Error] = "Error in Request Note Profile  Creating.";
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






                    var result = requestNoteService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Request Note Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList())
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل طلب";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.NoteSyntax;


                            oTbRealTimeNotifcation.CreatedBy = ITEM.RequestId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "تعديل طلب";
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
                        TempData[SD.Error] = "Error in Request Note Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsRequestNote = requestNoteService.getAll();
            return RedirectToAction("Details", "Request", new { id = ITEM.RequestId });
        }




        [Authorize(Roles = "Admin,حذف ملاحظات الطلب")]
        public IActionResult Delete(Guid id)
        {

            TbRequestNote oldItem = ctx.TbRequestNotes.Where(a => a.RequestNotesId == id).FirstOrDefault();



            var result = requestNoteService.Delete(oldItem);
            if (result == true)
            {
                TempData[SD.Success] = "Request Note Profile successfully Removed.";
            }
            else
            {
                TempData[SD.Error] = "Error in Request Note Profile  Removing.";
            }

            HomePageModel model = new HomePageModel();
            model.lsRequestNote = requestNoteService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل ملاحظات الطلب")]
        public IActionResult Form(Guid? id)
        {
            TbRequestNote oldItem = ctx.TbRequestNotes.Where(a => a.RequestNotesId == id).FirstOrDefault();
           

            return View(oldItem);
        }
        [Authorize(Roles = "Admin, اضافة او تعديل ملاحظات الطلب")]
        public IActionResult Form2(Guid? id)
        {


            TbRequestNote newItem = new TbRequestNote();
            newItem.RequestId = id;
            newItem.SenderId = _userManager.GetUserAsync(User).Result.Id;
            newItem.SenderName = _userManager.GetUserAsync(User).Result.FirstName;
            newItem.SenderDepartment = _userManager.GetUserAsync(User).Result.Department;

            return View(newItem);
        }
    }
}
