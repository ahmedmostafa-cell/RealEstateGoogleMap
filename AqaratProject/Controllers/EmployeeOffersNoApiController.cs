using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeOffersNoApiController : ControllerBase
    {
        OfferBookingService offerBookingService;
        OfferService offerService;
        Al3QaratContext ctx;
        public EmployeeOffersNoApiController(OfferBookingService OfferBookingService, OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;
            offerBookingService = OfferBookingService;

        }
        // GET: api/<EmployeeOffersNoApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeOffersNoApiController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            List<TbOffer> lstOffer = offerService.getAll().Where(A => A.SalesRepId == id.ToString()).Where(a => a.contract_type == "مباع").ToList();
            List<TbOfferBooking> lstOfferBooking = offerBookingService.getAll().Where(A => A.SalesRepId == id.ToString()).ToList();
            List<Guid?> lstOfferId = new List<Guid?>();
            foreach(var i in lstOffer) 
            {
                lstOfferId.Add(i.OfferId);
            }
            foreach(var i in lstOfferBooking) 
            {
                lstOfferId.Add(i.OfferId);

            }
            var activeDays = (from t in lstOfferId
                              group t by t.Value into myVar
                              select new
                              {
                                  k = myVar.Key,
                                  c = myVar.Count()
                              });
            return lstOffer.Count().ToString();
        }

        // POST api/<EmployeeOffersNoApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeOffersNoApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeOffersNoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
