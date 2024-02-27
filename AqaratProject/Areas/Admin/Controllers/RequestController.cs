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
    public class RequestController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        RequestService  requestService;
        RequestNoteService requestNoteService;
        Al3QaratContext ctx;
        RegionService regionService;
        UnitService unitService;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public RequestController(IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, RegionService RegionService, UnitService UnitService, RequestNoteService RequestNoteService ,RequestService  RequestService, Al3QaratContext context)
        {
            _userManager = userManager;
            regionService = RegionService;
            unitService = UnitService;
            requestNoteService = RequestNoteService;
            requestService = RequestService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;

        }
        [Authorize(Roles = "Admin,طلبات")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lsRequest = requestService.getAll();

            return View(model);


        }

        [Authorize(Roles = "Admin")]
        public IActionResult Approve(string id)
        {

            TbRequest oldItem = ctx.TbRequests.Where(a => a.RequestId == Guid.Parse(id)).FirstOrDefault();
            oldItem.Notes = "موافقة";
            var result = requestService.Edit(oldItem);
            if (result == true)
            {
                foreach (var i in _userManager.Users.ToList())
                {
                    TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                    oTbRealTimeNotifcation.NotificationType = "تم الموافقة عل الطلب  ";
                    oTbRealTimeNotifcation.NotificationSyntax = oldItem.RequestText;


                    oTbRealTimeNotifcation.CreatedBy = oldItem.RequestId.ToString();
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
                TempData[SD.Error] = "Error in Offer Profile  Updating.";
            }
            return RedirectToAction("Index");


        }

        [Authorize(Roles = "Admin,طلبات")]
        public IActionResult Requesters()
        {

            HomePageModel model = new HomePageModel();
            model.lsRequest = requestService.getAll();

            return View(model);


        }


        

        [HttpPost]
        public async Task<IActionResult> Save(TbRequest ITEM, int id, List<IFormFile> files)
        {
            ITEM.UnitName = unitService.getAll().Where(a => a.UnitId == ITEM.UnitId).FirstOrDefault().UnitName;
            ITEM.RegionName = regionService.getAll().Where(a => a.RegionId == ITEM.RegionId).FirstOrDefault().RegionName;
            ITEM.CreatedBy = _userManager.GetUserAsync(User).Result.Id;
            ITEM.UpdatedBy = _userManager.GetUserAsync(User).Result.FirstName;
            ITEM.icon = "screwdriver-wrench";
            ITEM.contract_number = ("غير محجوز");
            if (ITEM.RequestId == null)
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





                    var result = requestService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Request Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة طلب";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.RequestText;


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
                        TempData[SD.Error] = "Error in Request Profile  Creating.";
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


                    ITEM.contract_number = ("غير محجوز");

                    ITEM.icon = "screwdriver-wrench";

                    var result = requestService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Request Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل طلب";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.RequestText;


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
                        TempData[SD.Error] = "Error in Request Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lsRequest = requestService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف طلبات")]
        public IActionResult Delete(Guid id)
        {

            TbRequest oldItem = ctx.TbRequests.Where(a => a.RequestId == id).FirstOrDefault();

           
          
            var resultRequestNotes = true;
            foreach (var i in requestNoteService.getAll())
            {
                if (i.RequestId == id)
                {
                    resultRequestNotes = false;
                }

            }
           



            if (resultRequestNotes == true)
            {
                var result = requestService.Delete(oldItem);
                if (result == true)
                {
                    TempData[SD.Success] = "Offer Profile successfully Removed.";
                }
                else
                {
                    TempData[SD.Error] = "Error in Offer Profile  Removing.";
                }

            }
            else
            {
                TempData[SD.Error] = "يوجد معلومات متصلة بالعرض ف تقاير اخري و لا يمكن مسحها";
            }

           
         

            HomePageModel model = new HomePageModel();
            model.lsRequest = requestService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل طلبات")]
        public IActionResult Form(Guid? id)
        {
            TbRequest oldItem = ctx.TbRequests.Where(a => a.RequestId == id).FirstOrDefault();
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();

            return View(oldItem);
        }

        [Authorize(Roles = "Admin,تفاصيل الطلبات ")]
        public IActionResult Details(Guid? id)
        {
            HomePageModel model = new HomePageModel();
          
           
            model.OneRequest = requestService.getAll().Where(a => a.RequestId == id).FirstOrDefault();
            model.lsRequestNote = requestNoteService.getAll().Where(a => a.RequestId == id).OrderBy(a => a.CreatedDate).Where(a => a.Visibility != "false");
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Form2(string id)
        {
            TbRequest oldItem = ctx.TbRequests.Where(a => a.RequestId == Guid.Parse(id)).FirstOrDefault();


            return View(oldItem);
        }
    }
}
