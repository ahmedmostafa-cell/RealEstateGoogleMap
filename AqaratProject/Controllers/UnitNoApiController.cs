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
    public class UnitNoApiController : ControllerBase
    {
        UnitService unitService;
        OfferService offerService;
        Al3QaratContext ctx;
        public UnitNoApiController(UnitService UnitService,OfferService OfferService, Al3QaratContext context)
        {
            unitService = UnitService;
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<UnitNoApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UnitNoApiController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            List<TbOffer> lstOffer = offerService.getAll().Where(A => A.UnitId == id).ToList();

            return lstOffer.Count().ToString();
        }

        // POST api/<UnitNoApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UnitNoApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UnitNoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
