using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NosediveNetwork.Services;

namespace NosediveNetwork.Controllers
{
    public class WallController : Controller
    {
        private readonly NosediveService _nosediveService;

        public WallController(NosediveService nosediveService)
        {
            _nosediveService = nosediveService;
        }

        public IActionResult Index()
        {
            return View(_nosediveService.Wall(_nosediveService.GetUser("Morten Hansen")));
        }
    }
}