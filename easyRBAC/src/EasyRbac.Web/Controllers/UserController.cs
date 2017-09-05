// Copyright (c) GZNB. All rights reserved.

using System.IO;
using EasyRbac.Application.User;
using EasyRbac.Dto.User;
using Microsoft.AspNetCore.Mvc;
using EasyRbac.Dto.Exceptions;

namespace EasyRbac.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserControllerService _userService;

        public UserController(IUserControllerService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public void CreateUser([FromBody]CreateUserDto dto)
        {
            this._userService.AddUser(dto);
        }

        [HttpGet]
        public string GetUser()
        {
            throw new EasyRbacException("ggg");
            return "111";
        }
    }
}