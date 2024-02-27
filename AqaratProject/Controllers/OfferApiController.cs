using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{
    public class MySearchs
    {
        public bool OnlyActive { get; set; } = true;
        public bounds Ids { get; set; }
    }
    public class boundss
    {

        public decimal neLat { get; set; }

        public decimal neLng { get; set; }
        public decimal swLat { get; set; }
        public decimal swLng { get; set; }


    }
    [Route("api/[controller]")]
    [ApiController]
    public class OfferApiController : ControllerBase
    {
        OfferService offerService;
        Al3QaratContext ctx;
        public OfferApiController(OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<OfferApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OfferApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbOffer> Get(Guid id )
        {
            //decimal swLat = arr.Ids.swLat - 1;
            //decimal neLng = arr.Ids.neLng - 1;
            //decimal neLat = arr.Ids.neLat - 1;
            //decimal swLng = arr.Ids.swLng - 1;

            List<TbOffer> map1 = new List<TbOffer>();
            List<TbMap> map2 = new List<TbMap>();
            List<TbMap> map3 = new List<TbMap>();
            List<TbMap> map4 = new List<TbMap>();
            //map4 = ctx.TbMaps.ToList();
            //map1 = ctx.TbOffers.Where(a => a.lat >= arr.Ids.swLat && a.lat <= arr.Ids.neLat && a.lng >= arr.Ids.swLng && a.lng <= arr.Ids.neLng).ToList();

            return map1;
        }

        // POST api/<OfferApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<OfferApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OfferApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
