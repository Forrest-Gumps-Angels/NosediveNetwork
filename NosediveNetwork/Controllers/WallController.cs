using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NosediveNetwork.Models;
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
            return View(new WallViewModel(_nosediveService));
        }

        [HttpPost]
        public IActionResult PostComment(string content, string postid, string user)
        {
            _nosediveService.CreateComment(_nosediveService.GetPostFromId(postid), _nosediveService.GetUser(user), content);
            return View("Index", new WallViewModel(_nosediveService));
        }
    }
}