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
    public class EmployessPerfApiController : ControllerBase
    {
       OfferBookingService offerBookingService;
        Al3QaratContext ctx;
        public EmployessPerfApiController(OfferBookingService OfferBookingService, Al3QaratContext context)
        {
            offerBookingService = OfferBookingService;
            ctx = context;

        }
        // GET: api/<EmployessPerfApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployessPerfApiController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            List<TbOfferBooking> lstOfferBooking = offerBookingService.getAll().Where(A => A.SalesRepId == id.ToString()).ToList();

            return lstOfferBooking.Count().ToString();
        }

        // POST api/<EmployessPerfApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployessPerfApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployessPerfApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
