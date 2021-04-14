using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImperialBreath.Website.Models;
using ImperialBreath.Website.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ImperialBreath.Website.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ILogger<UpdateModel> _logger;
        private readonly JsonFileService jsonFileService;
        public List<Subject> Subjects { get; private set; }
        public Models.Task Task;

        public UpdateModel(ILogger<UpdateModel> logger, JsonFileService jsonFileService)
        {
            this.jsonFileService = jsonFileService;
            _logger = logger;
            
        }

        public void OnGet([FromQuery] int taskId)
        {
            Subjects = jsonFileService.GetSubjects().ToList();
            Task = jsonFileService.GetTasks().First(task => task.TaskId == taskId);
        }
    }
}
