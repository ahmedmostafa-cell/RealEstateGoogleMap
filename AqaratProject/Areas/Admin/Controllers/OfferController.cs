using AqaratProject.Models;
using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Identity;
using AqaratProject.Hubs;
using static AqaratProject.Hubs.NotificationHub;
using Microsoft.AspNetCore.SignalR;

namespace AqaratProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferController : Controller
    {
        OfferBookingService offerBookingService;
        private readonly UserManager<ApplicationUser> _userManager;
        OfferNoteService offerNoteService;
        OfferVideoService offerVideoService;
        OfferImageService offerImageService;
        RegionService regionService;
        UnitService unitService;
        OfferService offerService;
        RealTimeNotifcationService realTimeNotifcationService;
        public static int notificationCounter = 0;
        public static List<string> messages = new List<string>();
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;

        Al3QaratContext ctx;
        public OfferController(OfferBookingService OfferBookingService,IHubContext<OrderHub> orderHub, RealTimeNotifcationService RealTimeNotifcationService, IHubContext<NotificationHub> notificationHub, UserManager<ApplicationUser> userManager, OfferNoteService OfferNoteService, OfferVideoService OfferVideoService, OfferImageService OfferImageService, RegionService RegionService,UnitService UnitService,OfferService OfferService, Al3QaratContext context)
        {
            offerBookingService = OfferBookingService;
            _userManager = userManager;
            offerNoteService = OfferNoteService;
            offerVideoService = OfferVideoService;
            regionService = RegionService;
            unitService = UnitService;
            offerImageService = OfferImageService;
            offerService = OfferService;
            ctx = context;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;

        }
        [Authorize(Roles = "Admin,العروض")]
        public IActionResult Index()
        {

            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();

            return View(model);


        }

        public IActionResult SalesReport()
        {

          

            return View();


        }

        public IActionResult unselledoffers()
        {



            return View();


        }

        

        public IActionResult report()
        {

            HomePageModel model = new HomePageModel();
           
            ViewBag.customersNo = offerService.getAll().Count();
            ViewBag.RecepEmployess = _userManager.Users.Where(a => a.AccountType == "موظف استقبال").ToList().Count;
            ViewBag.SalesEmployess = _userManager.Users.Where(a => a.AccountType == "موظف مبيعات").ToList().Count;
            ViewBag.Admin = _userManager.Users.Where(a => a.AccountType == "ادمن").ToList().Count;
            return View(model);


        }


        public IActionResult UnitsNo()
        {


            ViewBag.units = unitService.getAll();


            return View();


        }

        [Authorize(Roles = "Admin,رسم بياني يوضح عدد مقدمي الخدمات حسب المدن")]
        public IActionResult ShowData()
        {
            ViewBag.regions = regionService.getAll();
            return View();
        }

        [HttpPost]
        public List<object> OffersByAreaUnit()
        {
            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();
            var OfferByUnit = (from t in model.lstOffers
                                group t by t.UnitName into myVar
                                       select new
                                       {
                                           k = myVar.Key,
                                           c = myVar.Count()
                                       });


            List<GetNoOffersByUnit> lstGetNoOffersByUnit = new List<GetNoOffersByUnit>();
            foreach (var i in OfferByUnit)
            {
                GetNoOffersByUnit element = new GetNoOffersByUnit();
                element.CityName = i.k;
                element.count = i.c;
                lstGetNoOffersByUnit.Add(element);

            }
          
            List<object> data = new List<object>();
            List<string> labels = lstGetNoOffersByUnit.Select(p => p.CityName).ToList();
            data.Add(labels);
            List<int> SalesNumber = lstGetNoOffersByUnit.Select(p => p.count).ToList();
            data.Add(SalesNumber);
            return data;


        }


        


        public IActionResult Offerers()
        {

            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();

            return View(model);


        }



      


        [Authorize(Roles = "Admin")]
        public IActionResult Approve(string id)
        {

            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == Guid.Parse(id)).FirstOrDefault();
            oldItem.OfferStatus = "موافقة";
            var result = offerService.Edit(oldItem);
            if (result == true)
            {
                foreach (var i in _userManager.Users.ToList())
                {
                    TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                    oTbRealTimeNotifcation.NotificationType = "تم الموافقة عل عرض  ";
                    oTbRealTimeNotifcation.NotificationSyntax = oldItem.OfferSyntax;


                    oTbRealTimeNotifcation.CreatedBy = oldItem.OfferId.ToString();
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
                TempData[SD.Error] = "Error in Offer Profile  Updating.";
            }
            return RedirectToAction("Index");


        }




        [HttpPost]
        public async Task<IActionResult> Save(TbOffer ITEM, int id, List<IFormFile> files)
        {
            ITEM.UnitName = unitService.getAll().Where(a => a.UnitId == ITEM.UnitId).FirstOrDefault().UnitName;
            ITEM.RegionName = regionService.getAll().Where(a => a.RegionId == ITEM.RegionId).FirstOrDefault().RegionName;
            ITEM.ReceptionRepId = _userManager.GetUserAsync(User).Result.Id;
            ITEM.ReceptionRepName = _userManager.GetUserAsync(User).Result.FirstName;
            ITEM.icon = "screwdriver-wrench";

            ITEM.contract_type = ("غير محجوز");
            if (ITEM.OfferId == null)
            {

               

                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            string ImageName = Guid.NewGuid().ToString() + ".pdf";
                            var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                            using (var stream = System.IO.File.Create(filePaths))
                            {
                                await file.CopyToAsync(stream);
                            }
                            ITEM.PropertyDocumentPath = ImageName;
                        }
                    }





                    var result = offerService.Add(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Offer Profile successfully Created.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم اضافة عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferSyntax;


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
                        TempData[SD.Error] = "Error in Offer Profile  Creating.";
                    }


                }


            }
            else
            {
                if(ITEM.SalesRepId != null) 
                {
                    ITEM.SalesRepName = _userManager.Users.Where(a => a.Id == ITEM.SalesRepId).FirstOrDefault().FirstName;
                }
              
              
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
                            ITEM.PropertyDocumentPath = ImageName;
                        }
                    }



                  
                    ITEM.icon = "screwdriver-wrench";

                    var result = offerService.Edit(ITEM);
                    if (result == true)
                    {
                        TempData[SD.Success] = "Offer Profile successfully Updated.";
                        foreach (var i in _userManager.Users.ToList()) 
                        {
                            TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                            oTbRealTimeNotifcation.NotificationType = "تم تعديل عرض  ";
                            oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferSyntax;


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
                        TempData[SD.Error] = "Error in Offer Profile  Updating.";
                    }

                }
            }




            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();
            return View("Index", model);
        }




        [Authorize(Roles = "Admin,حذف العروض")]
        public IActionResult Delete(Guid id)
        {

            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();
            var resultSaleState = true;
            if (oldItem.contract_type == "مباع")
            {
                resultSaleState = false;

            }
            var resultOfferBooking = true;
            foreach (var i in offerBookingService.getAll()) 
            {
                if(i.OfferId == id) 
                {
                    resultOfferBooking = false;
                }
               
            }
            var resultOfferNotes = true;
            foreach (var i in offerNoteService.getAll())
            {
                if (i.OfferId == id)
                {
                    resultOfferNotes = false;
                }

            }
            var resultOfferVideo = true;
            foreach (var i in offerVideoService.getAll())
            {
                if (i.OfferId == id)
                {
                    resultOfferVideo = false;
                }

            }
            var resultOfferImage = true;
            foreach (var i in offerImageService.getAll())
            {
                if (i.OfferId == id)
                {
                    resultOfferImage = false;
                }

            }
         


            if (resultOfferBooking == true && resultOfferNotes == true && resultOfferVideo && resultOfferImage == true && resultSaleState == true)
            {
                var result = offerService.Delete(oldItem);
                if(result == true) 
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
            model.lstOffers = offerService.getAll();
            return View("Index", model);



        }



        [Authorize(Roles = "Admin, اضافة او تعديل العروض")]
        public IActionResult Form(Guid? id)
        {
            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();

            return View(oldItem);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult OfferSale(Guid? id)
        {
            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();
            ViewBag.SalesRep = _userManager.Users.Where(a => a.AccountType == "موظف مبيعات").ToList();
            ViewBag.Regions = regionService.getAll();

            return View(oldItem);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OfferSale(TbOffer ITEM)
        {
            ITEM.contract_type = "مباع";
            ITEM.UpdatedDate = DateTime.Now;
            ITEM.SalesRepName = _userManager.Users.Where(a => a.Id == ITEM.SalesRepId).FirstOrDefault().FirstName;
            TbOfferNote oTbOfferNote = new TbOfferNote();
            oTbOfferNote.OfferId = ITEM.OfferId;
            oTbOfferNote.SenderId = _userManager.GetUserAsync(User).Result.Id;
            oTbOfferNote.SenderName = _userManager.GetUserAsync(User).Result.FirstName;
            oTbOfferNote.SenderDepartment = _userManager.GetUserAsync(User).Result.Department;
            oTbOfferNote.NoteSyntax = "قام بحجز الفرع";
            oTbOfferNote.Visibility = "true";
            offerNoteService.Add(oTbOfferNote);
            var result = offerService.Edit(ITEM);
            if (result == true)
            {
                TempData[SD.Success] = "Offer Profile successfully Updated.";
                foreach (var i in _userManager.Users.ToList()) 
                {
                    TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                    oTbRealTimeNotifcation.NotificationType = "تم بيع عرض  ";
                    oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferSyntax;


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
                TempData[SD.Error] = "Error in Offer Profile  Updating.";
            }
            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();

            return View("Index" , model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult OfferArchive(Guid? id)
        {
            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == id).FirstOrDefault();
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();

            return View(oldItem);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult OfferArchive(TbOffer ITEM)
        {
            ITEM.contract_type = "ارشيف";
           
            var result = offerService.Edit(ITEM);
            if (result == true)
            {
                TempData[SD.Success] = "Offer Profile successfully Updated.";
                foreach(var i in _userManager.Users.ToList()) 
                {
                    TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();

                    oTbRealTimeNotifcation.NotificationType = "تم ارشفة عرض  ";
                    oTbRealTimeNotifcation.NotificationSyntax = ITEM.OfferSyntax;


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
                TempData[SD.Error] = "Error in Offer Profile  Updating.";
            }
            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll();

            return View("Index", model);
        }
        [Authorize(Roles = "Admin,تفاصيل العروض ")]
        public IActionResult Details(Guid? id)
        {
            HomePageModel model = new HomePageModel();
            model.lstOfferImages = offerImageService.getAll().Where(a=> a.OfferId == id);
            model.lsOfferVideo = offerVideoService.getAll().Where(a => a.OfferId == id);
            model.OneOffer = offerService.getAll().Where(a => a.OfferId == id).FirstOrDefault();
            model.lsOfferNote = offerNoteService.getAll().Where(a => a.OfferId == id).OrderBy(a=> a.CreatedDate).Where(a=> a.Visibility!="false");
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Form2(string id)
        {
            TbOffer oldItem = ctx.TbOffers.Where(a => a.OfferId == Guid.Parse(id)).FirstOrDefault();
            ViewBag.Units = unitService.getAll();
            ViewBag.Regions = regionService.getAll();

            return View(oldItem);
        }
    }
}
