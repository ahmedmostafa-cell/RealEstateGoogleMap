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
    public class EmployessSalesNoApiController : ControllerBase
    {
       OfferService offerService;
        Al3QaratContext ctx;
        public EmployessSalesNoApiController(OfferService OfferService, Al3QaratContext context)
        {
            offerService = OfferService;
            ctx = context;

        }
        // GET: api/<EmployessSalesNoApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployessSalesNoApiController>/5
        [HttpGet("{id}")]
        public string Get(Guid id)
        {
            List<TbOffer> lstOffer = offerService.getAll().Where(A => A.SalesRepId == id.ToString()).Where(a=> a.contract_type == "مباع").ToList();

            return lstOffer.Count().ToString();
        }

        // POST api/<EmployessSalesNoApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployessSalesNoApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployessSalesNoApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
