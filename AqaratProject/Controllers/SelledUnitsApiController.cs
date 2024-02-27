using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{
    public class MySearchss
    {
        public bool OnlyActive { get; set; } = true;
        public bounds Ids { get; set; }
    }
    public class boundsss
    {

        public decimal neLat { get; set; }

        public decimal neLng { get; set; }
        public decimal swLat { get; set; }
        public decimal swLng { get; set; }


    }
   

    [Route("api/[controller]")]
	[ApiController]
	public class SelledUnitsApiController : ControllerBase
	{
        OfferService offerService;
        Al3QaratContext ctx;
        public SelledUnitsApiController(OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<SelledUnitsApiController>
        [HttpGet]
		public string Get()
		{
			return offerService.getAll().Where(a => a.contract_type == "مباع").ToList().Count().ToString();
		}

        // GET api/<SelledUnitsApiController>/5
        [HttpGet("{option}/{option1}")]
        public string Get(string option, string option1)
        {
            List<TbOffer> lstOffer = new List<TbOffer>();
            if (option == null && option1 == null)
            {
                return offerService.getAll().Where(a => a.contract_type == "مباع").ToList().Count().ToString();
            }
            else if (option != null && option1 == null)
            {
                return offerService.getAll().Where(a=> a.UpdatedDate <= DateTime.Parse(option1)).Where(a => a.contract_type == "مباع").ToList().Count().ToString();
            }
            else if (option == null && option1 != null)
            {
                return offerService.getAll().Where(a => a.UpdatedDate >= DateTime.Parse(option)).Where(a => a.contract_type == "مباع").ToList().Count().ToString();
            }
            else if (option != null && option1 != null)
            {
              
               
                return offerService.getAll().Where(a=> a.UpdatedDate <= DateTime.Parse(option1)).Where(a => a.UpdatedDate >= DateTime.Parse(option)).Where(a => a.contract_type == "مباع").ToList().Count().ToString();
            }
            else 
            {
                return offerService.getAll().Where(a => a.contract_type == "مباع").ToList().Count().ToString();
            }
          



           
        }

        // POST api/<SelledUnitsApiController>
        [HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<SelledUnitsApiController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<SelledUnitsApiController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
