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
    public class LogHistoryApiController : ControllerBase
    {
        LogHistoryService logHistoryService;
        Al3QaratContext ctx;
        public LogHistoryApiController(LogHistoryService LogHistoryService, Al3QaratContext context)
        {
            logHistoryService = LogHistoryService;
            ctx = context;

        }
        // GET: api/<LogHistoryApiController>
        [HttpGet]
        public IEnumerable<TbLogHistory> Get()
        {
            List<TbLogHistory> lstLogHistories = logHistoryService.getAll().ToList();

            return lstLogHistories;
        }

        // GET api/<LogHistoryApiController>/5
        [HttpGet("{id}")]
        public IEnumerable<TbLogHistory> Get(Guid id)
        {
            List<TbLogHistory> lstLogHistories = logHistoryService.getAll().Where(A => A.LoggedUserId == id).ToList();

            return lstLogHistories;
        }

        [HttpGet("{id}/{optionDate1}/{optionDate2}")]
        public IEnumerable<TbLogHistory> Get(Guid id , string optionDate1 , string optionDate2)
        {
            List<TbLogHistory> lstLogHistories = new List<TbLogHistory>();
            if (id != null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = logHistoryService.getAll().Where(A => A.LoggedUserId == id).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 == null)
            {
                lstLogHistories = logHistoryService.getAll().Where(A => A.LoggedUserId == id).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id != null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = logHistoryService.getAll().Where(A => A.LoggedUserId == id).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 == null)
            {
                lstLogHistories = logHistoryService.getAll().ToList();
            }
            else if (id == null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = logHistoryService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = logHistoryService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id == null && optionDate2 == null && optionDate1 != null)
            {
                lstLogHistories = logHistoryService.getAll().Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).ToList();
            }
            else if (id != null && optionDate2 != null && optionDate1 != null)
            {
                lstLogHistories = logHistoryService.getAll().Where(A => A.LoggedUserId == id).Where(a => a.CreatedDate > DateTime.Parse(optionDate1)).Where(a => a.CreatedDate < DateTime.Parse(optionDate2)).ToList();
            }



            return lstLogHistories;
        }

        // POST api/<LogHistoryApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LogHistoryApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogHistoryApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
