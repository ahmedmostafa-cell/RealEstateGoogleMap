using BL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class USelledUnitsApiController : ControllerBase
	{
        OfferService offerService;
        Al3QaratContext ctx;
        public USelledUnitsApiController(OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<USelledUnitsApiController>
        [HttpGet]
        public string Get()
        {
            return offerService.getAll().Where(a => a.contract_type != "مباع" && a.contract_type != "ارشيف").ToList().Count().ToString();
        }

        // GET api/<USelledUnitsApiController>/5
        [HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/<USelledUnitsApiController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<USelledUnitsApiController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<USelledUnitsApiController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
