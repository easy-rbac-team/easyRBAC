// Copyright (c) GZNB. All rights reserved.

using EasyRbac.Dto.User;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public CreateUserDto CreateUser(CreateUserDto dto)
        {
            return dto;

        }
    }
}