using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImperialBreath.Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImperialBreath.Website.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        public JsonFileService jsonFileService { get; }

        public SubjectsController(JsonFileService jsonFileService)
        {
            this.jsonFileService = jsonFileService;
        }

        [HttpGet]
        public IEnumerable<Models.Subject> Get()
        {
            return jsonFileService.GetSubjects();
        }

        [Route("add")]
        [HttpGet]
        public ActionResult Add(
            [FromQuery] string name)
        {
            jsonFileService.AddSubject(name);
            return Ok();   
        }

        [Route("remove")]
        [HttpGet]
        public ActionResult Remove(
            [FromQuery] int subjectid)
        {
            jsonFileService.RemoveSubject(subjectid);
            return Ok();
        }
    }
}