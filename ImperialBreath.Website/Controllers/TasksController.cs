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
    public class TasksController : ControllerBase
    {
        public JsonFileService jsonFileService { get; }

        public TasksController(JsonFileService jsonFileService)
        {
            this.jsonFileService = jsonFileService;
        }

        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
            return jsonFileService.GetTasks();
        }

        [Route("add")]
        [HttpGet]
        public ActionResult Add(
            [FromQuery] int subjectId,
            [FromQuery] string summary,
            [FromQuery] string content,
            [FromQuery] string deadline)
        {
            try
            {
                jsonFileService.AddTask(subjectId, summary, content, DateTime.Parse(deadline));
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
            [FromQuery] int taskId)
        {
            try
            {
                jsonFileService.RemoveTask(taskId);
            }
            catch (Exception e)
            {
                return RedirectToPage("/Result", new { comment = "Failed - " + e.Message });
            }
            return Ok();
        }
    }
}