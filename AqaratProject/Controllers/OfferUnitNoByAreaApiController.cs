using AqaratProject.Models;
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

    public class OfferUnitNoByAreaApiController : ControllerBase
    {
        OfferService offerService;
        Al3QaratContext ctx;
        public OfferUnitNoByAreaApiController(OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<OfferUnitNoByAreaApiController>
        [HttpGet]
        public List<object> Get()
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

        // GET api/<OfferUnitNoByAreaApiController>/5
        [HttpGet("{id}")]
        public List<object> Get(Guid id)
        {
            HomePageModel model = new HomePageModel();
            model.lstOffers = offerService.getAll().Where(a=> a.RegionId == id);
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

        // POST api/<OfferUnitNoByAreaApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OfferUnitNoByAreaApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferUnitNoByAreaApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
