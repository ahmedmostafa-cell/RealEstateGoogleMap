using AqaratProject.Hubs;
using BL;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using static AqaratProject.Hubs.NotificationHub;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class UnMovedUnitsController : ControllerBase
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
        public UnMovedUnitsController(UserManager<ApplicationUser> userManager, SettingService SettingService, OfferBookingService OfferBookingService, IHubContext<OrderHub> orderHub, IHubContext<NotificationHub> notificationHub, OfferNoteService OfferNoteService, OfferService OfferService, Al3QaratContext Ctx, RealTimeNotifcationService RealTimeNotifcationService)
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
        // GET: api/<UnMovedUnitsController>
        [HttpGet]
		public string Get()
		{
            List<TbOffer> lstTbOffer = new List<TbOffer>();
            List<TbOffer> lstTbOfferNoFollow = new List<TbOffer>();
            List<TbOfferNote> lstOfferNtes = new List<TbOfferNote>();
            lstTbOffer = offerService.getAll().Where(a => a.contract_type != "مباع" && a.contract_type != "ارشيف").ToList();
            TbSetting oTbSetting = settingService.getAll().FirstOrDefault();
            int noDays = int.Parse(oTbSetting.NoOfBookingDays);
            lstOfferNtes = offerNoteService.getAll().Where(a => a.CreatedDate > DateTime.Now.AddDays(-2)).ToList();
            if (lstOfferNtes.Count == 0)
            {
                foreach (var i in lstTbOffer)
                {
                    foreach (var ii in _userManager.Users.ToList())
                    {
                        
                      
                      
                          
                          
                           
                            lstTbOfferNoFollow.Add(i);

                       

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
                          
                           
                                lstTbOfferNoFollow.Add(i);
                                break;

                           



                        }
                    }



                }
            }

          
            return lstTbOfferNoFollow.Count().ToString();
		}

		// GET api/<UnMovedUnitsController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<UnMovedUnitsController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<UnMovedUnitsController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<UnMovedUnitsController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
