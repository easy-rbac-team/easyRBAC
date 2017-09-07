// Copyright (c) GZNB. All rights reserved.

using System.IO;
using System.Threading.Tasks;
using EasyRbac.Application.User;
using EasyRbac.Dto.User;
using Microsoft.AspNetCore.Mvc;
using EasyRbac.Dto.Exceptions;

namespace EasyRbac.Web.Controllers
{
    [Route("[controller]")]
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

        [HttpDelete("{userId}")]
        public Task DisableUser(long userId)
        {
            return this._userService.DisableUser(userId);
        }

        [HttpGet("{userId}")]
        public Task<UserInfoDto> GetUserInfo(long userId)
        {
            return this._userService.GetUserInfo(userId);
        }
    }
}