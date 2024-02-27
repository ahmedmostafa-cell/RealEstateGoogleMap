using AqaratProject.Hubs;
using BL;
using Domains;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static AqaratProject.Hubs.NotificationHub;

namespace AqaratProject.Areas.Admin.Controllers
{
    public class MySearch
    {
        
             public string id { get; set; } 
        public bool OnlyActive { get; set; } = true;
        public List<string> Ids { get; set; }
    }
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        SettingService settingService;
        RealTimeNotifcationService realTimeNotifcationService;
        OfferNoteService offerNoteService;
        OfferService offerService;
        OfferBookingService offerBookingService;
        Al3QaratContext ctx;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly IHubContext<NotificationHub> _notificationHub;
        public HomeController(UserManager<ApplicationUser> userManager,  SettingService SettingService ,OfferBookingService OfferBookingService,IHubContext<OrderHub> orderHub,  IHubContext<NotificationHub> notificationHub, OfferNoteService OfferNoteService,OfferService OfferService,Al3QaratContext Ctx,RealTimeNotifcationService RealTimeNotifcationService)
        {
           
           
            offerService = OfferService;
            offerNoteService = OfferNoteService;
            ctx = Ctx;
            realTimeNotifcationService = RealTimeNotifcationService;
            _orderHub = orderHub;
            _notificationHub = notificationHub;
            offerBookingService = OfferBookingService;
            settingService = SettingService;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            List<TbOffer> lstTbOffer = new List<TbOffer>();
            List<TbOffer> lstTbOfferNoFollow = new List<TbOffer>();
            List<TbOfferNote> lstOfferNtes = new List<TbOfferNote>();
            lstTbOffer = offerService.getAll().Where(a=> a.contract_type != "مباع" && a.contract_type != "ارشيف").ToList();
            TbSetting oTbSetting = settingService.getAll().FirstOrDefault();
            int noDays = int.Parse(oTbSetting.NoOfBookingDays);
            lstOfferNtes = offerNoteService.getAll().Where(a=> a.CreatedDate > DateTime.Now.AddDays(-2)).ToList();
            if(lstOfferNtes.Count ==0) 
            {
                foreach (var i in lstTbOffer)
                {
                    foreach (var ii in _userManager.Users.ToList()) 
                    {
                        TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();
                       


                            oTbRealTimeNotifcation.NotificationType = "عرض غير متحرك  ";
                            oTbRealTimeNotifcation.NotificationSyntax = i.OfferSyntax;


                            oTbRealTimeNotifcation.CreatedBy = i.OfferId.ToString();
                            oTbRealTimeNotifcation.UpdatedBy = "عرض غير متحرك";
                            oTbRealTimeNotifcation.Notes = ii.Id;
                            List<TbRealTimeNotifcation> lstNotify = realTimeNotifcationService.getAll().Where(a => a.CreatedBy == oTbRealTimeNotifcation.CreatedBy && a.Notes == _userManager.GetUserAsync(User).Result.Id).ToList();
                            if (lstNotify.Count == 0)
                            {
                                realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                                notificationCounter = ctx.TbRealTimeNotifcations.Where(a => a.Notes == _userManager.GetUserAsync(User).Result.Id).Count();
                                List<MessageObject> messages = new List<MessageObject>();
                                foreach (var iii in ctx.TbRealTimeNotifcations.ToList())
                                {
                                    messages.Add(new MessageObject { id = iii.RealTimeNotifcationId.ToString(), mesage = iii.NotificationType });


                                }
                                _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
                                _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();
                                lstTbOfferNoFollow.Add(i);

                            }
                       
                    }
                   
                   
                   

                  
                }

            }
            else 
            {
                foreach (var i in lstTbOffer)
                {
                   
                        foreach (var b in lstOfferNtes)
                        {
                            if (i.OfferId == b.OfferId)
                            {
                                break;
                            }
                            else
                            {
                                TbRealTimeNotifcation oTbRealTimeNotifcation = new TbRealTimeNotifcation();
                               


                                    oTbRealTimeNotifcation.NotificationType = "عرض غير متحرك  ";
                                    oTbRealTimeNotifcation.NotificationSyntax = i.OfferSyntax;


                                    oTbRealTimeNotifcation.CreatedBy = i.OfferId.ToString();
                                    oTbRealTimeNotifcation.UpdatedBy = "عرض غير متحرك";
                                    oTbRealTimeNotifcation.Notes = _userManager.GetUserAsync(User).Result.Id;
                                    List<TbRealTimeNotifcation> lstNotify = realTimeNotifcationService.getAll().Where(a => a.CreatedBy == oTbRealTimeNotifcation.CreatedBy && a.Notes == _userManager.GetUserAsync(User).Result.Id).ToList();
                                    if (lstNotify.Count == 0)
                                    {
                                        realTimeNotifcationService.Add(oTbRealTimeNotifcation);
                                         notificationCounter = ctx.TbRealTimeNotifcations.Where(a => a.Notes == _userManager.GetUserAsync(User).Result.Id).Count();
                                        List<MessageObject> messages = new List<MessageObject>();
                                        foreach (var iii in ctx.TbRealTimeNotifcations.ToList())
                                        {
                                            messages.Add(new MessageObject { id = iii.RealTimeNotifcationId.ToString(), mesage = iii.NotificationType });


                                        }
                                        _orderHub.Clients.All.SendAsync("newOrder").GetAwaiter().GetResult();
                                        _notificationHub.Clients.All.SendAsync("LoadNotification", messages, notificationCounter).GetAwaiter().GetResult();
                                        lstTbOfferNoFollow.Add(i);
                                        break;

                                    }
                               


                            }
                        }

                   
                      
                }
            }

            List<TbOfferBooking> lstTbOfferBooking = offerBookingService.getAll().Where(a => a.CreatedDate < DateTime.Now.AddDays(-noDays)).Where(a=> a.CurrentState == 1).ToList();

            foreach(var i in lstTbOfferBooking) 
            {
                i.CurrentState = 0;
                i.CreatedBy = "الحجز غير فعال";
                offerBookingService.Edit(i);
                TbOffer oTbOffer = offerService.getAll().Where(a => a.OfferId == i.OfferId).FirstOrDefault();
                oTbOffer.contract_type = "غير محجوز";
                offerService.Edit(oTbOffer);
            }


            return View();
        }
        [Authorize(Roles = "Admin, الرسوم البيانية")]
        public IActionResult MainPage()
        {
            return View();
        }
        public IActionResult RealTimeNotification(MySearch arr)
        {
            //foreach (var i in arr.Ids)
            //{
            //    TbRealTimeNotifcation oTbRealTimeNotifcation = ctx.TbRealTimeNotifcations.Where(a => a.RealTimeNotifcationId == Guid.Parse(i)).FirstOrDefault();
            //    RealTimeNotifcationService.Delete(oTbRealTimeNotifcation);

            //}


            TbRealTimeNotifcation oTbRealTimeNotifcation = ctx.TbRealTimeNotifcations.Where(a => a.RealTimeNotifcationId == Guid.Parse(arr.id)).FirstOrDefault();
            if (oTbRealTimeNotifcation.UpdatedBy == "اضافة استفسار")
            {
                Guid? id = Guid.Parse(oTbRealTimeNotifcation.CreatedBy);
                var data = new { id = id, type = oTbRealTimeNotifcation.UpdatedBy };
                realTimeNotifcationService.Delete(oTbRealTimeNotifcation);
                //return RedirectToAction("Form", "ContactUs", new { id });

                return Json(data);
            }
            else
            {
                Guid id = Guid.Parse(oTbRealTimeNotifcation.CreatedBy);
                var data = new { id = id, type = oTbRealTimeNotifcation.UpdatedBy };
                realTimeNotifcationService.Delete(oTbRealTimeNotifcation);
                //return RedirectToAction("Form", "Complains", new { id });

                return Json(data);
            }


        }
    }
}
