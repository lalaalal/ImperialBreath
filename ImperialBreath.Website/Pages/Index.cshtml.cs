using System;
using System.Collections.Generic;
using System.Linq;
using ImperialBreath.Website.Models;
using ImperialBreath.Website.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ImperialBreath.Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly JsonFileService jsonFileService;
        private readonly UserService _userService;
        public string UserId { get; private set; }

        public IEnumerable<Task> Tasks { get; private set; }

        public IEnumerable<Subject> Subjects { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            JsonFileService taskService,
            UserService userService)
        {
            _logger = logger;
            jsonFileService = taskService;
            _userService = userService;
        }

        public void OnGet()
        {
            UserId = HttpContext.Session.GetString(UserService.SESSION_USER_ID);
            if (UserId == null)
                Response.Redirect("/Login");
            Tasks = jsonFileService.GetTasks();
            Subjects = jsonFileService.GetSubjects();
            
        }

        public int[] GetDoneTaskId()
        {
            if (UserId == null)
                return new int[] { };

            IEnumerable<User> users = jsonFileService.GetUsers();
            User currentUser = users.First(user => user.Id == UserId);
            if (currentUser.DoneTaskId == null)
                return new int[0];
            return currentUser.DoneTaskId;
        }
    }
}
