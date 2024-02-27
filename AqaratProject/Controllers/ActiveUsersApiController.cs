using BL;
using Domains;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AqaratProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveUsersApiController : ControllerBase
    {
       ActiveUsersService activeUsersService;
        Al3QaratContext ctx;
        public ActiveUsersApiController(ActiveUsersService ActiveUsersService, Al3QaratContext context)
        {
            activeUsersService = ActiveUsersService;
            ctx = context;

        }
        // GET: api/<ActiveUsersApiController>
        [HttpGet]
        public IEnumerable<TbActiveUsers> Get()
        {
            List<TbActiveUsers> lstActiveUsers = activeUsersService.getAll().ToList();

            return lstActiveUsers;
        }

        // GET api/<ActiveUsersApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ActiveUsersApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ActiveUsersApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ActiveUsersApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
