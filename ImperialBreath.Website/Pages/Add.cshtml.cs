using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ImperialBreath.Website.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;

        public AddModel(ILogger<AddModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
