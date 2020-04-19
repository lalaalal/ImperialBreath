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
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;
        private readonly JsonFileService jsonFileService;
        public List<Subject> Subjects { get; private set; }

        public AddModel(ILogger<AddModel> logger, JsonFileService jsonFileService)
        {
            this.jsonFileService = jsonFileService;
            _logger = logger;
        }

        public void OnGet()
        {
            Subjects = jsonFileService.GetSubjects().ToList();
        }
    }
}
