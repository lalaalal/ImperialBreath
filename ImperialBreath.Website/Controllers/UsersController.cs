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
    public class UsersController : ControllerBase
    {
        private UserService userService;
        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [Route("auth")]
        [HttpPost]
        public ActionResult Authorize(
            [FromForm] string id, 
            [FromForm] string pw)
        {
            if (userService.Authorize(id, pw))
            {
                HttpContext.Session.SetString(UserService.SESSION_USER_ID, id);
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/result", new { status = false, comment = "실패" });
        }

        [Route("logout")]
        [HttpGet]
        public ActionResult Logout()
        {
            if (HttpContext.Session.GetString(UserService.SESSION_USER_ID) != null)
                HttpContext.Session.Remove(UserService.SESSION_USER_ID);
            return RedirectToPage("/Index");
        }

        [Route("add")]
        [HttpPost]
        public ActionResult SignUp(
            [FromForm] string id,
            [FromForm] string pw)
        {
            try
            {
                userService.AddUser(id, pw);
            } catch (UserException exception)
            {
                return RedirectToPage("/result", new { status = false, comment = exception.Message });
            }
            return RedirectToPage("/result", new { status = true, comment = "성공" });
        }

        [Route("toggle-task")]
        [HttpGet]
        public ActionResult ToggleTask([FromQuery] int taskId)
        {
            var userId = HttpContext.Session.GetString(UserService.SESSION_USER_ID);
            if (userId == null)
                return RedirectToPage("/login");
            userService.ToggleTask(userId, taskId);
            return Ok();
        }
    }
}