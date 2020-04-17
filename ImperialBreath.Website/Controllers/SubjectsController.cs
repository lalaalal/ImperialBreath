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
            try
            {
                jsonFileService.AddSubject(name);
            }
            catch (Exception e)
            {
                return RedirectToPage("/Result", new { comment = "Failed - " + e.Message });
            }
            return Ok();   
        }

        [Route("remove")]
        [HttpGet]
        public ActionResult Remove(
            [FromQuery] int subjectid)
        {
            try
            {
                jsonFileService.RemoveSubject(subjectid);
            }
            catch (Exception e)
            {
                return RedirectToPage("/Result", new { comment = "Failed - " + e.Message });
            }
            return Ok();
        }
    }
}