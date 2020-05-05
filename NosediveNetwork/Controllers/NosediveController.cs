using Microsoft.AspNetCore.Mvc;
using NosediveNetwork.Models;
using NosediveNetwork.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NosediveNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NosediveController : ControllerBase
    {
        private readonly NosediveService _nosediveService;

        public NosediveController(NosediveService nosediveService)
        {
            _nosediveService = nosediveService;
        }

        //[HttpGet]
        //public ActionResult<List<User>> Get() =>
        //    _nosediveService.GetUser();

    }
}
