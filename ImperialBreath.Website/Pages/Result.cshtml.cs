﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ImperialBreath.Website.Pages
{
    public class ResultModel : PageModel
    {
        private readonly ILogger<ResultModel> _logger;
        public bool Status { get; private set; }
        public string Comment { get; private set; }

        public ResultModel(ILogger<ResultModel> logger)
        {
            _logger = logger;
        }

        public void OnGet([FromQuery] bool status, [FromQuery] string comment)
        {
            Status = status;
            Comment = comment;
        }
    }
}
