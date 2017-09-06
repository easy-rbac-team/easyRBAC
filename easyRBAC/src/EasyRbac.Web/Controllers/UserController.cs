// Copyright (c) GZNB. All rights reserved.

using System.IO;
using System.Threading.Tasks;
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
        public Task CreateUser([FromBody]CreateUserDto dto)
        {
           return this._userService.AddUser(dto);
        }
       
        [HttpPut("{userId:long}/pwd")]
        public Task ChangePassword(long userId,[FromBody]ChangePwd dto)
        {
            return this._userService.ChangePwd(userId, dto);
        }

        [HttpGet]
        public string GetUser()
        {
            throw new EasyRbacException("ggg");
            return "111";
        }
    }
}