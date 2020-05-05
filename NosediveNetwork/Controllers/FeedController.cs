using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NosediveNetwork.Models;
using NosediveNetwork.Services;

namespace NosediveNetwork.Controllers
{
    public class FeedController : Controller
    {

        private readonly NosediveService _nosediveService;

        public FeedController(NosediveService nosediveService)
        {
            _nosediveService = nosediveService;
        }
        public IActionResult Index()
        {
            return View(new FeedViewModel(_nosediveService));
        }
    }
}