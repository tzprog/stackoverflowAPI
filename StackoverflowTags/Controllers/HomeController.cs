using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackoverflowTags.Data;
using StackoverflowTags.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackoverflowTags.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStackoverflowData _StackOverflowData;

        public HomeController(ILogger<HomeController> logger, IStackoverflowData StackoverflowData)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
