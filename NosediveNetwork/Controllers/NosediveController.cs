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

        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            ///////// Creating dummy data ////////////


            // Creating users
            _nosediveService.CreateUser(new Models.User() { Name = "Morten Hansen", Age = 22, Gender = "Male" });
            _nosediveService.CreateUser(new Models.User() { Name = "Rasmus Føgh", Age = 12, Gender = "Unidentifiable" });
            _nosediveService.CreateUser(new Models.User() { Name = "Viktor Lundsgaard", Age = 23, Gender = "Male" });

            // Creating circles
            _nosediveService.CreateCircle(_nosediveService.GetUser("Morten Hansen"), "Area 51 conspiracy group");
            _nosediveService.CreateCircle(_nosediveService.GetUser("Viktor Lundsgaard"), "Hot girls group");
            _nosediveService.CircleAddUser(_nosediveService.GetCircle("Area 51 conspiracy group"), _nosediveService.GetUser("Rasmus Føgh"));

            // Creating posts
            _nosediveService.CreateTextPost(_nosediveService.GetUser("Morten Hansen"), "Hej jeg er Morten!", _nosediveService.GetCircle("Area 51 conspiracy group"));
            _nosediveService.CreateTextPost(_nosediveService.GetUser("Rasmus Føgh"), "Hej jeg er i tvivl om mit køn!", _nosediveService.GetCircle("Area 51 conspiracy group"));
            _nosediveService.CreateTextPost(_nosediveService.GetUser("Viktor Lundsgaard"), "Hej jeg laver mange damer!", _nosediveService.GetCircle("Hot girls group"));


            // Adding friends
            _nosediveService.UserAddFriend(_nosediveService.GetUser("Morten Hansen"), _nosediveService.GetUser("Viktor Lundsgaard"));

            return _nosediveService.Feed(_nosediveService.GetUser("Morten Hansen"));
        }

    }
}
