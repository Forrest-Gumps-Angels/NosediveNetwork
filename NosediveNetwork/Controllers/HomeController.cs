using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NosediveNetwork.Models;
using NosediveNetwork.Services;

namespace NosediveNetwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly NosediveService _nosediveService;

        private readonly ILogger<HomeController> _logger;


        public HomeController(NosediveService nosediveService, ILogger<HomeController> logger)
        {
            _nosediveService = nosediveService;
            _logger = logger;
        }

        public IActionResult Index(string user)
        {
            if (_nosediveService.GetAllUsers().FirstOrDefault(x => x.Name == user) != null) CurrentUser.CurrentUserName = user;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
