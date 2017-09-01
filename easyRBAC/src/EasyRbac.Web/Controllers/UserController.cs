// Copyright (c) GZNB. All rights reserved.

using System.IO;
using EasyRbac.Dto.User;
using Microsoft.AspNetCore.Mvc;
using EasyRbac.Dto.Exceptions;

namespace EasyRbac.Web.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public CreateUserDto CreateUser([FromBody]CreateUserDto dto)
        {
            return dto;
        }

        [HttpGet]
        public string GetUser()
        {
            throw new EasyRbacException("ggg");
            return "111";
        }
    }
}